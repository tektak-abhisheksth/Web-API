using BLL.Account;
using DAL.Account;
using Model.Account;
using Model.Common;
using Model.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TekTak.iLoop.Sealed.Account;
using Utility;

namespace Test
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        #region Variables
        private List<Type> _displayClasses;
        private Dictionary<int, string> _cboCategories;
        private Dictionary<string, string> _cboItems;
        private Type _attType;
        private List<RichTextBox> _lst;
        private List<Label> _lbl;
        private State _state;
        private ConstructorInfo _constorInfo;
        private ConstructorInfo _constorInfoUniversal;
        private IEnumerable<object> _samples;
        private Assembly[] _assemblies;
        private BindingFlags _bFlags;
        private string[] _ignoreList;
        #endregion

        #region Events
        private void Test_Load(object sender, EventArgs e)
        {
            _attType = typeof(AsyncStateMachineAttribute);
            _assemblies = AppDomain.CurrentDomain.GetAssemblies();
            _ignoreList = new[] { "Equals", "GetHashCode", "GetType", "ToString" };

            _cboCategories = new Dictionary<int, string> { { 1, "EF" }, { 2, "RPC" }, { 3, "BAL" } };
            cboCategories.DataSource = new BindingSource(_cboCategories, null);
            cboCategories.DisplayMember = "Value";
            cboCategories.ValueMember = "Key";
            //cboCategories.SelectedIndex = 0;

            _state = new State();
            var uow = new TekTak.iLoop.UOW.UnitOfWork();
            _constorInfo = new ConstructorInfo
            {
                ConstorParamType = new[] { uow.GetServices().GetType() },
                ConstorParams = new object[] { uow.GetServices() }
            };
            LoadState();
            _samples = API.SampleConfig.GetSamples();
            cboClasses.SelectedIndex = cboClasses.Items.Count >= _state.Combo ? _state.Combo : 0;
            if (treeMethods.Nodes.Count > 0) treeMethods.SelectedNode = treeMethods.Nodes.Count > _state.Node ? treeMethods.Nodes[_state.Node] : treeMethods.Nodes[0];
            treeMethods.Focus();

        }

        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var key = ((KeyValuePair<int, string>)cboCategories.SelectedItem).Key;
            Assembly assembly;
            switch (key)
            {
                case 1:
                    assembly = Assembly.GetAssembly(typeof(AccountRepository));
                    _displayClasses = assembly.GetTypes().Where(t => t.Namespace != null && t.IsClass && t.IsPublic).ToList();
                    if (_displayClasses.Any())
                    {
                        var uw = new DAL.DbEntity.UnitOfWork();
                        _constorInfoUniversal = new ConstructorInfo
                        {
                            ConstorParamType = new[] { uw.GetContext().GetType() },
                            ConstorParams = new[] { uw.GetContext() }
                        };
                    }
                    _bFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
                    break;
                case 2:
                    assembly = Assembly.GetAssembly(typeof(AccountRepositorySealed));
                    //TekTak.iLoop.Sealed
                    _displayClasses = assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.StartsWith("TekTak.iLoop.Sealed", StringComparison.OrdinalIgnoreCase) && t.IsClass && t.IsPublic).ToList();
                    _constorInfoUniversal = _constorInfo;
                    _bFlags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public;
                    break;
                case 3:
                    assembly = Assembly.GetAssembly(typeof(AccountService));
                    _displayClasses = assembly.GetTypes().Where(t => t.Namespace != null && t.IsClass && t.IsPublic).ToList();
                    if (_displayClasses.Any())
                    {
                        var uw = new DAL.DbEntity.UnitOfWork();
                        var uow = new TekTak.iLoop.UOW.UnitOfWork();
                        _constorInfoUniversal = new ConstructorInfo
                        {
                            ConstorParamType = new[] { uw.GetType(), uow.GetType() },
                            ConstorParams = new object[] { uw, uow }
                        };
                    }
                    _bFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
                    break;
            }
            _cboItems = new Dictionary<string, string>();
            foreach (var type in _displayClasses)
            {
                var display = type.Name.IndexOf("Repo", StringComparison.Ordinal) > 0
                    ? type.Name.Substring(0, type.Name.IndexOf("Repo", StringComparison.Ordinal))
                    : type.Name;
                display = display.EndsWith("Service", StringComparison.OrdinalIgnoreCase) ? display.Substring(0, display.LastIndexOf("Service", StringComparison.OrdinalIgnoreCase)) : display;
                _cboItems.Add(type.FullName, display.AddSpaceBeforeCapitalLetters());
            }
            cboClasses.DataSource = new BindingSource(_cboItems, null);
            cboClasses.DisplayMember = "Value";
            cboClasses.ValueMember = "Key";
            cboClasses.SelectedIndex = 0;
        }

        private void cboClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeMethods.Nodes.Clear();
            spltResults.Panel1.Controls.Clear();
            var key = ((KeyValuePair<string, string>)cboClasses.SelectedItem).Key;
            var currentclass = _displayClasses.FirstOrDefault(x => x.FullName.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (currentclass != null)
            {
                var methodInfos = currentclass.GetMethods(_bFlags)
                    //.Where(x => x.GetCustomAttribute(_attType) != null)
                    .Where(x => !_ignoreList.Contains(x.Name))
                    .OrderBy(x => x.Name);
                foreach (var methodInfo in methodInfos)
                {
                    var node = new TreeNode(methodInfo.Name.AddSpaceBeforeCapitalLetters());
                    node.Name = methodInfo.Name;
                    node.ToolTipText = methodInfo.GetSignature(true);
                    treeMethods.Nodes.Add(node);
                }
                grpMethods.Text = string.Format("{0} ({1})", cboClasses.SelectedItem, methodInfos.Count());
            }
        }

        private void treeMethods_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ProcessNode(e.Node);
        }

        private void treeMethods_MouseEnter(object sender, EventArgs e)
        {
            ((TreeView)sender).Focus();
        }

        private void treeMethods_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ProcessNode(e.Node);
        }

        private void btnInvoke_Click(object sender, EventArgs e)
        {
            var objects = new List<object>();
            spltResults.Panel2.Controls.Clear();
            try
            {
                if (_lst != null && _lst.Any() && treeMethods.SelectedNode != null)
                {
                    foreach (var richTextBox in _lst)
                    {
                        var str = richTextBox.Text;
                        var assembly = _assemblies.FirstOrDefault(a => a.GetType(richTextBox.Name, false) != null);
                        Object obj = null;
                        Type theType;
                        if (assembly != null && !assembly.GetName().ToString().StartsWith("mscorlib", StringComparison.OrdinalIgnoreCase))
                        {
                            theType = assembly.GetTypes().FirstOrDefault(a => a.FullName == richTextBox.Name);
                            if (theType == null)
                                TestHelper.TryGetTypeByName(richTextBox.Name, out theType, _assemblies);
                            obj = JsonConvert.DeserializeObject(str, theType);
                        }
                        else
                        {
                            if (!Convert.ToBoolean(richTextBox.AccessibleName))
                            {
                                TestHelper.TryGetTypeByName(richTextBox.Name, out theType, _assemblies);
                                if (theType != null)
                                    obj = JsonConvert.DeserializeObject(str, theType);
                            }
                            else obj = JsonConvert.DeserializeObject(str);
                        }

                        if (Convert.ToBoolean(richTextBox.AccessibleName))
                        {
                            var theValue = JToken.Parse(str).First.Value<JProperty>().Value;
                            switch (theValue.Type)
                            {
                                case JTokenType.Object:
                                    obj = theValue.ToString();
                                    break;
                                case JTokenType.Array:
                                    obj = theValue;
                                    break;
                                case JTokenType.Integer:
                                    var temp = Convert.ToInt64(theValue.ToString());
                                    if (temp <= Byte.MaxValue && temp >= Byte.MinValue)
                                        obj = Convert.ToByte(temp);
                                    else if (temp >= Int16.MinValue && temp <= Int16.MaxValue)
                                        obj = Convert.ToInt16(temp);
                                    else if (temp >= Int32.MinValue && temp <= Int32.MaxValue)
                                        obj = Convert.ToInt32(temp);
                                    else
                                        obj = temp;
                                    break;
                                case JTokenType.Float:
                                    obj = Convert.ToDouble(theValue.ToString());
                                    break;
                                case JTokenType.Boolean:
                                    obj = Convert.ToBoolean(theValue.ToString());
                                    break;
                                case JTokenType.Date:
                                    obj = Convert.ToDateTime(theValue.ToString());
                                    break;
                                case JTokenType.Guid:
                                    obj = (Guid)theValue;
                                    break;
                                case JTokenType.Null:
                                    obj = null;
                                    break;
                                default:
                                    obj = theValue.ToString();
                                    break;
                            }
                        }
                        objects.Add(obj);
                    }

                    var key = ((KeyValuePair<string, string>)cboClasses.SelectedItem).Key;
                    var currentclass = _displayClasses.FirstOrDefault(x => x.FullName.Equals(key, StringComparison.OrdinalIgnoreCase));
                    if (currentclass != null)
                    {
                        var result = Result(currentclass, treeMethods.SelectedNode.Name, objects, _constorInfoUniversal);
                        var txtResponse = new RichTextBox
                        {
                            AutoSize = true,
                            Text = JsonConvert.SerializeObject(result, Formatting.Indented),
                            ReadOnly = true,
                            Dock = DockStyle.Fill
                        };
                        spltResults.Panel2.Controls.Add(txtResponse);
                    }
                }
            }
            catch (JsonReaderException ex)
            {
                DisplayError(ex.Message);
            }
            catch (TargetInvocationException ex)
            {
                DisplayError(ex.InnerException != null ? (ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message + Environment.NewLine + ex.InnerException.InnerException.StackTrace : ex.InnerException.Message) : ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveState();
        }

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnInvoke.PerformClick();
            }
        }

        private void lblToken_Click(object sender, EventArgs e)
        {
            txtAuth.Text = LoadToken();

        }

        private void spltResults_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (_lst != null)
                foreach (var richTextBox in _lst)
                    richTextBox.Width = spltResults.Panel1.Width - 30;
        }
        #endregion

        #region Helpers
        private void LoadState()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "iLoopDesktopState.txt")))
                using (var sr = new StreamReader(Path.Combine(Application.StartupPath, "iLoopDesktopState.txt")))
                {
                    try
                    {
                        _state = JsonConvert.DeserializeObject<State>(sr.ReadToEnd());
                        cboCategories.SelectedIndex = cboCategories.Items.Count >= _state.Category ? _state.Category : 0;
                        //cboClasses.SelectedIndex = cboClasses.Items.Count >= _state.Combo ? _state.Combo : 0;
                        //treeMethods.SelectedNode = treeMethods.Nodes.Count > _state.Node ? treeMethods.Nodes[_state.Node] : treeMethods.Nodes[0];
                        //treeMethods.Focus();
                        txtAuth.Text = _state.Token;
                        WindowState = _state.IsMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
                        spltMajor.SplitterDistance = _state.SplitMajor;
                        spltResults.SplitterDistance = _state.SplitMinor;
                    }
                    catch (Exception)
                    { }
                }
        }

        private string LoadToken()
        {
            var token = string.Empty;
            var sample = (LoginRequest)_samples.FirstOrDefault(x => x.GetType().Name == "LoginRequest");
            if (sample != null)
            {
                var resp = (StatusData<LoginResponse>)Result(typeof(AccountRepositorySealed), "Login", new List<object> { sample, "" }, _constorInfo);
                token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}:{3}:{4}",
                    resp.Data.UserId,
                    sample.UserName,
                    (byte)resp.Data.UserTypeId,
                    sample.DeviceId,
                    resp.Data.Token)));
            }
            return token;
        }

        private void ReadState()
        {
            _state.Token = txtAuth.Text;
            _state.Category = cboCategories.SelectedIndex;
            _state.Combo = cboClasses.SelectedIndex;
            _state.Node = treeMethods.SelectedNode != null ? treeMethods.SelectedNode.Index : 0;
            _state.IsMaximized = WindowState == FormWindowState.Maximized;
            _state.SplitMajor = spltMajor.SplitterDistance;
            _state.SplitMinor = spltResults.SplitterDistance;
        }

        private void SaveState()
        {
            ReadState();
            using (var sw = new StreamWriter(Path.Combine(Application.StartupPath, "iLoopDesktopState.txt"), false))
                sw.Write(JsonConvert.SerializeObject(_state, Formatting.Indented));
        }

        private void ProcessNode(TreeNode e)
        {
            treeMethods.SelectedNode = e;
            var key = ((KeyValuePair<string, string>)cboClasses.SelectedItem).Key;
            var currentclass = _displayClasses.FirstOrDefault(x => x.FullName.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (currentclass != null)
            {
                var methodInfo = currentclass.GetMethods()
                    //.Where(x => x.GetCustomAttribute(_attType) != null)
                    .FirstOrDefault(x => x.Name.Equals(e.Name, StringComparison.OrdinalIgnoreCase));
                strpInfo.Text = methodInfo.GetSignature();

                strpInfo.ToolTipText = strpInfo.Text;

                var authenticationToken = txtAuth.Text;
                if (!string.IsNullOrWhiteSpace(authenticationToken) && Helper.IsBase64String(authenticationToken))
                {
                    try
                    {
                        var credential = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                        var credentials = Array.AsReadOnly(credential.Split(':'));
                        var session = new SystemSession
                        {
                            UserId = Convert.ToInt32(credentials[(int)SystemSessionEntity.UserId]),
                            UserName = credentials[(int)SystemSessionEntity.UserName],
                            UserTypeId = Convert.ToByte(credentials[(int)SystemSessionEntity.UserTypeId]),
                            DeviceId = credentials[(int)SystemSessionEntity.DeviceId],
                            LoginToken = credentials[(int)SystemSessionEntity.LoginToken]
                        };

                        var pms = methodInfo.GetParameters();
                        _lst = new List<RichTextBox>();
                        _lbl = new List<Label>();

                        foreach (var item in pms)
                        {
                            var isChanged = false;
                            var sample = _samples.FirstOrDefault(x => x.GetType() == item.ParameterType);
                            if (item.Name.Equals("Session", StringComparison.OrdinalIgnoreCase))
                                sample = session;
                            if (sample != null)
                                sample = JsonConvert.SerializeObject(sample);
                            else
                            {
                                Type genericUnderlyingType = null;
                                if (item.ParameterType.IsGenericType && item.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    var nullableConverter = new NullableConverter(item.ParameterType);
                                    genericUnderlyingType = nullableConverter.UnderlyingType;
                                    //Convert.ChangeType(value, conversionType);
                                }

                                if (item.ParameterType == typeof(string))
                                {
                                    sample = string.Format("{0} \"{1}\": \"{2}\" {3}", "{", item.Name, string.Empty, "}");
                                    isChanged = true;
                                }
                                else if (item.ParameterType.IsPrimitive || (genericUnderlyingType != null && genericUnderlyingType.IsPrimitive))
                                {
                                    var v = Activator.CreateInstance(genericUnderlyingType ?? Type.GetType(item.ParameterType.FullName));
                                    var theValue = v is bool ? v.ToString().ToLower() : v;
                                    sample = string.Format("{0} \"{1}\": {2} {3}", "{", item.Name, theValue, "}");
                                    isChanged = true;
                                }
                                else if (item.ParameterType.IsEnum)
                                {
                                    var realType = item.ParameterType;
                                    var v = Activator.CreateInstance(realType);
                                    sample = string.Format("{0} \"{1}\": {2} {3}", "{", item.Name, Convert.ToInt32(v), "}");
                                    isChanged = true;
                                }
                                else if (item.ParameterType.IsGenericType && item.ParameterType.IsAbstract && item.ParameterType.IsInterface)
                                {
                                    var genericListType = typeof(List<>);
                                    var listType = genericListType.MakeGenericType(item.ParameterType);
                                    sample = JsonConvert.SerializeObject(Activator.CreateInstance(listType));
                                }
                                else if (item.ParameterType.IsGenericType && item.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    var nullableConverter = new NullableConverter(item.ParameterType);
                                    var conversionType = nullableConverter.UnderlyingType;
                                    //Convert.ChangeType(value, conversionType);
                                }
                                else
                                {
                                    var realType = item.ParameterType.IsByRef ? item.ParameterType.GetElementType() : item.ParameterType;
                                    sample = JsonConvert.SerializeObject(Activator.CreateInstance(realType));
                                }
                            }
                            var lbl = new Label
                            {
                                //Location = new System.Drawing.Point(rtb.Width - 50, 0),
                                Size = new System.Drawing.Size(400, 20),
                                Text = item.Name,
                                Top = _lst.Any() ? _lst.Last().Top + _lst.Last().Height + 20 : 20
                            };
                            _lbl.Add(lbl);

                            var rtb = new RichTextBox
                            {
                                Text = sample != null ? JsonConvert.SerializeObject(JsonConvert.DeserializeObject<object>(sample.ToString()), Formatting.Indented) : string.Empty,
                                Width = spltResults.Panel1.Width - 30,
                                Top = _lbl.Any() ? _lbl.Last().Top + _lbl.Last().Height : 0,
                                AccessibleName = isChanged.ToString(),
                                Name = item.ParameterType.IsByRef ? item.ParameterType.GetElementType().FullName : item.ParameterType.FullName
                            };
                            var rtbSize = TextRenderer.MeasureText(rtb.Text, rtb.Font, rtb.Size, TextFormatFlags.WordBreak);
                            rtb.Height = rtbSize.Height + 20;
                            rtb.KeyDown += richTextBox_KeyDown;
                            rtb.ContentsResized += richTextBox_ContentsResized;
                            _lst.Add(rtb);
                        }
                        spltResults.Panel1.Controls.Clear();

                        for (var index = 0; index < _lst.Count; index++)
                        {
                            spltResults.Panel1.Controls.Add(_lbl[index]);
                            spltResults.Panel1.Controls.Add(_lst[index]);
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex.Message);
                    }
                }
            }
        }
        private void richTextBox_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            //richTextBox.Width = e.NewRectangle.Width;
            richTextBox.Height = e.NewRectangle.Height + 20;
        }

        private void DisplayError(string error)
        {
            var rtbException = new RichTextBox { Dock = DockStyle.Fill, Text = error, ReadOnly = true };
            spltResults.Panel2.Controls.Add(rtbException);
        }

        private object Result(Type currentclass, string methodName, List<object> objects, ConstructorInfo cInfo)
        {
            object result = null;
            var methodInfo = currentclass.GetMethods()
                //.Where(x => x.GetCustomAttribute(_attType) != null)
                .FirstOrDefault(x => x.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase));
            if (methodInfo != null)
            {
                var constr = currentclass.GetConstructor(cInfo.ConstorParamType);
                var ctorObj = constr.Invoke(cInfo.ConstorParams);

                if (methodInfo.GetCustomAttribute(_attType) != null)
                {
                    var task = (Task)TestHelper.CallMethod(ctorObj, methodInfo, objects);
                    //var task = (Task)methodInfo.Invoke(ctorObj, objects.ToArray());
                    result = task.GetType().GetProperty("Result").GetValue(task);
                }
                else
                {
                    result = TestHelper.CallMethod(ctorObj, methodInfo, objects);
                    if (result is Task)
                    {
                        var task = (Task)result;
                        result = task.GetType().GetProperty("Result").GetValue(task);
                    }
                }
            }
            return result;
        }
        #endregion
    }
}

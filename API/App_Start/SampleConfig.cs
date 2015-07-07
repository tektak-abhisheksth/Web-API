using API.Areas.HelpPage;
using API.Handlers;
using Model.Account;
using Model.Base;
using Model.Category;
using Model.Chat;
using Model.Common;
using Model.Friend;
using Model.Inbox;
using Model.Media;
using Model.Notification;
using Model.Profile;
using Model.Profile.Personal;
using Model.Search;
using Model.Setting;
using Model.Types;
using Model.WebClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Utility;
using Formatting = Newtonsoft.Json.Formatting;

namespace API
{
    /// <summary>
    /// A disposable sample configuration file (not to be moved to production).
    /// </summary>
    public static class SampleConfig
    {
        private static readonly Dictionary<string, string> CountriesList;

        private static readonly List<string> FirstNames = new List<string> { "Anwesh", "Indira", "Sudiya", "DP", "Madan", "Abhishek", "Mohan", "Nitu", "Roshan", "Janak", "Shushila", "Subhash", "Hari", "Ashok", "Prabin", "Dipak", "Deepa", "Anil", "Krishna", "Sanjaya", "Sobit" };
        private static readonly List<string> LastNames = new List<string> { "Tiwari", "Sapkota", "Pradhan", "Bhatt", "Tamang", "Pant", "Niroula", "Maharjan", "Shrestha", "Rana", "Shah", "Khatri", "Upadhaya", "Acharya", "Paudel", "Shakya", "Malla", "Magar", "Bhaila", "Karki", "Khadka" };
        private static readonly List<string> BusinessFirstName = new List<string> { "Civil", "New Baneshwor", "Trade", "TekTak", "D2HawkEye", "Arihant", "Zoom", "Kantipur", "Kathmandu", "Nepal", "JhumJhum" };
        private static readonly List<string> BusinessLastName = new List<string> { "Mall", "House", "Center", "Mills", "Information Technologies", "Technologies", "Cafe", "Hospital", "College", "Cafe and Bar", "Homes" };
        private static readonly List<string> BusinessTypes = new List<string> { "Pvt. Ltd.", "Public Ltd.", "Corps.", "and sons", "Limited" };
        private static readonly List<string> CategoryList = new List<string> { "Close Friends", "Galz", "Jom Guys!", "Khuraafati Gang" };
        private static readonly List<string> CategoryDescriptionList = new List<string> { "My Gang", "Sathi haru ko group", "My near and dear friends", "Friend's Circle" };
        private static readonly List<string> FolderNameList = new List<string> { "Office", "Private", "GirlFriend", "BoyFriend" };
        private static readonly List<string> SubjectList = new List<string> { "Leave", "Message", "Love him", "Hate you" };
        private static readonly List<string> ChatGroupList = new List<string> { "Trekking", "Hiking", "Team Building", "Meeting", "Discussion", "Project Update" };


        static SampleConfig()
        {
            CountriesList = new Dictionary<string, string>
            {
                {"NP", "Nepal"},
                {"IN","India"},
                {"NO", "Norway"},
                {"MU", "Mauritius"},
                {"MA", "Morocco"},
                {"HT", "Haiti"}
            };
        }

        #region Helpers
        public static void Register(HttpConfiguration config)
        {
            var samples = GetSamples();
            if (samples != null)
                foreach (var sample in GetSamples())
                    SetSample(config, sample);
        }

        private static void SetSample(HttpConfiguration config, object sample)
        {
            var json = JsonConvert.SerializeObject(sample, Formatting.Indented);
            var xml = CreateXml(sample);
            var sampleType = sample.GetType();
            var gen = config.GetHelpPageSampleGenerator();

            var mimeTypes = new List<MediaTypeHeaderValue>
            {
                new MediaTypeHeaderValue("application/json"),
                new MediaTypeHeaderValue("text/json"),
                new MediaTypeHeaderValue("text/html"),
                new MediaTypeHeaderValue("text/plain")
            };

            foreach (var mediaType in mimeTypes)
            {
                var smpl = gen.ActionSamples.FirstOrDefault(x =>
                    Equals(x.Key, new HelpPageSampleKey(mediaType, sampleType)));
                if (smpl.Value != null)
                    gen.ActionSamples[smpl.Key] = json;
                else config.SetSampleForType(json, mediaType, sampleType);
            }

            mimeTypes = new List<MediaTypeHeaderValue>
            {
                new MediaTypeHeaderValue("text/xml"),
                new MediaTypeHeaderValue("application/xml")
            };
            foreach (var mediaType in mimeTypes)
            {
                var smpl = gen.ActionSamples.FirstOrDefault(x =>
                    Equals(x.Key, new HelpPageSampleKey(mediaType, sampleType)));
                if (smpl.Value != null)
                    gen.ActionSamples[smpl.Key] = xml;
                else config.SetSampleForType(xml, mediaType, sampleType);
            }
        }

        private static string CreateXml(Object sample)
        {
            var xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            var xmlSerializer = new XmlSerializer(sample.GetType());
            // Creates a stream whose backing store is memory. 
            using (var xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, sample);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                //return xmlDoc.InnerXml;
                var xml = XDocument.Parse(xmlDoc.InnerXml);
                return xml.ToString();
            }
        }

        private static string GenerateRandomNames(params List<string>[] lst)
        {
            var sb = new StringBuilder();
            foreach (var t in lst)
                sb.Append(t[Helper.GenerateRandomCode(0, t.Count)]).Append(" ");
            return sb.ToString().Trim();
        }
        #endregion

        public static IEnumerable<object> GetSamples()
        {
            var samples = new List<object>();
            var randDay = DateTimeOffset.Now.AddDays(Helper.GenerateRandomCode(0, 7));
            var holder = new Dictionary<string, string>
            {
                {"CC", GenerateRandomNames(CountriesList.Keys.ToList())},
                {"FN", GenerateRandomNames(FirstNames, LastNames)},
                {"BN", GenerateRandomNames(BusinessFirstName, BusinessLastName, BusinessTypes)},
                {"CN", GenerateRandomNames(CategoryList)},
                {"CD", GenerateRandomNames(CategoryDescriptionList)},
                {"IN", GenerateRandomNames(FolderNameList)},
                {"SL", GenerateRandomNames(SubjectList)},
                {"CG", GenerateRandomNames(ChatGroupList)}
            };

            Trace latestUserInfo = null;
            if (MessageHandler.TraceList != null && MessageHandler.TraceList.Any())
                latestUserInfo = MessageHandler.TraceList.LastOrDefault(x => !string.IsNullOrWhiteSpace(x.Status) && x.Status.StartsWith("2") && !string.IsNullOrWhiteSpace(x.DeviceTypeId) && !x.DeviceTypeId.Equals("W", StringComparison.OrdinalIgnoreCase));
            if (latestUserInfo == null)
                latestUserInfo = new Trace { Token = "he9s3Glf1422849654974", UserId = 1, UserName = "indira", UserTypeId = 1, DeviceId = Helper.GenerateRandomCode(1000000000, long.MaxValue).ToString(CultureInfo.InvariantCulture) };

            samples.Add(new RequestBase { DeviceId = latestUserInfo.DeviceId, UserId = latestUserInfo.UserId });

            samples.Add(new SignUpRequestPerson { CountryCode = holder["CC"], DeviceId = latestUserInfo.DeviceId, FirstName = holder["FN"].Split(' ')[0], LastName = holder["FN"].Split(' ')[1], Email = string.Concat(holder["FN"].Replace(" ", string.Empty), "@tektak.com"), DateOfBirth = randDay.AddYears(-Helper.GenerateRandomCode(10, 70)).Date, Gender = (SystemGender)Helper.GenerateRandomCode(1, 2), Password = "tektak", UserName = holder["FN"].Split(' ')[0] + Helper.GenerateRandomCode(1, 1000), BluetoothId = Helper.GenerateRandomCode(111111111111, 999999999999).ToString(), ModelName = "Nokia-" + Guid.NewGuid().ToString().Substring(0, 8) });

            samples.Add(new SignUpRequestBusiness { CountryCode = holder["CC"], DeviceId = latestUserInfo.DeviceId, CompanyName = holder["BN"].Split(' ')[0], Email = string.Concat(holder["FN"].Replace(" ", string.Empty), "@tektak.com"), Password = "tektak", UserName = holder["BN"].Split(' ')[0] + Helper.GenerateRandomCode(1, 1000), BluetoothId = Helper.GenerateRandomCode(111111111111, 999999999999).ToString(), ModelName = "Nokia-" + Guid.NewGuid().ToString().Substring(0, 8), CompanyType = 36, CompanyUrl = "tektak.com.np", EstablishedDate = randDay.AddYears(-Helper.GenerateRandomCode(10, 70)).Date, OwnershipTypeId = 3 });

            samples.Add(new FriendshipRequest { DeviceId = latestUserInfo.DeviceId, UserId = latestUserInfo.UserId, FriendId = "indira" });

            samples.Add(new FriendRespondRequest { DeviceId = latestUserInfo.DeviceId, UserId = latestUserInfo.UserId, FriendId = "indira", Accept = Helper.GenerateRandomCode(0, 2) <= 1, CategoryId = 98 });

            samples.Add(new FriendRequest { DeviceId = latestUserInfo.DeviceId, UserId = latestUserInfo.UserId, FriendId = "sudiya" });

            samples.Add(new LoginRequest { UserName = "indira", Password = "tektak", DeviceId = latestUserInfo.DeviceId, BluetoothId = Helper.GenerateRandomCode(111111111111, 999999999999).ToString(), ModelName = "Nokia-" + Guid.NewGuid().ToString().Substring(0, 8), DeviceType = "I", PushCode = "ta412ob61417503289665" });

            samples.Add(new PhoneBookContactsRequest
            {
                UserId = latestUserInfo.UserId,//user.UserID,
                DeviceId = latestUserInfo.DeviceId,
                CTag = Guid.NewGuid().ToString(),
                Add = new List<PhoneBookContact>{
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }, 
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }, 
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }, 
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }, 
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }},
                Delete = new List<PhoneBookContact>{
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }, 
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }, 
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }, 
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }, 
                                 new PhoneBookContact { CountryCode = GenerateRandomNames(CountriesList.Keys.ToList()), MobileNumber = Helper.GenerateRandomCode(9841000000, 9849999999999) }}
            });

            samples.Add(new CategoryRequest { DeviceId = latestUserInfo.DeviceId, Name = holder["CN"], Description = holder["CD"], UserId = latestUserInfo.UserId });

            samples.Add(new CategoryAddRequest { DeviceId = latestUserInfo.DeviceId, Name = holder["CN"], Description = holder["CD"], UserId = latestUserInfo.UserId, Friends = new List<int> { 2, 2157, 2166, 2180, 2181 } });

            samples.Add(new CategoryUpdateRequest { DeviceId = latestUserInfo.DeviceId, Name = holder["CN"], Description = holder["CD"], UserId = latestUserInfo.UserId, CategoryId = 5 });

            samples.Add(new CategoryFriends { DeviceId = latestUserInfo.DeviceId, UserId = latestUserInfo.UserId, Friends = new List<int> { 2, 2157, 2166, 2180, 2181 }, CategoryId = 16 });

            samples.Add(new CopyCategory { DeviceId = latestUserInfo.DeviceId, UserId = latestUserInfo.UserId, Friends = new List<int> { 2, 2157, 2166, 2180, 2181 }, CategoryId = 16, TargetCategories = new List<int> { 10, 17, 32, 36, 42 } });

            samples.Add(new ActiveDeviceRequest { DeviceId = latestUserInfo.DeviceId, UserId = latestUserInfo.UserId, DeviceIds = new List<string> { "5917107813877178368", "3143932933767546368", "C048039B-A352-481F-B1E9-095D1E60D545", "AFE01F86-B0BF-412F-8F02-86C2B4F58687", "BD9B50FB-A676-43C8-9BA1-EC818D4DA367" } });

            samples.Add(new UserSettingRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                SettingTypeId = (int)SystemSettingPerson.ReceiveMessages,
                Entries = new List<int> { 1, 83, 2157, 2158, 2166 },
                SettingGroupId = 2,
                Value = 11 //SystemSettingValue.Yes
            });

            samples.Add(new ChangePasswordRequest { UserId = latestUserInfo.UserId, Password = "tektak", NewPassword = "tektak1", DeviceId = latestUserInfo.DeviceId });

            samples.Add(new ForgotPasswordRequest { UserName = holder["FN"].Split(' ')[0] + Helper.GenerateRandomCode(1, 1000), DeviceId = latestUserInfo.DeviceId });

            samples.Add(new DeleteCategory { DeviceId = latestUserInfo.DeviceId, UserId = latestUserInfo.UserId, CategoryList = new List<int> { 10, 17, 32, 36, 42 } });

            samples.Add(new InboxRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Name = holder["IN"],
                Rule = new List<InboxRuleRequest>
                {
                    new InboxRuleRequest
                    {
                        UserSelection = SystemUserSelection.Contacts,
                        RuleTypeUser = SystemRuleTypeUser.Any,
                        ContactList = new List<string> {"indira", "sudiya"},
                        GroupList = new List<string>{"6aqv3owi1415643407089",
                                "hRou9Ge21415478049028"},
                        RuleTypeSubject = SystemRuleTypeSubject.Matches,
                        Subject = holder["SL"],
                        ApplyOnOldMessage = true
                    },
                    new InboxRuleRequest
                    {
                        UserSelection = SystemUserSelection.Groups,
                        RuleTypeUser = SystemRuleTypeUser.Matches,
                        GroupList =
                            new List<string>
                            {
                               "6aqv3owi1415643407089",
                                "hRou9Ge21415478049028"
                            }
                    }
                }
            });

            samples.Add(new InboxMuteRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                FolderList = new List<int> { 10, 11, 12, 13, 14, 15 },
                Mute = false
            });

            samples.Add(new RuleAddRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                FolderId = 1,
                Rule = new InboxRuleRequest
                {
                    UserSelection = SystemUserSelection.Contacts,
                    RuleTypeUser = SystemRuleTypeUser.Contains,
                    ContactList = new List<string> { "indira", "sudiya" },
                    GroupList = new List<string>{"6aqv3owi1415643407089",
                                "hRou9Ge21415478049028"},
                    RuleTypeSubject = SystemRuleTypeSubject.Matches,
                    Subject = holder["SL"],
                    ApplyOnOldMessage = true
                }
            });
            samples.Add(new RuleUpdateRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                MessageRuleId = 1,
                Rule = new InboxRuleRequest
                {
                    UserSelection = SystemUserSelection.Groups,
                    RuleTypeUser = SystemRuleTypeUser.Contains,
                    GroupList = new List<string> { "6aqv3owi1415643407089", "hRou9Ge21415478049028" }
                }
            });

            samples.Add(new SingleData<List<GeneralKvPair<string, string>>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new List<GeneralKvPair<string, string>>
                {
                    new GeneralKvPair<string, string> {Id = "DeviceID1", Value = "Token1"},
                    new GeneralKvPair<string, string>{Id = "DeviceID2", Value = "Token2"}
                }
            });

            samples.Add(new SingleData<GeneralKvPair<long, List<long>>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new GeneralKvPair<long, List<long>> { Id = 1, Value = new List<long> { 1, 2, 3, 4 } }
            });

            samples.Add(new SingleData<GeneralKvPair<long, List<int>>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new GeneralKvPair<long, List<int>> { Id = 1, Value = new List<int> { 1, 2, 3, 4 } }
            });

            samples.Add(new SingleData<List<string>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = FirstNames.OrderBy(x => Guid.NewGuid()).Where(x => x.Length >= 6).Take(Helper.GenerateRandomCode(1, 10)).ToList()
            });

            samples.Add(new SingleData<string>
                        {
                            DeviceId = latestUserInfo.DeviceId,
                            UserId = latestUserInfo.UserId,
                            Data = holder["IN"]
                        });

            samples.Add(new SingleData<int>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = Helper.GenerateRandomCode(1, 1000)
            });

            samples.Add(new SingleData<long>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = Helper.GenerateRandomCode(1, long.MaxValue)
            });

            samples.Add(new SingleData<List<int>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new List<int> { 1, 2 }
            });

            samples.Add(new SingleDataAnonymous<string>
            {
                DeviceId = latestUserInfo.DeviceId,
                Data = FirstNames.OrderBy(x => Guid.NewGuid()).FirstOrDefault()
            });


            samples.Add(new SignOut
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Token = latestUserInfo.Token
            });

            samples.Add(new ChatRequest.MessageRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                PageSize = 10,
                DrillUp = null,
                DrillDown = null,
                Limit = null,
                InstanceId = "b93htf8a1414754593172",
                LastMessage = null,
                Cursor = null,
                UnReadMessage = true
            });

            samples.Add(new ChatRequest.InstanceRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                PageSize = 10,
                DrillUp = null,
                DrillDown = null,
                Limit = null,
                IncludeMessage = true,
                Cursor = null,
                //InstanceList = new List<string> { "o8di9f6m1415179223674", "o8di9f6m1415179223674", "tJ18vaG21415179674642", "qmez6ox01415088783825", "8tzsJy5i1415091403362" },
                FolderId = "0"
            });

            samples.Add(new ChatRequest.BlockUsersRequest
             {
                 DeviceId = latestUserInfo.DeviceId,
                 UserId = latestUserInfo.UserId,
                 InstanceId = "b3Cfskuo1420169872845",
                 Users = new List<string> { "sudiya", "roshan", "emlie123", "abhishek", "evagreen" }
             });

            samples.Add(new ChatRequest.ChatGroupRequest
             {
                 DeviceId = latestUserInfo.DeviceId,
                 UserId = latestUserInfo.UserId,
                 CreatedDate = 1415091642069,
                 GroupName = holder["CG"],
                 Members = new List<string> { "sudiya", "indira", "emlie123", "abhishek", "evagreen" },
                 //GroupSettings = new List<KvPair<string, string>> { new KvPair<string, string>("SettingKey", "SettingValue") }
                 GroupS = new List<KvPair<string, bool>> { new KvPair<string, bool> { K = "allowothers", V = true }, new KvPair<string, bool> { K = "autoaccept", V = false } }
             });

            samples.Add(new ChatRequest.GroupSettingRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                GroupId = "590c2350a3f44b3199318990ef6080c2",
                //GroupSettings = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("SettingKey", "SettingValue") }
                GroupS = new List<KvPair<string, bool>> { new KvPair<string, bool> { K = "allowothers", V = true }, new KvPair<string, bool> { K = "autoaccept", V = false } }
            });

            samples.Add(new ChatRequest.MemberRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                GroupId = "590c2350a3f44b3199318990ef6080c2",
                Users = new List<string> { "sudiya", "indira", "emlie123", "abhishek", "evagreen" }
            });

            samples.Add(new ChatRequest.ChatGroupPullRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                PageSize = 10,
                DrillUp = null,
                DrillDown = null,
                Limit = null,
                IncludeInstance = true,
                Cursor = null
            });

            samples.Add(new ChatRequest.GearUpRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                InstanceList = new List<string> { "o8di9f6m1415179223674", "o8di9f6m1415179223674", "tJ18vaG21415179674642", "qmez6ox01415088783825", "8tzsJy5i1415091403362" },
                TimeStamp = 1415091403363
            });

            samples.Add(new ChatRequest.ApproveRejectGroupRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Mar = new List<ChatRequest.MemberApproveRejectRequest>
                {
                    new ChatRequest.MemberApproveRejectRequest{RType = 'a', UserId = "indira"},
                    new ChatRequest.MemberApproveRejectRequest{RType = 'r', UserId = "sudiya"}
                },
                GroupId = "6aqv3owi1415643407089"
            });

            samples.Add(new ChatRequest.InstanceOperationRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                InstanceList = new List<string> { "o8di9f6m1415179223674", "o8di9f6m1415179223654", "tJ18vaG21415179674642", "qmez6ox01415088783825", "8tzsJy5i1415091403362" },
                Mode = SystemInstanceOperation.Delete
            });

            samples.Add(new ChatRequest.MessageDeleteRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                MessageList = new List<string> { "1420457385107", "1420456740605", "1420455103843", "1420455924458", "1420455103843" },
                InstanceId = "o8di9f6m1415179223674"
            });

            samples.Add(new ImageRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                FileId = "is3h58m91419330877695.jpg",
                AskWebp = true,
                UserName = null,
                IsProfilePicture = true,
                SizeCode = SystemImageSizeCode.FiftyFifty
            });

            samples.Add(new ImageCropRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                FileId = "is3h58m91419330877695.jpg",
                AskWebp = true,
                UserName = null,
                CropDetail = new ImageCropDetailRequest { Bottom = 50, Height = 60, Left = 40, Right = 40, SizeCode = SystemImageSizeCode.FiftyFifty, Top = 80, Width = 80 }
            });

            samples.Add(new PaginatedRequest<NotificationRequest>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new NotificationRequest
                {
                    FilterType = 0,
                    RequestDirection = 1,
                    NotificationRequestId = 0,
                    NotificationRequestTypes = new List<int> { 1, 5, 6 }
                },
                PageIndex = 0,
                PageSize = 10
            });

            samples.Add(new PaginatedRequest<GeneralKvPair<SystemProfileViewType, int>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new GeneralKvPair<SystemProfileViewType, int> { Id = SystemProfileViewType.Direct, Value = 0 },
                PageIndex = 0,
                PageSize = 5
            });

            samples.Add(new SingleData<GeneralKvPair<string, string>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new GeneralKvPair<string, string> { Id = "Id", Value = "Value" }
            });

            samples.Add(new SingleData<GeneralKvPair<int, string>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new GeneralKvPair<int, string> { Id = 6, Value = "Value" }
            });

            samples.Add(new SingleData<List<GeneralKvPair<int, long>>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new List<GeneralKvPair<int, long>> { new GeneralKvPair<int, long> { Id = 83, Value = 1429772265170 }, new GeneralKvPair<int, long> { Id = 2180, Value = 1429856055123 } }
            });

            samples.Add(new ChatRequest.MoveToInboxRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                FromFolderId = "2",
                InstanceList = new List<string> { "o8di9f6m1415179223654", "tJ18vaG21415179674642" },
                ToFolderId = "16"
            });

            samples.Add(new UserLocationRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Latitude = 27.7288254664252,
                Longitude = 85.3468425705338,
                Offset = DateTimeOffset.Now
            });

            samples.Add(new ChatRequest.MessageInformationRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                InstanceId = "9vy5Gfoa1423106394045",
                MessageId = "1423545071181"
            });

            samples.Add(new ChatRequest.DisposableInstanceRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                InstanceId = "9vy5Gfoa1423106394045",
                ViewCount = 2,
                ViewTimeLimit = 16
            });

            samples.Add(new ContactRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Address = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First(),
                Email = string.Concat(holder["FN"].Replace(" ", string.Empty), "@tektak.com"),
                Fax = "01-4123475",
                Mobile = "9841111111",
                Phone = "01-4221286",
                Website = "www.tektaknepal.com",
                TemporaryContact = new TemporaryContactRequest { CompanyUserId = 2369, StartDate = DateTime.UtcNow.Date, EndDate = randDay.UtcDateTime.Date },
                ChatNetworks = new List<ChatNetworkRequest> { new ChatNetworkRequest { ChatNetworkId = SystemContactMarkedType.GoogleTalk, Value = "my.gmail.id" }, new ChatNetworkRequest { ChatNetworkId = SystemContactMarkedType.Skype, Value = "my.skype.id" } },
                CustomContacts = new List<CustomContactRequest> { new CustomContactRequest { Name = string.Concat(GenerateRandomNames(FirstNames, LastNames), "'s " + BusinessLastName.OrderBy(x => Guid.NewGuid()).First()), Value = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First() }, new CustomContactRequest { Name = string.Concat(GenerateRandomNames(FirstNames, LastNames), "'s " + BusinessLastName.OrderBy(x => Guid.NewGuid()).First()), Value = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First() } }
            });

            samples.Add(new UpdateContactRequest
            {
                ContactId = 1,
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Address = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First(),
                Email = string.Concat(holder["FN"].Replace(" ", string.Empty), "@tektak.com"),
                Fax = "01-4345678",
                Mobile = "9841234567",
                Phone = "01-4221287",
                Website = "www.tektak.co.au",
                TemporaryContact = new TemporaryContactRequest { CompanyUserId = 2369, StartDate = DateTime.UtcNow.Date, EndDate = randDay.UtcDateTime.Date },
                CustomContacts = new List<CustomUpsertContact> { new CustomUpsertContact { ContactCustomId = 56, FieldType = SystemContactMarkedType.Mobile, Value = "9841567894" } },
                ChatNetworks = new List<CustomUpsertContactChatNetwork> { new CustomUpsertContactChatNetwork { ChatNetworkId = SystemContactMarkedType.GoogleTalk, Value = "my.new.id@tektak.com", ContactChatNetworkId = null } }
            });

            samples.Add(new UpdateUserChatNetworkRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                ContactId = 1,
                ChatNetworks = new List<UpdateChatNetworkRequest> { new UpdateChatNetworkRequest { ChatNetworkId = SystemContactMarkedType.GoogleTalk, ContactChatNetworkId = 1, Value = "my.gmail.id" } }
            });

            samples.Add(new UpdateUserCustomContactRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                ContactId = 1,
                CustomContacts = new List<UpdateCustomContactRequest> { new UpdateCustomContactRequest { ContactCustomId = 1, Name = string.Concat(GenerateRandomNames(FirstNames, LastNames), "'s " + BusinessLastName.OrderBy(x => Guid.NewGuid()).First()), Value = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First() } }
            });

            samples.Add(new MetaChatNetworkRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                ContactId = 1,
                ChatNetworks = new List<ChatNetworkRequest> { new ChatNetworkRequest { ChatNetworkId = SystemContactMarkedType.GoogleTalk, Value = "my.gmail.id" } }
            });

            samples.Add(new MetaCustomContactNetwork
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                ContactId = 1
                //CustomContacts = new List<CustomContactRequest> { new CustomContactRequest { Name = string.Concat(GenerateRandomNames(FirstNames, LastNames), "'s " + BusinessLastName.OrderBy(x => Guid.NewGuid()).First()), Value = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First() } }
            });

            samples.Add(new AddSuggestContactRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                TargetUserId = 2,
                Address = null,
                Email = null,
                Fax = null,
                Mobile = null,
                Phone = null,
                Website = null,
                ContactId = 1,
                ChatNetworks = new List<AddSuggestedChatNetworkRequest> { new AddSuggestedChatNetworkRequest { ChatNetworkId = SystemContactMarkedType.GoogleTalk, Value = "my.gmail.id" }, new AddSuggestedChatNetworkRequest { ChatNetworkId = SystemContactMarkedType.Skype, Value = "my.skype.id" } },
                CustomContacts = new List<CustomContactRequest> { new CustomContactRequest { Name = string.Concat(GenerateRandomNames(FirstNames, LastNames), "'s " + BusinessLastName.OrderBy(x => Guid.NewGuid()).First()), Value = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First() }, new CustomContactRequest { Name = string.Concat(GenerateRandomNames(FirstNames, LastNames), "'s " + BusinessLastName.OrderBy(x => Guid.NewGuid()).First()), Value = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First() } }
            });

            samples.Add(new UpdateSuggestContactRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                TargetUserId = 2,
                Address = null,
                Email = null,
                Fax = null,
                Mobile = null,
                Phone = null,
                Website = null,
                ContactId = 1,
                ChatNetworks = new List<UpdateChatNetworkRequest> { new UpdateChatNetworkRequest { ChatNetworkId = SystemContactMarkedType.GoogleTalk, Value = "my.gmail.id" } },
                CustomContacts = new List<UpdateCustomContactRequest> { new UpdateCustomContactRequest { Name = string.Concat(GenerateRandomNames(FirstNames, LastNames), "'s " + BusinessLastName.OrderBy(x => Guid.NewGuid()).First()), Value = BusinessFirstName.OrderBy(x => Guid.NewGuid()).First() } }
            });

            samples.Add(new SuggestedContactOperationRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                ContactCustomId = 4,
                Mode = 1
            });

            samples.Add(new BasicContactPersonRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                FirstName = "Indira",
                LastName = "Sapkota",
                Title = null
            });

            samples.Add(new ChatRequest.SearchRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Query = "Hello",
                InstanceId = "zy9soGwx1426683290497",
                Start = 0,
                Limit = 4
            });

            samples.Add(new ContactSettingCategoriesRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                ContactId = 1,
                ContactCustomId = 69
            });

            samples.Add(new ContactSettingRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                ContactId = 1,
                ContactCustomId = 2532,
                FieldType = SystemContactMarkedType.Address,
                ContactChatNetworkId = 0,
                Value = 4,
                Entries = new List<int> { 77, 78, 79, 80, 81 }
            });

            samples.Add(new DataRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                Id = null,
                SearchString = null
            });

            samples.Add(new StatusSetRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                StatusTypeId = SystemUserAvailabilityCode.OutOfOffice,
                OutOfOfficeRequest = new OutOfOfficeRequest
                {
                    BeginDateAndTime = new DateAndTime
                    {
                        Date = randDay.UtcDateTime.Date,
                        Time = randDay.UtcDateTime.TimeOfDay
                    },
                    EndDateAndTime = new DateAndTime
                    {
                        Date = randDay.UtcDateTime.Date.AddDays(Helper.GenerateRandomCode(3, 10)),
                        Time = DateTime.UtcNow.TimeOfDay
                    },
                    Location = "Sydney",
                    AssigneeUserId = latestUserInfo.UserId,
                    ReceptionMode = (SystemStatusReceptionMode)SystemUserAvailabilityCode.Available
                }
            });

            samples.Add(new UserSearchRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Query = latestUserInfo.UserName,
                Cursor = null,
                Limit = 0
            });

            samples.Add(new SignalViewRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                TargetUser = latestUserInfo.UserName,
                GroupId = null,
                ViewType = SystemProfileViewType.Direct,
                TypeId = 1
            });

            #region Web-only

            samples.Add(new BasicContactPersonWebRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                FirstName = "Indira",
                LastName = "Sapkota",
                BirthDate = randDay.AddYears(-Helper.GenerateRandomCode(10, 70)).Date,
                Gender = SystemGender.Female,
                Interests = null,
                Nationality = "Nepali",
                RelationshipStatusId = SystemRelationshipStatus.Single,
                Religion = "Hindu",
            });

            samples.Add(new AddAcademicRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                AcademicInstitute = "Khwopa Engineering College",
                CityId = 358818,
                Concentration = "Computer Information Systems",
                GraduatedYear = 2009,
                HasGraduated = true,
                JoinedYear = 2005
            });

            samples.Add(new UpdateAcademicRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                AcademicId = 11,
                AcademicInstitute = "University of Australia",
                CityId = 1818798,
                Concentration = "Computer Information Systems",
                GraduatedYear = null,
                HasGraduated = true,
                JoinedYear = 2016
            });

            samples.Add(new AboutRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                About = "Today is a good day to die!"
            });

            samples.Add(new SingleData<List<LanguageRequest>>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new List<LanguageRequest> { new LanguageRequest { Language = "English", ProficiencyId = SystemProficiencyType.FullProfessionalProficiency }, new LanguageRequest { Language = "Nepali", ProficiencyId = SystemProficiencyType.NativeOrBilingualProficiency } }
            });

            samples.Add(new AddAwardAndHonorRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Title = "Best spokesperson",
                Date = randDay.AddYears(-Helper.GenerateRandomCode(10, 70)).Date,
                Description = "Forward in public speaking.",
                Issuer = "Intelligent provider"
            });

            samples.Add(new UpdateAwardAndHonorRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Title = "Best lato award",
                Date = randDay.AddYears(-Helper.GenerateRandomCode(10, 70)).Date,
                Description = "Didn't speak a thing.",
                Issuer = "Lothyangro",
                AwardAndHonorId = 2
            });

            samples.Add(new SkillRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Skills = new List<string> { "Web API", "XML" }
            });

            samples.Add(new SkillSuggestionRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Skills = new List<string> { "PHP", "LAMP" },
                Suggestor = "prabin"
            });

            samples.Add(new PaginatedRequest<string>
            {
                Data = "prabin",
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                PageSize = 25,
                PageIndex = 0
            });

            samples.Add(new PaginatedRequest<int>
            {
                Data = 2,
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                PageSize = 25,
                PageIndex = 0
            });

            samples.Add(new SkillAcceptanceRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                IsAccepted = true,
                Skills = new List<string> { "Web API" },
                Suggestor = "prabin"
            });

            samples.Add(new ThumbsForSkillRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                SkillTypeId = 1,
                ProfileUser = "prabin",
                ThumbsUporDown = 1
            });

            samples.Add(new PaginatedRequest<ThumbsForSkillDetailRequest>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new ThumbsForSkillDetailRequest { SkillTypeId = 65, TargetUser = "prabin" },
                PageSize = 10,
                PageIndex = 0
            });

            samples.Add(new AddEmployeeRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                CityId = 1350884,
                CompanyId = null,
                CompanyName = "TekTak Nepal",
                EmployeeTypeId = SystemEmployeeType.FullTimer,
                StartDate = randDay.UtcDateTime.Date,
                EndDate = randDay.UtcDateTime.Date.AddDays(Helper.GenerateRandomCode(100, 1000)),
                Position = "Software Engineer",
                WorkSchedule = new EmployeeWorkSchedule
                {
                    Schedules = new List<WorkSchedule>{
                new WorkSchedule { Day = SystemDayOfWeek.Monday, StartTime = new TimeSpan(0, 9, 0, 0), EndTime = new TimeSpan(0, 18, 0, 0) },new WorkSchedule { Day = SystemDayOfWeek.Tuesday, StartTime = new TimeSpan(0, 9, 0, 0), EndTime = new TimeSpan(0, 18, 0, 0) },new WorkSchedule { Day = SystemDayOfWeek.Wednesday, StartTime = new TimeSpan(0, 9, 0, 0), EndTime = new TimeSpan(0, 18, 0, 0) }, new WorkSchedule { Day = SystemDayOfWeek.Thursday, StartTime = new TimeSpan(0, 9, 0, 0), EndTime = new TimeSpan(0, 18, 0, 0) },new WorkSchedule { Day = SystemDayOfWeek.Friday, StartTime = new TimeSpan(0, 9, 0, 0), EndTime = new TimeSpan(0, 18, 0, 0) }},
                    ScheduleType = SystemWorkSchedule.Fixed
                }
            });

            samples.Add(new UpdateEmployeeRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                CityId = 1350884,
                CompanyName = "TekTak Nepal Pvt. Ltd.",
                EmployeeTypeId = SystemEmployeeType.PartTimer,
                Position = "Senior Software Engineer",
                StartDate = randDay.UtcDateTime.Date,
                EndDate = randDay.UtcDateTime.Date.AddDays(Helper.GenerateRandomCode(100, 1000)),
                PersonEmploymentId = 16,
                WorkSchedule = new EmployeeWorkSchedule
                {
                    Schedules = new List<WorkSchedule>{
                new WorkSchedule { Day = SystemDayOfWeek.Monday, StartTime = new TimeSpan(0, 9, 0, 0), EndTime = new TimeSpan(0, 18, 0, 0) }, new WorkSchedule { Day = SystemDayOfWeek.Wednesday, StartTime = new TimeSpan(0, 11, 0, 0), EndTime = new TimeSpan(0, 18, 0, 0) },new WorkSchedule { Day = SystemDayOfWeek.Friday, StartTime = new TimeSpan(0, 9, 0, 0), EndTime = new TimeSpan(0, 14, 0, 0) }},
                    ScheduleType = SystemWorkSchedule.Flexible
                }
            });

            samples.Add(new ActivateUser
            {
                DeviceId = latestUserInfo.DeviceId,
                UserName = latestUserInfo.UserName,
                UserGuid = Guid.NewGuid().ToString()
            });

            samples.Add(new ResetPasswordRequest
            {
                DeviceId = latestUserInfo.DeviceId,
                UserName = latestUserInfo.UserName,
                RequestCode = Guid.NewGuid().ToString(),
                Password = "tektak" + Helper.GenerateRandomCode(1, 1000)
            });

            samples.Add(new PaginatedRequest<FriendsInCategoryRequest>
            {
                DeviceId = latestUserInfo.DeviceId,
                UserId = latestUserInfo.UserId,
                Data = new FriendsInCategoryRequest { CategoryId = 16, InvertCategorySearch = true },
                PageSize = 10,
                PageIndex = 0
            });
            #endregion

            return samples;
        }
    }
}

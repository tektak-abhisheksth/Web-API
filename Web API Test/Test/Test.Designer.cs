namespace Test
{
    partial class Test
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test));
            this.statusInfo = new System.Windows.Forms.StatusStrip();
            this.strpInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.cboClasses = new System.Windows.Forms.ComboBox();
            this.lblClasses = new System.Windows.Forms.Label();
            this.txtAuth = new System.Windows.Forms.TextBox();
            this.btnInvoke = new System.Windows.Forms.Button();
            this.lblToken = new System.Windows.Forms.Label();
            this.ttipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.lblCategories = new System.Windows.Forms.Label();
            this.cboCategories = new System.Windows.Forms.ComboBox();
            this.spltMajor = new System.Windows.Forms.SplitContainer();
            this.grpMethods = new System.Windows.Forms.GroupBox();
            this.treeMethods = new System.Windows.Forms.TreeView();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.spltResults = new System.Windows.Forms.SplitContainer();
            this.statusInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltMajor)).BeginInit();
            this.spltMajor.Panel1.SuspendLayout();
            this.spltMajor.Panel2.SuspendLayout();
            this.spltMajor.SuspendLayout();
            this.grpMethods.SuspendLayout();
            this.grpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltResults)).BeginInit();
            this.spltResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusInfo
            // 
            this.statusInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.strpInfo});
            this.statusInfo.Location = new System.Drawing.Point(0, 472);
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusInfo.Size = new System.Drawing.Size(790, 22);
            this.statusInfo.SizingGrip = false;
            this.statusInfo.TabIndex = 1;
            this.statusInfo.Text = "InfoStrip";
            // 
            // strpInfo
            // 
            this.strpInfo.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.strpInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.strpInfo.Name = "strpInfo";
            this.strpInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // cboClasses
            // 
            this.cboClasses.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClasses.FormattingEnabled = true;
            this.cboClasses.Location = new System.Drawing.Point(199, 8);
            this.cboClasses.Name = "cboClasses";
            this.cboClasses.Size = new System.Drawing.Size(150, 21);
            this.cboClasses.Sorted = true;
            this.cboClasses.TabIndex = 1;
            this.ttipInfo.SetToolTip(this.cboClasses, "List of available classes.");
            this.cboClasses.SelectedIndexChanged += new System.EventHandler(this.cboClasses_SelectedIndexChanged);
            // 
            // lblClasses
            // 
            this.lblClasses.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblClasses.AutoSize = true;
            this.lblClasses.Location = new System.Drawing.Point(147, 12);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(43, 13);
            this.lblClasses.TabIndex = 0;
            this.lblClasses.Text = "Classes";
            this.ttipInfo.SetToolTip(this.lblClasses, "List of available classes.");
            // 
            // txtAuth
            // 
            this.txtAuth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtAuth.Location = new System.Drawing.Point(405, 8);
            this.txtAuth.Name = "txtAuth";
            this.txtAuth.Size = new System.Drawing.Size(297, 20);
            this.txtAuth.TabIndex = 2;
            this.ttipInfo.SetToolTip(this.txtAuth, "Current token.");
            // 
            // btnInvoke
            // 
            this.btnInvoke.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnInvoke.Location = new System.Drawing.Point(711, 7);
            this.btnInvoke.Name = "btnInvoke";
            this.btnInvoke.Size = new System.Drawing.Size(75, 23);
            this.btnInvoke.TabIndex = 0;
            this.btnInvoke.Text = "&Execute";
            this.ttipInfo.SetToolTip(this.btnInvoke, "Click to execute the method (Alt+E).");
            this.btnInvoke.UseVisualStyleBackColor = true;
            this.btnInvoke.Click += new System.EventHandler(this.btnInvoke_Click);
            // 
            // lblToken
            // 
            this.lblToken.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblToken.AutoSize = true;
            this.lblToken.Location = new System.Drawing.Point(358, 12);
            this.lblToken.Name = "lblToken";
            this.lblToken.Size = new System.Drawing.Size(38, 13);
            this.lblToken.TabIndex = 3;
            this.lblToken.Text = "Token";
            this.ttipInfo.SetToolTip(this.lblToken, "Click here to generate a new token.");
            this.lblToken.Click += new System.EventHandler(this.lblToken_Click);
            // 
            // ttipInfo
            // 
            this.ttipInfo.AutoPopDelay = 5000;
            this.ttipInfo.InitialDelay = 100;
            this.ttipInfo.IsBalloon = true;
            this.ttipInfo.ReshowDelay = 100;
            this.ttipInfo.ToolTipTitle = "Web API Information";
            // 
            // lblCategories
            // 
            this.lblCategories.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCategories.AutoSize = true;
            this.lblCategories.Location = new System.Drawing.Point(5, 12);
            this.lblCategories.Name = "lblCategories";
            this.lblCategories.Size = new System.Drawing.Size(57, 13);
            this.lblCategories.TabIndex = 7;
            this.lblCategories.Text = "Categories";
            this.ttipInfo.SetToolTip(this.lblCategories, "List of available projects.");
            // 
            // cboCategories
            // 
            this.cboCategories.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategories.FormattingEnabled = true;
            this.cboCategories.Location = new System.Drawing.Point(71, 8);
            this.cboCategories.Name = "cboCategories";
            this.cboCategories.Size = new System.Drawing.Size(67, 21);
            this.cboCategories.TabIndex = 8;
            this.ttipInfo.SetToolTip(this.cboCategories, "List of available projects.");
            this.cboCategories.SelectedIndexChanged += new System.EventHandler(this.cboCategories_SelectedIndexChanged);
            // 
            // spltMajor
            // 
            this.spltMajor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spltMajor.Location = new System.Drawing.Point(0, 43);
            this.spltMajor.Name = "spltMajor";
            this.spltMajor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltMajor.Panel1
            // 
            this.spltMajor.Panel1.AutoScroll = true;
            this.spltMajor.Panel1.Controls.Add(this.grpMethods);
            // 
            // spltMajor.Panel2
            // 
            this.spltMajor.Panel2.AutoScroll = true;
            this.spltMajor.Panel2.Controls.Add(this.grpResults);
            this.spltMajor.Size = new System.Drawing.Size(790, 426);
            this.spltMajor.SplitterDistance = 213;
            this.spltMajor.TabIndex = 6;
            // 
            // grpMethods
            // 
            this.grpMethods.Controls.Add(this.treeMethods);
            this.grpMethods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMethods.Location = new System.Drawing.Point(0, 0);
            this.grpMethods.Name = "grpMethods";
            this.grpMethods.Size = new System.Drawing.Size(790, 213);
            this.grpMethods.TabIndex = 1;
            this.grpMethods.TabStop = false;
            this.grpMethods.Text = "Methods";
            // 
            // treeMethods
            // 
            this.treeMethods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMethods.HotTracking = true;
            this.treeMethods.Location = new System.Drawing.Point(3, 16);
            this.treeMethods.Name = "treeMethods";
            this.treeMethods.ShowNodeToolTips = true;
            this.treeMethods.ShowPlusMinus = false;
            this.treeMethods.Size = new System.Drawing.Size(784, 194);
            this.treeMethods.TabIndex = 0;
            this.treeMethods.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMethods_AfterSelect);
            this.treeMethods.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeMethods_NodeMouseClick);
            this.treeMethods.MouseEnter += new System.EventHandler(this.treeMethods_MouseEnter);
            // 
            // grpResults
            // 
            this.grpResults.Controls.Add(this.spltResults);
            this.grpResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResults.Location = new System.Drawing.Point(0, 0);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(790, 209);
            this.grpResults.TabIndex = 7;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Results";
            // 
            // spltResults
            // 
            this.spltResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spltResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltResults.Location = new System.Drawing.Point(3, 16);
            this.spltResults.Name = "spltResults";
            // 
            // spltResults.Panel1
            // 
            this.spltResults.Panel1.AutoScroll = true;
            // 
            // spltResults.Panel2
            // 
            this.spltResults.Panel2.AutoScroll = true;
            this.spltResults.Size = new System.Drawing.Size(784, 190);
            this.spltResults.SplitterDistance = 389;
            this.spltResults.TabIndex = 6;
            this.spltResults.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spltResults_SplitterMoved);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 494);
            this.Controls.Add(this.cboCategories);
            this.Controls.Add(this.lblCategories);
            this.Controls.Add(this.spltMajor);
            this.Controls.Add(this.lblToken);
            this.Controls.Add(this.statusInfo);
            this.Controls.Add(this.btnInvoke);
            this.Controls.Add(this.txtAuth);
            this.Controls.Add(this.lblClasses);
            this.Controls.Add(this.cboClasses);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Test";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Web API";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Test_FormClosing);
            this.Load += new System.EventHandler(this.Test_Load);
            this.statusInfo.ResumeLayout(false);
            this.statusInfo.PerformLayout();
            this.spltMajor.Panel1.ResumeLayout(false);
            this.spltMajor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltMajor)).EndInit();
            this.spltMajor.ResumeLayout(false);
            this.grpMethods.ResumeLayout(false);
            this.grpResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltResults)).EndInit();
            this.spltResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboClasses;
        private System.Windows.Forms.Label lblClasses;
        private System.Windows.Forms.StatusStrip statusInfo;
        private System.Windows.Forms.ToolStripStatusLabel strpInfo;
        private System.Windows.Forms.TextBox txtAuth;
        private System.Windows.Forms.Button btnInvoke;
        private System.Windows.Forms.Label lblToken;
        private System.Windows.Forms.ToolTip ttipInfo;
        private System.Windows.Forms.SplitContainer spltMajor;
        private System.Windows.Forms.GroupBox grpMethods;
        private System.Windows.Forms.TreeView treeMethods;
        private System.Windows.Forms.SplitContainer spltResults;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.Label lblCategories;
        private System.Windows.Forms.ComboBox cboCategories;

    }
}


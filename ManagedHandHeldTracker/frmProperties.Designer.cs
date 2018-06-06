namespace ManagedHandHeldTracker
{
    partial class frmProperties
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.btnManageIMEI = new System.Windows.Forms.Button();
            this.txtGPSUpdateTime = new System.Windows.Forms.TextBox();
            this.txtSpeedLimit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.btnMore = new System.Windows.Forms.Button();
            this.txtToSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.lblBanner = new System.Windows.Forms.Label();
            this.tabWorkingModes = new System.Windows.Forms.TabPage();
            this.chkMobileController = new System.Windows.Forms.RadioButton();
            this.chkAccessController = new System.Windows.Forms.RadioButton();
            this.chkMasterCards = new System.Windows.Forms.CheckBox();
            this.chkBlockingMode = new System.Windows.Forms.CheckBox();
            this.chkTriggerEvents = new System.Windows.Forms.CheckBox();
            this.tabLinkCardholder = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.picSearchedEmp = new System.Windows.Forms.PictureBox();
            this.btnBadgeSearch = new System.Windows.Forms.Button();
            this.lblTarjeta = new System.Windows.Forms.Label();
            this.lblCI = new System.Windows.Forms.Label();
            this.lblApellido = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtBadgeSearch = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btnLink = new System.Windows.Forms.Button();
            this.grpSelected = new System.Windows.Forms.GroupBox();
            this.picLinkedEmp = new System.Windows.Forms.PictureBox();
            this.lblBadge = new System.Windows.Forms.Label();
            this.lblSSNO = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnForceLogout = new System.Windows.Forms.Button();
            this.tabControlProperties.SuspendLayout();
            this.tabProperties.SuspendLayout();
            this.tabMessages.SuspendLayout();
            this.tabWorkingModes.SuspendLayout();
            this.tabLinkCardholder.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchedEmp)).BeginInit();
            this.grpSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLinkedEmp)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 26);
            this.label1.TabIndex = 132;
            this.label1.Text = "Device Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(9, 54);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(376, 47);
            this.lblName.TabIndex = 143;
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Controls.Add(this.tabProperties);
            this.tabControlProperties.Controls.Add(this.tabMessages);
            this.tabControlProperties.Controls.Add(this.tabWorkingModes);
            this.tabControlProperties.Controls.Add(this.tabLinkCardholder);
            this.tabControlProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlProperties.Location = new System.Drawing.Point(0, -1);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(398, 412);
            this.tabControlProperties.TabIndex = 150;
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.btnForceLogout);
            this.tabProperties.Controls.Add(this.btnManageIMEI);
            this.tabProperties.Controls.Add(this.txtGPSUpdateTime);
            this.tabProperties.Controls.Add(this.txtSpeedLimit);
            this.tabProperties.Controls.Add(this.label4);
            this.tabProperties.Controls.Add(this.label3);
            this.tabProperties.Controls.Add(this.label1);
            this.tabProperties.Controls.Add(this.lblName);
            this.tabProperties.Location = new System.Drawing.Point(4, 25);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabProperties.Size = new System.Drawing.Size(390, 383);
            this.tabProperties.TabIndex = 0;
            this.tabProperties.Text = "Properties";
            this.tabProperties.UseVisualStyleBackColor = true;
            this.tabProperties.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnManageIMEI
            // 
            this.btnManageIMEI.Location = new System.Drawing.Point(66, 258);
            this.btnManageIMEI.Name = "btnManageIMEI";
            this.btnManageIMEI.Size = new System.Drawing.Size(258, 29);
            this.btnManageIMEI.TabIndex = 155;
            this.btnManageIMEI.Text = "Manage IMEI";
            this.btnManageIMEI.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnManageIMEI.UseVisualStyleBackColor = true;
            this.btnManageIMEI.Click += new System.EventHandler(this.btnManageIMEI_Click);
            // 
            // txtGPSUpdateTime
            // 
            this.txtGPSUpdateTime.Location = new System.Drawing.Point(138, 186);
            this.txtGPSUpdateTime.Name = "txtGPSUpdateTime";
            this.txtGPSUpdateTime.Size = new System.Drawing.Size(166, 23);
            this.txtGPSUpdateTime.TabIndex = 154;
            // 
            // txtSpeedLimit
            // 
            this.txtSpeedLimit.Location = new System.Drawing.Point(138, 149);
            this.txtSpeedLimit.Name = "txtSpeedLimit";
            this.txtSpeedLimit.Size = new System.Drawing.Size(166, 23);
            this.txtSpeedLimit.TabIndex = 153;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 26);
            this.label4.TabIndex = 152;
            this.label4.Text = "GPS Update time:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 26);
            this.label3.TabIndex = 151;
            this.label3.Text = "Speed Limit :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabMessages
            // 
            this.tabMessages.Controls.Add(this.btnMore);
            this.tabMessages.Controls.Add(this.txtToSend);
            this.tabMessages.Controls.Add(this.btnSend);
            this.tabMessages.Controls.Add(this.txtMessages);
            this.tabMessages.Controls.Add(this.lblBanner);
            this.tabMessages.Location = new System.Drawing.Point(4, 25);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabMessages.Size = new System.Drawing.Size(390, 383);
            this.tabMessages.TabIndex = 1;
            this.tabMessages.Text = "Messages";
            this.tabMessages.UseVisualStyleBackColor = true;
            // 
            // btnMore
            // 
            this.btnMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMore.Location = new System.Drawing.Point(254, 7);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(114, 25);
            this.btnMore.TabIndex = 141;
            this.btnMore.Text = "Previous";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // txtToSend
            // 
            this.txtToSend.Location = new System.Drawing.Point(6, 336);
            this.txtToSend.Multiline = true;
            this.txtToSend.Name = "txtToSend";
            this.txtToSend.Size = new System.Drawing.Size(274, 44);
            this.txtToSend.TabIndex = 140;
            this.txtToSend.TextChanged += new System.EventHandler(this.txtToSend_TextChanged_1);
            this.txtToSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToSend_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(286, 336);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(82, 44);
            this.btnSend.TabIndex = 139;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click_1);
            // 
            // txtMessages
            // 
            this.txtMessages.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessages.Location = new System.Drawing.Point(6, 38);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(362, 292);
            this.txtMessages.TabIndex = 138;
            // 
            // lblBanner
            // 
            this.lblBanner.BackColor = System.Drawing.Color.Black;
            this.lblBanner.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.lblBanner.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanner.ForeColor = System.Drawing.Color.White;
            this.lblBanner.Location = new System.Drawing.Point(5, 6);
            this.lblBanner.Name = "lblBanner";
            this.lblBanner.Size = new System.Drawing.Size(243, 26);
            this.lblBanner.TabIndex = 137;
            this.lblBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabWorkingModes
            // 
            this.tabWorkingModes.Controls.Add(this.chkMobileController);
            this.tabWorkingModes.Controls.Add(this.chkAccessController);
            this.tabWorkingModes.Controls.Add(this.chkMasterCards);
            this.tabWorkingModes.Controls.Add(this.chkBlockingMode);
            this.tabWorkingModes.Controls.Add(this.chkTriggerEvents);
            this.tabWorkingModes.Location = new System.Drawing.Point(4, 25);
            this.tabWorkingModes.Name = "tabWorkingModes";
            this.tabWorkingModes.Size = new System.Drawing.Size(390, 383);
            this.tabWorkingModes.TabIndex = 3;
            this.tabWorkingModes.Text = "Working Modes";
            this.tabWorkingModes.UseVisualStyleBackColor = true;
            // 
            // chkMobileController
            // 
            this.chkMobileController.AutoSize = true;
            this.chkMobileController.Location = new System.Drawing.Point(211, 29);
            this.chkMobileController.Name = "chkMobileController";
            this.chkMobileController.Size = new System.Drawing.Size(132, 21);
            this.chkMobileController.TabIndex = 140;
            this.chkMobileController.TabStop = true;
            this.chkMobileController.Text = "Mobile Controller";
            this.chkMobileController.UseVisualStyleBackColor = true;
            this.chkMobileController.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // chkAccessController
            // 
            this.chkAccessController.AutoSize = true;
            this.chkAccessController.Location = new System.Drawing.Point(30, 29);
            this.chkAccessController.Name = "chkAccessController";
            this.chkAccessController.Size = new System.Drawing.Size(136, 21);
            this.chkAccessController.TabIndex = 139;
            this.chkAccessController.TabStop = true;
            this.chkAccessController.Text = "Access Controller";
            this.chkAccessController.UseVisualStyleBackColor = true;
            this.chkAccessController.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // chkMasterCards
            // 
            this.chkMasterCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMasterCards.AutoSize = true;
            this.chkMasterCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMasterCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMasterCards.Location = new System.Drawing.Point(136, 217);
            this.chkMasterCards.Name = "chkMasterCards";
            this.chkMasterCards.Size = new System.Drawing.Size(108, 21);
            this.chkMasterCards.TabIndex = 136;
            this.chkMasterCards.Text = "Master Cards";
            this.chkMasterCards.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMasterCards.UseVisualStyleBackColor = true;
            this.chkMasterCards.CheckedChanged += new System.EventHandler(this.chkMasterCards_CheckedChanged);
            // 
            // chkBlockingMode
            // 
            this.chkBlockingMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBlockingMode.AutoSize = true;
            this.chkBlockingMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBlockingMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBlockingMode.Location = new System.Drawing.Point(136, 168);
            this.chkBlockingMode.Name = "chkBlockingMode";
            this.chkBlockingMode.Size = new System.Drawing.Size(116, 21);
            this.chkBlockingMode.TabIndex = 135;
            this.chkBlockingMode.Text = "Blocking Mode";
            this.chkBlockingMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkBlockingMode.UseVisualStyleBackColor = true;
            this.chkBlockingMode.CheckedChanged += new System.EventHandler(this.chkBlockingMode_CheckedChanged);
            // 
            // chkTriggerEvents
            // 
            this.chkTriggerEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTriggerEvents.AutoSize = true;
            this.chkTriggerEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTriggerEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTriggerEvents.Location = new System.Drawing.Point(122, 123);
            this.chkTriggerEvents.Name = "chkTriggerEvents";
            this.chkTriggerEvents.Size = new System.Drawing.Size(117, 21);
            this.chkTriggerEvents.TabIndex = 134;
            this.chkTriggerEvents.Text = "Trigger Events";
            this.chkTriggerEvents.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTriggerEvents.UseVisualStyleBackColor = true;
            this.chkTriggerEvents.CheckedChanged += new System.EventHandler(this.chkTriggerEvents_CheckedChanged);
            // 
            // tabLinkCardholder
            // 
            this.tabLinkCardholder.Controls.Add(this.groupBox3);
            this.tabLinkCardholder.Controls.Add(this.btnLink);
            this.tabLinkCardholder.Controls.Add(this.grpSelected);
            this.tabLinkCardholder.Location = new System.Drawing.Point(4, 25);
            this.tabLinkCardholder.Name = "tabLinkCardholder";
            this.tabLinkCardholder.Size = new System.Drawing.Size(390, 383);
            this.tabLinkCardholder.TabIndex = 4;
            this.tabLinkCardholder.Text = "Linked CardHolder";
            this.tabLinkCardholder.UseVisualStyleBackColor = true;
            this.tabLinkCardholder.Click += new System.EventHandler(this.tabPage5_Click);
            this.tabLinkCardholder.Enter += new System.EventHandler(this.tabPage5_Enter);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.picSearchedEmp);
            this.groupBox3.Controls.Add(this.btnBadgeSearch);
            this.groupBox3.Controls.Add(this.lblTarjeta);
            this.groupBox3.Controls.Add(this.lblCI);
            this.groupBox3.Controls.Add(this.lblApellido);
            this.groupBox3.Controls.Add(this.lblNombre);
            this.groupBox3.Controls.Add(this.txtBadgeSearch);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Location = new System.Drawing.Point(8, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 187);
            this.groupBox3.TabIndex = 155;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search cardholder";
            // 
            // picSearchedEmp
            // 
            this.picSearchedEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSearchedEmp.Location = new System.Drawing.Point(243, 55);
            this.picSearchedEmp.Name = "picSearchedEmp";
            this.picSearchedEmp.Size = new System.Drawing.Size(108, 119);
            this.picSearchedEmp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSearchedEmp.TabIndex = 158;
            this.picSearchedEmp.TabStop = false;
            // 
            // btnBadgeSearch
            // 
            this.btnBadgeSearch.Location = new System.Drawing.Point(242, 22);
            this.btnBadgeSearch.Name = "btnBadgeSearch";
            this.btnBadgeSearch.Size = new System.Drawing.Size(108, 26);
            this.btnBadgeSearch.TabIndex = 157;
            this.btnBadgeSearch.Text = "Search";
            this.btnBadgeSearch.UseVisualStyleBackColor = true;
            this.btnBadgeSearch.Click += new System.EventHandler(this.btnBadgeSearch_Click);
            // 
            // lblTarjeta
            // 
            this.lblTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTarjeta.Location = new System.Drawing.Point(93, 154);
            this.lblTarjeta.Name = "lblTarjeta";
            this.lblTarjeta.Size = new System.Drawing.Size(143, 20);
            this.lblTarjeta.TabIndex = 156;
            // 
            // lblCI
            // 
            this.lblCI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCI.Location = new System.Drawing.Point(93, 124);
            this.lblCI.Name = "lblCI";
            this.lblCI.Size = new System.Drawing.Size(143, 20);
            this.lblCI.TabIndex = 156;
            // 
            // lblApellido
            // 
            this.lblApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblApellido.Location = new System.Drawing.Point(93, 94);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(143, 20);
            this.lblApellido.TabIndex = 156;
            // 
            // lblNombre
            // 
            this.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNombre.Location = new System.Drawing.Point(93, 64);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(143, 20);
            this.lblNombre.TabIndex = 156;
            // 
            // txtBadgeSearch
            // 
            this.txtBadgeSearch.Location = new System.Drawing.Point(70, 22);
            this.txtBadgeSearch.Name = "txtBadgeSearch";
            this.txtBadgeSearch.Size = new System.Drawing.Size(166, 23);
            this.txtBadgeSearch.TabIndex = 152;
            this.txtBadgeSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBadgeSearch_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(11, 154);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 17);
            this.label21.TabIndex = 155;
            this.label21.Text = "Badge:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 17);
            this.label11.TabIndex = 151;
            this.label11.Text = "Badge:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(11, 124);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(51, 17);
            this.label22.TabIndex = 154;
            this.label22.Text = "SSNO:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(11, 94);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(78, 17);
            this.label23.TabIndex = 153;
            this.label23.Text = "Last name:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(11, 64);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(78, 17);
            this.label24.TabIndex = 152;
            this.label24.Text = "First name:";
            // 
            // btnLink
            // 
            this.btnLink.Enabled = false;
            this.btnLink.Location = new System.Drawing.Point(273, 357);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(85, 26);
            this.btnLink.TabIndex = 156;
            this.btnLink.Text = "Link";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // grpSelected
            // 
            this.grpSelected.Controls.Add(this.picLinkedEmp);
            this.grpSelected.Controls.Add(this.lblBadge);
            this.grpSelected.Controls.Add(this.lblSSNO);
            this.grpSelected.Controls.Add(this.lblLastName);
            this.grpSelected.Controls.Add(this.lblFirstName);
            this.grpSelected.Controls.Add(this.label16);
            this.grpSelected.Controls.Add(this.label15);
            this.grpSelected.Controls.Add(this.label14);
            this.grpSelected.Controls.Add(this.label13);
            this.grpSelected.Location = new System.Drawing.Point(8, 15);
            this.grpSelected.Name = "grpSelected";
            this.grpSelected.Size = new System.Drawing.Size(357, 141);
            this.grpSelected.TabIndex = 155;
            this.grpSelected.TabStop = false;
            this.grpSelected.Text = "Linked cardholder";
            // 
            // picLinkedEmp
            // 
            this.picLinkedEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLinkedEmp.Location = new System.Drawing.Point(243, 15);
            this.picLinkedEmp.Name = "picLinkedEmp";
            this.picLinkedEmp.Size = new System.Drawing.Size(108, 119);
            this.picLinkedEmp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLinkedEmp.TabIndex = 158;
            this.picLinkedEmp.TabStop = false;
            // 
            // lblBadge
            // 
            this.lblBadge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBadge.Location = new System.Drawing.Point(93, 112);
            this.lblBadge.Name = "lblBadge";
            this.lblBadge.Size = new System.Drawing.Size(143, 20);
            this.lblBadge.TabIndex = 156;
            // 
            // lblSSNO
            // 
            this.lblSSNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSSNO.Location = new System.Drawing.Point(93, 83);
            this.lblSSNO.Name = "lblSSNO";
            this.lblSSNO.Size = new System.Drawing.Size(143, 20);
            this.lblSSNO.TabIndex = 156;
            // 
            // lblLastName
            // 
            this.lblLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLastName.Location = new System.Drawing.Point(93, 54);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(143, 20);
            this.lblLastName.TabIndex = 156;
            // 
            // lblFirstName
            // 
            this.lblFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFirstName.Location = new System.Drawing.Point(93, 25);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(143, 20);
            this.lblFirstName.TabIndex = 156;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 112);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 17);
            this.label16.TabIndex = 155;
            this.label16.Text = "Badge:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 83);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 17);
            this.label15.TabIndex = 154;
            this.label15.Text = "SSNO:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 54);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 17);
            this.label14.TabIndex = 153;
            this.label14.Text = "Last name:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 17);
            this.label13.TabIndex = 152;
            this.label13.Text = "First name:";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(314, 417);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 26);
            this.btnOK.TabIndex = 152;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(219, 417);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 26);
            this.btnCancel.TabIndex = 151;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnForceLogout
            // 
            this.btnForceLogout.Location = new System.Drawing.Point(66, 302);
            this.btnForceLogout.Name = "btnForceLogout";
            this.btnForceLogout.Size = new System.Drawing.Size(258, 29);
            this.btnForceLogout.TabIndex = 156;
            this.btnForceLogout.Text = "Force logout";
            this.btnForceLogout.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnForceLogout.UseVisualStyleBackColor = true;
            this.btnForceLogout.Click += new System.EventHandler(this.btnForceLogout_Click);
            // 
            // frmProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 448);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControlProperties);
            this.Name = "frmProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Device properties";
            this.Activated += new System.EventHandler(this.frmProperties_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProperties_FormClosing);
            this.Load += new System.EventHandler(this.frmProperties_Load);
            this.tabControlProperties.ResumeLayout(false);
            this.tabProperties.ResumeLayout(false);
            this.tabProperties.PerformLayout();
            this.tabMessages.ResumeLayout(false);
            this.tabMessages.PerformLayout();
            this.tabWorkingModes.ResumeLayout(false);
            this.tabWorkingModes.PerformLayout();
            this.tabLinkCardholder.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchedEmp)).EndInit();
            this.grpSelected.ResumeLayout(false);
            this.grpSelected.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLinkedEmp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.TextBox txtToSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Label lblBanner;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabWorkingModes;
        private System.Windows.Forms.CheckBox chkBlockingMode;
        private System.Windows.Forms.CheckBox chkTriggerEvents;
        private System.Windows.Forms.CheckBox chkMasterCards;
        private System.Windows.Forms.TabPage tabLinkCardholder;
        private System.Windows.Forms.TextBox txtBadgeSearch;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnLink;
        private System.Windows.Forms.GroupBox grpSelected;
        private System.Windows.Forms.Label lblBadge;
        private System.Windows.Forms.Label lblSSNO;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBadgeSearch;
        private System.Windows.Forms.Label lblTarjeta;
        private System.Windows.Forms.Label lblCI;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.RadioButton chkAccessController;
        private System.Windows.Forms.RadioButton chkMobileController;
        private System.Windows.Forms.PictureBox picSearchedEmp;
        private System.Windows.Forms.PictureBox picLinkedEmp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGPSUpdateTime;
        private System.Windows.Forms.TextBox txtSpeedLimit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnManageIMEI;
        private System.Windows.Forms.Button btnForceLogout;
    }
}
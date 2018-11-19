namespace stonemgr
{
    partial class MainForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stoneInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stoneListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whsearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whcountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stoneorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adduserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupmgrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permissionmgrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(810, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 21);
            this.textBox1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stoneInfoToolStripMenuItem,
            this.stoneToolStripMenuItem,
            this.stoneorderToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1010, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // stoneInfoToolStripMenuItem
            // 
            this.stoneInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.addToolStripMenuItem,
            this.stoneListToolStripMenuItem,
            this.showToolStripMenuItem});
            this.stoneInfoToolStripMenuItem.Name = "stoneInfoToolStripMenuItem";
            this.stoneInfoToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.stoneInfoToolStripMenuItem.Text = "石位信息";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.searchToolStripMenuItem.Text = "搜索石位";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "添加石位";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // stoneListToolStripMenuItem
            // 
            this.stoneListToolStripMenuItem.Name = "stoneListToolStripMenuItem";
            this.stoneListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stoneListToolStripMenuItem.Text = "石位列表";
            this.stoneListToolStripMenuItem.Click += new System.EventHandler(this.stoneListToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showToolStripMenuItem.Text = "showAll";
            this.showToolStripMenuItem.Visible = false;
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // stoneToolStripMenuItem
            // 
            this.stoneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whinToolStripMenuItem,
            this.whoutToolStripMenuItem,
            this.whsearchToolStripMenuItem,
            this.whcountToolStripMenuItem});
            this.stoneToolStripMenuItem.Name = "stoneToolStripMenuItem";
            this.stoneToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.stoneToolStripMenuItem.Text = "石头仓库";
            // 
            // whinToolStripMenuItem
            // 
            this.whinToolStripMenuItem.Name = "whinToolStripMenuItem";
            this.whinToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.whinToolStripMenuItem.Text = "进仓操作";
            // 
            // whoutToolStripMenuItem
            // 
            this.whoutToolStripMenuItem.Name = "whoutToolStripMenuItem";
            this.whoutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.whoutToolStripMenuItem.Text = "出仓操作";
            // 
            // whsearchToolStripMenuItem
            // 
            this.whsearchToolStripMenuItem.Name = "whsearchToolStripMenuItem";
            this.whsearchToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.whsearchToolStripMenuItem.Text = "库存查询";
            // 
            // whcountToolStripMenuItem
            // 
            this.whcountToolStripMenuItem.Name = "whcountToolStripMenuItem";
            this.whcountToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.whcountToolStripMenuItem.Text = "库存统计";
            // 
            // stoneorderToolStripMenuItem
            // 
            this.stoneorderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildorderToolStripMenuItem,
            this.orderlistToolStripMenuItem});
            this.stoneorderToolStripMenuItem.Name = "stoneorderToolStripMenuItem";
            this.stoneorderToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.stoneorderToolStripMenuItem.Text = "订单管理";
            // 
            // buildorderToolStripMenuItem
            // 
            this.buildorderToolStripMenuItem.Name = "buildorderToolStripMenuItem";
            this.buildorderToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.buildorderToolStripMenuItem.Text = "新订单";
            // 
            // orderlistToolStripMenuItem
            // 
            this.orderlistToolStripMenuItem.Name = "orderlistToolStripMenuItem";
            this.orderlistToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.orderlistToolStripMenuItem.Text = "订单列表";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adduserToolStripMenuItem,
            this.groupmgrToolStripMenuItem,
            this.permissionmgrToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.optionToolStripMenuItem.Text = "配置";
            // 
            // adduserToolStripMenuItem
            // 
            this.adduserToolStripMenuItem.Name = "adduserToolStripMenuItem";
            this.adduserToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.adduserToolStripMenuItem.Text = "用户管理";
            this.adduserToolStripMenuItem.Click += new System.EventHandler(this.adduserToolStripMenuItem_Click);
            // 
            // groupmgrToolStripMenuItem
            // 
            this.groupmgrToolStripMenuItem.Name = "groupmgrToolStripMenuItem";
            this.groupmgrToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.groupmgrToolStripMenuItem.Text = "用户组管理";
            this.groupmgrToolStripMenuItem.Click += new System.EventHandler(this.groupmgrToolStripMenuItem_Click);
            // 
            // permissionmgrToolStripMenuItem
            // 
            this.permissionmgrToolStripMenuItem.Name = "permissionmgrToolStripMenuItem";
            this.permissionmgrToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.permissionmgrToolStripMenuItem.Text = "权限管理";
            this.permissionmgrToolStripMenuItem.Click += new System.EventHandler(this.permissionmgrToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutmeToolStripMenuItem,
            this.versionToolStripMenuItem,
            this.devlogToolStripMenuItem,
            this.toolStripMenuItem2});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.aboutToolStripMenuItem.Text = "关于";
            // 
            // aboutmeToolStripMenuItem
            // 
            this.aboutmeToolStripMenuItem.Name = "aboutmeToolStripMenuItem";
            this.aboutmeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.aboutmeToolStripMenuItem.Text = "关于我";
            this.aboutmeToolStripMenuItem.Click += new System.EventHandler(this.aboutmeToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.versionToolStripMenuItem.Text = "软件版本";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // devlogToolStripMenuItem
            // 
            this.devlogToolStripMenuItem.Name = "devlogToolStripMenuItem";
            this.devlogToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.devlogToolStripMenuItem.Text = "开发日志";
            this.devlogToolStripMenuItem.Click += new System.EventHandler(this.devlogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "软件协议";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 662);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 692);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "商品石位数据管理系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stoneInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stoneListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whsearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whcountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stoneorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adduserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupmgrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permissionmgrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutmeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devlogToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}
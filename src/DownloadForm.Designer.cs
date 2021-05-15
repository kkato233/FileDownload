namespace FileDownloadApplication
{
    /// <summary>
    /// インターネットからファイルをダウンロードを行うためのアプリケーションのメインフォームです。
    /// </summary>
    partial class DownloadForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonChangeDownloadPath = new System.Windows.Forms.Button();
            this.textDownloadPath = new System.Windows.Forms.TextBox();
            this.listURLList = new System.Windows.Forms.ListBox();
            this.buttonAddUrl = new System.Windows.Forms.Button();
            this.textDownloadUrl = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChangeDownloadPath
            // 
            this.buttonChangeDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChangeDownloadPath.Location = new System.Drawing.Point(540, 10);
            this.buttonChangeDownloadPath.Name = "buttonChangeDownloadPath";
            this.buttonChangeDownloadPath.Size = new System.Drawing.Size(45, 23);
            this.buttonChangeDownloadPath.TabIndex = 1;
            this.buttonChangeDownloadPath.Text = "...";
            this.buttonChangeDownloadPath.UseVisualStyleBackColor = true;
            this.buttonChangeDownloadPath.Click += new System.EventHandler(this.buttonChangeDownloadPath_Click);
            // 
            // textDownloadPath
            // 
            this.textDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textDownloadPath.BackColor = System.Drawing.SystemColors.Window;
            this.textDownloadPath.Location = new System.Drawing.Point(115, 12);
            this.textDownloadPath.Name = "textDownloadPath";
            this.textDownloadPath.ReadOnly = true;
            this.textDownloadPath.Size = new System.Drawing.Size(410, 19);
            this.textDownloadPath.TabIndex = 2;
            // 
            // listURLList
            // 
            this.listURLList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listURLList.FormattingEnabled = true;
            this.listURLList.ItemHeight = 12;
            this.listURLList.Location = new System.Drawing.Point(33, 58);
            this.listURLList.Name = "listURLList";
            this.listURLList.Size = new System.Drawing.Size(550, 184);
            this.listURLList.TabIndex = 3;
            // 
            // buttonAddUrl
            // 
            this.buttonAddUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddUrl.Enabled = false;
            this.buttonAddUrl.Location = new System.Drawing.Point(33, 297);
            this.buttonAddUrl.Name = "buttonAddUrl";
            this.buttonAddUrl.Size = new System.Drawing.Size(195, 23);
            this.buttonAddUrl.TabIndex = 4;
            this.buttonAddUrl.Text = "読み込みURLの追加";
            this.buttonAddUrl.UseVisualStyleBackColor = true;
            this.buttonAddUrl.Click += new System.EventHandler(this.buttonAddUrl_Click);
            // 
            // textDownloadUrl
            // 
            this.textDownloadUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textDownloadUrl.Location = new System.Drawing.Point(33, 272);
            this.textDownloadUrl.Name = "textDownloadUrl";
            this.textDownloadUrl.Size = new System.Drawing.Size(550, 19);
            this.textDownloadUrl.TabIndex = 5;
            this.textDownloadUrl.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(609, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(594, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "ダウンロード先：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "URL一覧";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "ダウンロードURL";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.Personal;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 361);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textDownloadUrl);
            this.Controls.Add(this.buttonAddUrl);
            this.Controls.Add(this.listURLList);
            this.Controls.Add(this.textDownloadPath);
            this.Controls.Add(this.buttonChangeDownloadPath);
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "DownloadForm";
            this.Text = "ファイルのダウンロード";
            this.Shown += new System.EventHandler(this.DownloadForm_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DownloadForm_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonChangeDownloadPath;
        private System.Windows.Forms.TextBox textDownloadPath;
        private System.Windows.Forms.ListBox listURLList;
        private System.Windows.Forms.Button buttonAddUrl;
        private System.Windows.Forms.TextBox textDownloadUrl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Timer timer1;

    }
}
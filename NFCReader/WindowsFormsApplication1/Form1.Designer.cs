namespace WindowsFormsApplication1
{
    partial class NFC_Test
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Test_BTN = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.UID_Text = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Test_BTN
            // 
            this.Test_BTN.BackColor = System.Drawing.Color.SpringGreen;
            this.Test_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Test_BTN.Font = new System.Drawing.Font("华文新魏", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Test_BTN.ForeColor = System.Drawing.Color.LightYellow;
            this.Test_BTN.Location = new System.Drawing.Point(50, 35);
            this.Test_BTN.Name = "Test_BTN";
            this.Test_BTN.Size = new System.Drawing.Size(268, 79);
            this.Test_BTN.TabIndex = 0;
            this.Test_BTN.Text = "开始测试";
            this.Test_BTN.UseVisualStyleBackColor = false;
            this.Test_BTN.Click += new System.EventHandler(this.Test_BTN_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 270);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(358, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // UID_Text
            // 
            this.UID_Text.AutoSize = true;
            this.UID_Text.Font = new System.Drawing.Font("华文新魏", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UID_Text.Location = new System.Drawing.Point(46, 141);
            this.UID_Text.Name = "UID_Text";
            this.UID_Text.Size = new System.Drawing.Size(14, 19);
            this.UID_Text.TabIndex = 2;
            this.UID_Text.Text = " ";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Font = new System.Drawing.Font("华文新魏", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusLabel.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(200, 3, 0, 2);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatusLabel.Size = new System.Drawing.Size(103, 17);
            this.StatusLabel.Text = "设备初始化...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.UID_Text);
            this.panel1.Controls.Add(this.Test_BTN);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 270);
            this.panel1.TabIndex = 3;
            // 
            // NFC_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 292);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "NFC_Test";
            this.Text = "NFC自动读卡测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NFC_Test_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Test_BTN;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label UID_Text;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Panel panel1;
    }
}


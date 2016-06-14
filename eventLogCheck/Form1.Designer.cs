namespace eventLogCheck
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkTimer = new System.Windows.Forms.Timer(this.components);
            this.send_testMail_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 42);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(519, 406);
            this.textBox1.TabIndex = 1;
            // 
            // checkTimer
            // 
            this.checkTimer.Tick += new System.EventHandler(this.checkTimer_Tick);
            // 
            // send_testMail_btn
            // 
            this.send_testMail_btn.Location = new System.Drawing.Point(13, 13);
            this.send_testMail_btn.Name = "send_testMail_btn";
            this.send_testMail_btn.Size = new System.Drawing.Size(202, 23);
            this.send_testMail_btn.TabIndex = 2;
            this.send_testMail_btn.Text = "寄發測試信";
            this.send_testMail_btn.UseVisualStyleBackColor = true;
            this.send_testMail_btn.Click += new System.EventHandler(this.send_testMail_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 460);
            this.Controls.Add(this.send_testMail_btn);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "EventLogCheck";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer checkTimer;
        private System.Windows.Forms.Button send_testMail_btn;
    }
}


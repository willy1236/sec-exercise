namespace RSA01
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
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.PubKey = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.PvtKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PlainText = new System.Windows.Forms.TextBox();
            this.CryText = new System.Windows.Forms.TextBox();
            this.DecodeText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "產生金鑰";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PubKey
            // 
            this.PubKey.Location = new System.Drawing.Point(147, 37);
            this.PubKey.Multiline = true;
            this.PubKey.Name = "PubKey";
            this.PubKey.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PubKey.Size = new System.Drawing.Size(278, 240);
            this.PubKey.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(30, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 55);
            this.button2.TabIndex = 3;
            this.button2.Text = "讀取金鑰";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PvtKey
            // 
            this.PvtKey.Location = new System.Drawing.Point(444, 37);
            this.PvtKey.Multiline = true;
            this.PvtKey.Name = "PvtKey";
            this.PvtKey.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PvtKey.Size = new System.Drawing.Size(278, 240);
            this.PvtKey.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "公鑰";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(451, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "私鑰";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(30, 333);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 36);
            this.button3.TabIndex = 7;
            this.button3.Text = "加密";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(30, 392);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 40);
            this.button4.TabIndex = 8;
            this.button4.Text = "解密";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "明文";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(308, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "密文";
            // 
            // PlainText
            // 
            this.PlainText.Location = new System.Drawing.Point(147, 318);
            this.PlainText.Multiline = true;
            this.PlainText.Name = "PlainText";
            this.PlainText.Size = new System.Drawing.Size(157, 123);
            this.PlainText.TabIndex = 12;
            // 
            // CryText
            // 
            this.CryText.Location = new System.Drawing.Point(310, 318);
            this.CryText.Multiline = true;
            this.CryText.Name = "CryText";
            this.CryText.Size = new System.Drawing.Size(225, 169);
            this.CryText.TabIndex = 13;
            // 
            // DecodeText
            // 
            this.DecodeText.Location = new System.Drawing.Point(541, 318);
            this.DecodeText.Multiline = true;
            this.DecodeText.Name = "DecodeText";
            this.DecodeText.Size = new System.Drawing.Size(157, 123);
            this.DecodeText.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(539, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "明文";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 534);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DecodeText);
            this.Controls.Add(this.CryText);
            this.Controls.Add(this.PlainText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PvtKey);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PubKey);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox PubKey;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox PvtKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PlainText;
        private System.Windows.Forms.TextBox CryText;
        private System.Windows.Forms.TextBox DecodeText;
        private System.Windows.Forms.Label label5;
    }
}


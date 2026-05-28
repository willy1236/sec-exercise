namespace authenticator
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
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.picQr = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picQr)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "綁定Google Authenticator";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "SecreKey";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(383, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 57);
            this.button2.TabIndex = 2;
            this.button2.Text = "驗證";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 323);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "輸入驗證碼";
            // 
            // txtSecret
            // 
            this.txtSecret.Location = new System.Drawing.Point(113, 117);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(151, 22);
            this.txtSecret.TabIndex = 4;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(319, 299);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(33, 12);
            this.lblResult.TabIndex = 5;
            this.lblResult.Text = "label3";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(113, 320);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 22);
            this.txtCode.TabIndex = 6;
            // 
            // picQr
            // 
            this.picQr.Location = new System.Drawing.Point(321, 32);
            this.picQr.Name = "picQr";
            this.picQr.Size = new System.Drawing.Size(250, 250);
            this.picQr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picQr.TabIndex = 7;
            this.picQr.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 404);
            this.Controls.Add(this.picQr);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtSecret);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picQr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.PictureBox picQr;
    }
}


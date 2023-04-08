namespace Client
{
    partial class ClientForm
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
            this.lstMess = new System.Windows.Forms.ListView();
            this.txtMess = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstMess
            // 
            this.lstMess.HideSelection = false;
            this.lstMess.Location = new System.Drawing.Point(12, 12);
            this.lstMess.Name = "lstMess";
            this.lstMess.Size = new System.Drawing.Size(575, 287);
            this.lstMess.TabIndex = 0;
            this.lstMess.UseCompatibleStateImageBehavior = false;
            this.lstMess.View = System.Windows.Forms.View.List;
            // 
            // txtMess
            // 
            this.txtMess.Location = new System.Drawing.Point(12, 305);
            this.txtMess.Multiline = true;
            this.txtMess.Name = "txtMess";
            this.txtMess.Size = new System.Drawing.Size(500, 37);
            this.txtMess.TabIndex = 1;
            this.txtMess.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMess_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(512, 305);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 37);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 354);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMess);
            this.Controls.Add(this.lstMess);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ClientForm";
            this.Text = "CLIENT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstMess;
        private System.Windows.Forms.TextBox txtMess;
        private System.Windows.Forms.Button btnSend;
    }
}


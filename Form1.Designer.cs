namespace Flatty2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new System.Windows.Forms.Label();
            chatMessageBox = new System.Windows.Forms.TextBox();
            refresh = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(21, 561);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(124, 25);
            label1.TabIndex = 0;
            label1.Text = "chat message:";
            // 
            // chatMessageBox
            // 
            chatMessageBox.Location = new System.Drawing.Point(151, 558);
            chatMessageBox.Name = "chatMessageBox";
            chatMessageBox.Size = new System.Drawing.Size(898, 31);
            chatMessageBox.TabIndex = 1;
            chatMessageBox.KeyDown += SendChatMessage;
            // 
            // refresh
            // 
            refresh.Enabled = true;
            refresh.Tick += refresh_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1200, 650);
            Controls.Add(chatMessageBox);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Paint += OnPaint;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chatMessageBox;
        private System.Windows.Forms.Timer refresh;
    }
}

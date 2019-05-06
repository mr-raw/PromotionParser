namespace PromotionParser.UI.Forms
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
            this.TestButton = new System.Windows.Forms.Button();
            this.ResultTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(669, 408);
            this.TestButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(88, 27);
            this.TestButton.TabIndex = 0;
            this.TestButton.Text = "Test";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // ResultTreeView
            // 
            this.ResultTreeView.Location = new System.Drawing.Point(12, 12);
            this.ResultTreeView.Name = "ResultTreeView";
            this.ResultTreeView.Size = new System.Drawing.Size(746, 390);
            this.ResultTreeView.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 443);
            this.Controls.Add(this.ResultTreeView);
            this.Controls.Add(this.TestButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Testing";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TreeView ResultTreeView;
        private System.Windows.Forms.Button TestButton;
    }
}


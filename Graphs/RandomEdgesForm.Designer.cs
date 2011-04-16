namespace Graphs
{
    partial class RandomEdgesForm
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
            this.randomEdgesText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // randomEdgesText
            // 
            this.randomEdgesText.Location = new System.Drawing.Point(10, 25);
            this.randomEdgesText.Name = "randomEdgesText";
            this.randomEdgesText.Size = new System.Drawing.Size(100, 20);
            this.randomEdgesText.TabIndex = 0;
            this.randomEdgesText.TextChanged += new System.EventHandler(this.randomEdgesText_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Введите число случайных рёбер:";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(10, 68);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(92, 23);
            this.generateButton.TabIndex = 2;
            this.generateButton.Text = "Генерировать";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // RandomEdgesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 101);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.randomEdgesText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RandomEdgesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Генерация случайных рёбер";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox randomEdgesText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button generateButton;
    }
}
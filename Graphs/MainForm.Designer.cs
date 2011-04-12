namespace Graphs
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
            this.generateButton = new System.Windows.Forms.Button();
            this.genLabel = new System.Windows.Forms.Label();
            this.vertexCountText = new System.Windows.Forms.TextBox();
            this.renderFrame = new System.Windows.Forms.Panel();
            this.addEdgeButton = new System.Windows.Forms.Button();
            this.deleteEdgeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(516, 457);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(96, 23);
            this.generateButton.TabIndex = 0;
            this.generateButton.Text = "Сгенерировать";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // genLabel
            // 
            this.genLabel.AutoSize = true;
            this.genLabel.Location = new System.Drawing.Point(359, 462);
            this.genLabel.Name = "genLabel";
            this.genLabel.Size = new System.Drawing.Size(85, 13);
            this.genLabel.TabIndex = 1;
            this.genLabel.Text = "Кол-во вершин:";
            // 
            // vertexCountText
            // 
            this.vertexCountText.Location = new System.Drawing.Point(450, 459);
            this.vertexCountText.Name = "vertexCountText";
            this.vertexCountText.Size = new System.Drawing.Size(60, 20);
            this.vertexCountText.TabIndex = 2;
            this.vertexCountText.TextChanged += new System.EventHandler(this.vertexCountText_TextChanged);
            // 
            // renderFrame
            // 
            this.renderFrame.BackColor = System.Drawing.Color.Gray;
            this.renderFrame.Location = new System.Drawing.Point(12, 51);
            this.renderFrame.Name = "renderFrame";
            this.renderFrame.Size = new System.Drawing.Size(600, 400);
            this.renderFrame.TabIndex = 3;
            // 
            // addEdgeButton
            // 
            this.addEdgeButton.Location = new System.Drawing.Point(12, 457);
            this.addEdgeButton.Name = "addEdgeButton";
            this.addEdgeButton.Size = new System.Drawing.Size(100, 23);
            this.addEdgeButton.TabIndex = 4;
            this.addEdgeButton.Text = "Добавить ребро";
            this.addEdgeButton.UseVisualStyleBackColor = true;
            this.addEdgeButton.Click += new System.EventHandler(this.addEdgeButton_Click);
            // 
            // deleteEdgeButton
            // 
            this.deleteEdgeButton.Location = new System.Drawing.Point(118, 457);
            this.deleteEdgeButton.Name = "deleteEdgeButton";
            this.deleteEdgeButton.Size = new System.Drawing.Size(100, 23);
            this.deleteEdgeButton.TabIndex = 5;
            this.deleteEdgeButton.Text = "Удалить ребро";
            this.deleteEdgeButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 492);
            this.Controls.Add(this.deleteEdgeButton);
            this.Controls.Add(this.addEdgeButton);
            this.Controls.Add(this.renderFrame);
            this.Controls.Add(this.vertexCountText);
            this.Controls.Add(this.genLabel);
            this.Controls.Add(this.generateButton);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 530);
            this.MinimumSize = new System.Drawing.Size(640, 530);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphs Renderer by Alexander Mironov";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label genLabel;
        private System.Windows.Forms.TextBox vertexCountText;
        private System.Windows.Forms.Panel renderFrame;
        private System.Windows.Forms.Button addEdgeButton;
        private System.Windows.Forms.Button deleteEdgeButton;

    }
}


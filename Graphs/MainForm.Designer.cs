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
            this.graphInfoLabel = new System.Windows.Forms.Label();
            this.addEdgeButton = new System.Windows.Forms.Button();
            this.deleteEdgeButton = new System.Windows.Forms.Button();
            this.typeSelectBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.orientLabel = new System.Windows.Forms.Label();
            this.orientSelectBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(210, 21);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(78, 40);
            this.generateButton.TabIndex = 0;
            this.generateButton.Text = "Генерация";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // genLabel
            // 
            this.genLabel.AutoSize = true;
            this.genLabel.Location = new System.Drawing.Point(6, 21);
            this.genLabel.Name = "genLabel";
            this.genLabel.Size = new System.Drawing.Size(49, 13);
            this.genLabel.TabIndex = 1;
            this.genLabel.Text = "Вершин:";
            // 
            // vertexCountText
            // 
            this.vertexCountText.Location = new System.Drawing.Point(9, 40);
            this.vertexCountText.Name = "vertexCountText";
            this.vertexCountText.Size = new System.Drawing.Size(46, 20);
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
            // graphInfoLabel
            // 
            this.graphInfoLabel.AutoSize = true;
            this.graphInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.graphInfoLabel.ForeColor = System.Drawing.Color.DimGray;
            this.graphInfoLabel.Location = new System.Drawing.Point(12, 35);
            this.graphInfoLabel.Name = "graphInfoLabel";
            this.graphInfoLabel.Size = new System.Drawing.Size(105, 13);
            this.graphInfoLabel.TabIndex = 0;
            this.graphInfoLabel.Text = "Сгенерируйте граф";
            // 
            // addEdgeButton
            // 
            this.addEdgeButton.Location = new System.Drawing.Point(12, 511);
            this.addEdgeButton.Name = "addEdgeButton";
            this.addEdgeButton.Size = new System.Drawing.Size(100, 23);
            this.addEdgeButton.TabIndex = 4;
            this.addEdgeButton.Text = "Добавить ребро";
            this.addEdgeButton.UseVisualStyleBackColor = true;
            this.addEdgeButton.Click += new System.EventHandler(this.addEdgeButton_Click);
            // 
            // deleteEdgeButton
            // 
            this.deleteEdgeButton.Location = new System.Drawing.Point(118, 511);
            this.deleteEdgeButton.Name = "deleteEdgeButton";
            this.deleteEdgeButton.Size = new System.Drawing.Size(100, 23);
            this.deleteEdgeButton.TabIndex = 5;
            this.deleteEdgeButton.Text = "Удалить ребро";
            this.deleteEdgeButton.UseVisualStyleBackColor = true;
            this.deleteEdgeButton.Click += new System.EventHandler(this.deleteEdgeButton_Click);
            // 
            // typeSelectBox
            // 
            this.typeSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeSelectBox.FormattingEnabled = true;
            this.typeSelectBox.Items.AddRange(new object[] {
            "M",
            "L"});
            this.typeSelectBox.Location = new System.Drawing.Point(75, 39);
            this.typeSelectBox.Name = "typeSelectBox";
            this.typeSelectBox.Size = new System.Drawing.Size(34, 21);
            this.typeSelectBox.TabIndex = 6;
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(72, 21);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(29, 13);
            this.typeLabel.TabIndex = 7;
            this.typeLabel.Text = "Тип:";
            // 
            // orientLabel
            // 
            this.orientLabel.AutoSize = true;
            this.orientLabel.Location = new System.Drawing.Point(124, 21);
            this.orientLabel.Name = "orientLabel";
            this.orientLabel.Size = new System.Drawing.Size(71, 13);
            this.orientLabel.TabIndex = 8;
            this.orientLabel.Text = "Ориентация:";
            // 
            // orientSelectBox
            // 
            this.orientSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.orientSelectBox.FormattingEnabled = true;
            this.orientSelectBox.Items.AddRange(new object[] {
            "Неориент.",
            "Ориент."});
            this.orientSelectBox.Location = new System.Drawing.Point(127, 39);
            this.orientSelectBox.Name = "orientSelectBox";
            this.orientSelectBox.Size = new System.Drawing.Size(77, 21);
            this.orientSelectBox.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.orientSelectBox);
            this.groupBox1.Controls.Add(this.genLabel);
            this.groupBox1.Controls.Add(this.orientLabel);
            this.groupBox1.Controls.Add(this.generateButton);
            this.groupBox1.Controls.Add(this.vertexCountText);
            this.groupBox1.Controls.Add(this.typeSelectBox);
            this.groupBox1.Controls.Add(this.typeLabel);
            this.groupBox1.Location = new System.Drawing.Point(318, 464);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 70);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Генерация графа";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(624, 24);
            this.mainMenu.TabIndex = 11;
            this.mainMenu.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.загрузитьToolStripMenuItem,
            this.toolStripSeparator1,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить...";
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 547);
            this.Controls.Add(this.graphInfoLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.deleteEdgeButton);
            this.Controls.Add(this.addEdgeButton);
            this.Controls.Add(this.renderFrame);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 585);
            this.MinimumSize = new System.Drawing.Size(640, 585);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рендер Графов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
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
        private System.Windows.Forms.ComboBox typeSelectBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label orientLabel;
        private System.Windows.Forms.ComboBox orientSelectBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label graphInfoLabel;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;

    }
}


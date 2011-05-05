namespace GraphsRender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.generateBox = new System.Windows.Forms.GroupBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genRandomEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskOneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertButton = new System.Windows.Forms.Button();
            this.isEdgeButton = new System.Windows.Forms.Button();
            this.edgeOperationsBox = new System.Windows.Forms.GroupBox();
            this.edgeWeightSetButton = new System.Windows.Forms.Button();
            this.taskOneButton = new System.Windows.Forms.Button();
            this.edgeInfoLabel = new System.Windows.Forms.Label();
            this.taskTwoButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.generateBox.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.edgeOperationsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(195, 21);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(93, 40);
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
            this.vertexCountText.Text = "5";
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
            this.addEdgeButton.Location = new System.Drawing.Point(6, 17);
            this.addEdgeButton.Name = "addEdgeButton";
            this.addEdgeButton.Size = new System.Drawing.Size(71, 23);
            this.addEdgeButton.TabIndex = 4;
            this.addEdgeButton.Text = "Добавить";
            this.addEdgeButton.UseVisualStyleBackColor = true;
            this.addEdgeButton.Click += new System.EventHandler(this.addEdgeButton_Click);
            // 
            // deleteEdgeButton
            // 
            this.deleteEdgeButton.Location = new System.Drawing.Point(83, 17);
            this.deleteEdgeButton.Name = "deleteEdgeButton";
            this.deleteEdgeButton.Size = new System.Drawing.Size(63, 23);
            this.deleteEdgeButton.TabIndex = 5;
            this.deleteEdgeButton.Text = "Удалить";
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
            this.typeSelectBox.Location = new System.Drawing.Point(66, 39);
            this.typeSelectBox.Name = "typeSelectBox";
            this.typeSelectBox.Size = new System.Drawing.Size(34, 21);
            this.typeSelectBox.TabIndex = 6;
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(63, 21);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(29, 13);
            this.typeLabel.TabIndex = 7;
            this.typeLabel.Text = "Тип:";
            // 
            // orientLabel
            // 
            this.orientLabel.AutoSize = true;
            this.orientLabel.Location = new System.Drawing.Point(108, 21);
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
            this.orientSelectBox.Location = new System.Drawing.Point(111, 39);
            this.orientSelectBox.Name = "orientSelectBox";
            this.orientSelectBox.Size = new System.Drawing.Size(77, 21);
            this.orientSelectBox.TabIndex = 9;
            // 
            // generateBox
            // 
            this.generateBox.Controls.Add(this.orientSelectBox);
            this.generateBox.Controls.Add(this.genLabel);
            this.generateBox.Controls.Add(this.orientLabel);
            this.generateBox.Controls.Add(this.generateButton);
            this.generateBox.Controls.Add(this.vertexCountText);
            this.generateBox.Controls.Add(this.typeSelectBox);
            this.generateBox.Controls.Add(this.typeLabel);
            this.generateBox.Location = new System.Drawing.Point(318, 480);
            this.generateBox.Name = "generateBox";
            this.generateBox.Size = new System.Drawing.Size(294, 70);
            this.generateBox.TabIndex = 10;
            this.generateBox.TabStop = false;
            this.generateBox.Text = "Генерация графа";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.functionsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(624, 24);
            this.mainMenu.TabIndex = 11;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "&Файл";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = global::GraphsRender.Properties.Resources.OpenSelectedItemHS;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.loadToolStripMenuItem.Text = "&Открыть...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::GraphsRender.Properties.Resources.saveHS;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.saveToolStripMenuItem.Text = "&Сохранить...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.exitToolStripMenuItem.Text = "&Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // functionsToolStripMenuItem
            // 
            this.functionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.genRandomEdgesToolStripMenuItem,
            this.taskOneToolStripMenuItem});
            this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            this.functionsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.functionsToolStripMenuItem.Text = "Ф&ункции";
            // 
            // genRandomEdgesToolStripMenuItem
            // 
            this.genRandomEdgesToolStripMenuItem.Name = "genRandomEdgesToolStripMenuItem";
            this.genRandomEdgesToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.genRandomEdgesToolStripMenuItem.Text = "Генерация случайных рёбер";
            this.genRandomEdgesToolStripMenuItem.Click += new System.EventHandler(this.genRandomEdgesToolStripMenuItem_Click);
            // 
            // taskOneToolStripMenuItem
            // 
            this.taskOneToolStripMenuItem.Name = "taskOneToolStripMenuItem";
            this.taskOneToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.taskOneToolStripMenuItem.Text = "Определение вершин отделимости";
            this.taskOneToolStripMenuItem.Click += new System.EventHandler(this.taskOneToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(513, 26);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(99, 23);
            this.convertButton.TabIndex = 12;
            this.convertButton.Text = "Конвертировать";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // isEdgeButton
            // 
            this.isEdgeButton.Location = new System.Drawing.Point(152, 17);
            this.isEdgeButton.Name = "isEdgeButton";
            this.isEdgeButton.Size = new System.Drawing.Size(70, 23);
            this.isEdgeButton.TabIndex = 13;
            this.isEdgeButton.Text = "Проверить";
            this.isEdgeButton.UseVisualStyleBackColor = true;
            this.isEdgeButton.Click += new System.EventHandler(this.isEdgeButton_Click);
            // 
            // edgeOperationsBox
            // 
            this.edgeOperationsBox.Controls.Add(this.edgeWeightSetButton);
            this.edgeOperationsBox.Controls.Add(this.addEdgeButton);
            this.edgeOperationsBox.Controls.Add(this.isEdgeButton);
            this.edgeOperationsBox.Controls.Add(this.deleteEdgeButton);
            this.edgeOperationsBox.Location = new System.Drawing.Point(12, 500);
            this.edgeOperationsBox.Name = "edgeOperationsBox";
            this.edgeOperationsBox.Size = new System.Drawing.Size(300, 50);
            this.edgeOperationsBox.TabIndex = 14;
            this.edgeOperationsBox.TabStop = false;
            this.edgeOperationsBox.Text = "Операции с рёбрами";
            // 
            // edgeWeightSetButton
            // 
            this.edgeWeightSetButton.Location = new System.Drawing.Point(228, 17);
            this.edgeWeightSetButton.Name = "edgeWeightSetButton";
            this.edgeWeightSetButton.Size = new System.Drawing.Size(66, 23);
            this.edgeWeightSetButton.TabIndex = 14;
            this.edgeWeightSetButton.Text = "Уст. вес";
            this.edgeWeightSetButton.UseVisualStyleBackColor = true;
            this.edgeWeightSetButton.Click += new System.EventHandler(this.edgeWeightSetButton_Click);
            // 
            // taskOneButton
            // 
            this.taskOneButton.Location = new System.Drawing.Point(369, 454);
            this.taskOneButton.Name = "taskOneButton";
            this.taskOneButton.Size = new System.Drawing.Size(121, 23);
            this.taskOneButton.TabIndex = 15;
            this.taskOneButton.Text = "Задача #1 (Вер. отд.)";
            this.taskOneButton.UseVisualStyleBackColor = true;
            this.taskOneButton.Click += new System.EventHandler(this.taskOneButton_Click);
            // 
            // edgeInfoLabel
            // 
            this.edgeInfoLabel.AutoSize = true;
            this.edgeInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.edgeInfoLabel.ForeColor = System.Drawing.Color.DimGray;
            this.edgeInfoLabel.Location = new System.Drawing.Point(12, 454);
            this.edgeInfoLabel.Name = "edgeInfoLabel";
            this.edgeInfoLabel.Size = new System.Drawing.Size(90, 13);
            this.edgeInfoLabel.TabIndex = 16;
            this.edgeInfoLabel.Text = "Выберите ребро";
            // 
            // taskTwoButton
            // 
            this.taskTwoButton.Location = new System.Drawing.Point(496, 454);
            this.taskTwoButton.Name = "taskTwoButton";
            this.taskTwoButton.Size = new System.Drawing.Size(116, 23);
            this.taskTwoButton.TabIndex = 17;
            this.taskTwoButton.Text = "Задача №2 (Круск.)";
            this.taskTwoButton.UseVisualStyleBackColor = true;
            this.taskTwoButton.Click += new System.EventHandler(this.taskTwoButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.resetButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.resetButton.Location = new System.Drawing.Point(496, 454);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(116, 23);
            this.resetButton.TabIndex = 18;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Visible = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 557);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.taskTwoButton);
            this.Controls.Add(this.taskOneButton);
            this.Controls.Add(this.edgeInfoLabel);
            this.Controls.Add(this.edgeOperationsBox);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.graphInfoLabel);
            this.Controls.Add(this.generateBox);
            this.Controls.Add(this.renderFrame);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(630, 585);
            this.MinimumSize = new System.Drawing.Size(630, 585);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рендер Графов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.generateBox.ResumeLayout(false);
            this.generateBox.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.edgeOperationsBox.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox generateBox;
        private System.Windows.Forms.Label graphInfoLabel;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.Button isEdgeButton;
        private System.Windows.Forms.GroupBox edgeOperationsBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genRandomEdgesToolStripMenuItem;
        private System.Windows.Forms.Button taskOneButton;
        private System.Windows.Forms.ToolStripMenuItem taskOneToolStripMenuItem;
        private System.Windows.Forms.Label edgeInfoLabel;
        private System.Windows.Forms.Button edgeWeightSetButton;
        private System.Windows.Forms.Button taskTwoButton;
        private System.Windows.Forms.Button resetButton;

    }
}


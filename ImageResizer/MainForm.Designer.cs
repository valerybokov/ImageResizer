namespace ImageResizer
{
	partial class MainForm
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.bStart = new System.Windows.Forms.Button();
			this.chlbSizes = new System.Windows.Forms.CheckedListBox();
			this.bChooseImage = new System.Windows.Forms.Button();
			this.tbImagePath = new System.Windows.Forms.TextBox();
			this.tbDestinationDirectory = new System.Windows.Forms.TextBox();
			this.bChooseDestinationDirectory = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.chbCheckAll = new System.Windows.Forms.CheckBox();
			this.bOpenDestDirectory = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tbNewFileName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.menu = new System.Windows.Forms.MenuStrip();
			this.miFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.miAddSize = new System.Windows.Forms.ToolStripMenuItem();
			this.miRewriteSameFiles = new System.Windows.Forms.ToolStripMenuItem();
			this.miInterpolationMode = new System.Windows.Forms.ToolStripMenuItem();
			this.miImageFormat = new System.Windows.Forms.ToolStripMenuItem();
			this.miFormat_png = new System.Windows.Forms.ToolStripMenuItem();
			this.miFormat_jpg = new System.Windows.Forms.ToolStripMenuItem();
			this.miFormat_bmp = new System.Windows.Forms.ToolStripMenuItem();
			this.miReference = new System.Windows.Forms.ToolStripMenuItem();
			this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// bStart
			// 
			this.bStart.Image = ((System.Drawing.Image)(resources.GetObject("bStart.Image")));
			this.bStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bStart.Location = new System.Drawing.Point(282, 228);
			this.bStart.Name = "bStart";
			this.bStart.Size = new System.Drawing.Size(77, 33);
			this.bStart.TabIndex = 7;
			this.bStart.Text = "Старт";
			this.bStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.bStart.UseVisualStyleBackColor = true;
			this.bStart.Click += new System.EventHandler(this.Button_Click);
			// 
			// chlbSizes
			// 
			this.chlbSizes.CheckOnClick = true;
			this.chlbSizes.FormattingEnabled = true;
			this.chlbSizes.Location = new System.Drawing.Point(338, 53);
			this.chlbSizes.Name = "chlbSizes";
			this.chlbSizes.Size = new System.Drawing.Size(126, 154);
			this.chlbSizes.TabIndex = 6;
			this.chlbSizes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
			// 
			// bChooseImage
			// 
			this.bChooseImage.Location = new System.Drawing.Point(299, 45);
			this.bChooseImage.Name = "bChooseImage";
			this.bChooseImage.Size = new System.Drawing.Size(33, 23);
			this.bChooseImage.TabIndex = 2;
			this.bChooseImage.Text = "...";
			this.bChooseImage.UseVisualStyleBackColor = true;
			this.bChooseImage.Click += new System.EventHandler(this.Button_Click);
			// 
			// tbImagePath
			// 
			this.tbImagePath.AllowDrop = true;
			this.tbImagePath.Location = new System.Drawing.Point(6, 46);
			this.tbImagePath.Name = "tbImagePath";
			this.tbImagePath.Size = new System.Drawing.Size(278, 20);
			this.tbImagePath.TabIndex = 1;
			this.tbImagePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
			this.tbImagePath.DragOver += new System.Windows.Forms.DragEventHandler(this.TextBox_DragOver);
			this.tbImagePath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
			this.tbImagePath.MouseHover += new System.EventHandler(this.TextBox_MouseHover);
			// 
			// tbDestinationDirectory
			// 
			this.tbDestinationDirectory.AllowDrop = true;
			this.tbDestinationDirectory.Location = new System.Drawing.Point(6, 90);
			this.tbDestinationDirectory.Name = "tbDestinationDirectory";
			this.tbDestinationDirectory.Size = new System.Drawing.Size(239, 20);
			this.tbDestinationDirectory.TabIndex = 3;
			this.tbDestinationDirectory.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
			this.tbDestinationDirectory.DragOver += new System.Windows.Forms.DragEventHandler(this.TextBox_DragOver);
			this.tbDestinationDirectory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
			this.tbDestinationDirectory.MouseHover += new System.EventHandler(this.TextBox_MouseHover);
			// 
			// bChooseDestinationDirectory
			// 
			this.bChooseDestinationDirectory.Location = new System.Drawing.Point(299, 88);
			this.bChooseDestinationDirectory.Name = "bChooseDestinationDirectory";
			this.bChooseDestinationDirectory.Size = new System.Drawing.Size(33, 23);
			this.bChooseDestinationDirectory.TabIndex = 4;
			this.bChooseDestinationDirectory.Text = "...";
			this.bChooseDestinationDirectory.UseVisualStyleBackColor = true;
			this.bChooseDestinationDirectory.Click += new System.EventHandler(this.Button_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Папка для новых изображений:";
			// 
			// chbCheckAll
			// 
			this.chbCheckAll.AutoSize = true;
			this.chbCheckAll.Location = new System.Drawing.Point(341, 30);
			this.chbCheckAll.Name = "chbCheckAll";
			this.chbCheckAll.Size = new System.Drawing.Size(91, 17);
			this.chbCheckAll.TabIndex = 5;
			this.chbCheckAll.Text = "Выбрать все";
			this.chbCheckAll.UseVisualStyleBackColor = true;
			this.chbCheckAll.CheckedChanged += new System.EventHandler(this.Button_Click);
			// 
			// bOpenDestDirectory
			// 
			this.bOpenDestDirectory.Image = ((System.Drawing.Image)(resources.GetObject("bOpenDestDirectory.Image")));
			this.bOpenDestDirectory.Location = new System.Drawing.Point(251, 88);
			this.bOpenDestDirectory.Name = "bOpenDestDirectory";
			this.bOpenDestDirectory.Size = new System.Drawing.Size(33, 22);
			this.bOpenDestDirectory.TabIndex = 8;
			this.bOpenDestDirectory.UseVisualStyleBackColor = true;
			this.bOpenDestDirectory.Click += new System.EventHandler(this.Button_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(131, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Выберите изображение:";
			// 
			// tbNewFileName
			// 
			this.tbNewFileName.Location = new System.Drawing.Point(112, 119);
			this.tbNewFileName.Name = "tbNewFileName";
			this.tbNewFileName.Size = new System.Drawing.Size(220, 20);
			this.tbNewFileName.TabIndex = 5;
			this.tbNewFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
			this.tbNewFileName.MouseHover += new System.EventHandler(this.TextBox_MouseHover);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 122);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Новое имя файла:";
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point(12, 184);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(320, 23);
			this.progress.Step = 1;
			this.progress.TabIndex = 0;
			// 
			// menu
			// 
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miSettings,
            this.miReference});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(474, 24);
			this.menu.TabIndex = 17;
			this.menu.Text = "menuStrip1";
			// 
			// miFile
			// 
			this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExit});
			this.miFile.Name = "miFile";
			this.miFile.Size = new System.Drawing.Size(48, 20);
			this.miFile.Text = "Файл";
			// 
			// miExit
			// 
			this.miExit.Name = "miExit";
			this.miExit.Size = new System.Drawing.Size(152, 22);
			this.miExit.Text = "Выход";
			this.miExit.Click += new System.EventHandler(this.MenuItem_Click);
			// 
			// miSettings
			// 
			this.miSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddSize,
            this.miRewriteSameFiles,
            this.miInterpolationMode,
            this.miImageFormat});
			this.miSettings.Name = "miSettings";
			this.miSettings.Size = new System.Drawing.Size(78, 20);
			this.miSettings.Text = "Настройка";
			// 
			// miAddSize
			// 
			this.miAddSize.Name = "miAddSize";
			this.miAddSize.Size = new System.Drawing.Size(335, 22);
			this.miAddSize.Text = "Добавить размеры";
			this.miAddSize.Click += new System.EventHandler(this.MenuItem_Click);
			// 
			// miRewriteSameFiles
			// 
			this.miRewriteSameFiles.CheckOnClick = true;
			this.miRewriteSameFiles.Name = "miRewriteSameFiles";
			this.miRewriteSameFiles.Size = new System.Drawing.Size(335, 22);
			this.miRewriteSameFiles.Text = "Перезаписывать файлы с одинаковым именем";
			this.miRewriteSameFiles.ToolTipText = resources.GetString("miRewriteSameFiles.ToolTipText");
			// 
			// miInterpolationMode
			// 
			this.miInterpolationMode.Name = "miInterpolationMode";
			this.miInterpolationMode.Size = new System.Drawing.Size(335, 22);
			this.miInterpolationMode.Text = "Алгоритм масштабирования";
			// 
			// miImageFormat
			// 
			this.miImageFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFormat_png,
            this.miFormat_jpg,
            this.miFormat_bmp});
			this.miImageFormat.Name = "miImageFormat";
			this.miImageFormat.Size = new System.Drawing.Size(335, 22);
			this.miImageFormat.Text = "Формат изображения";
			// 
			// miFormat_png
			// 
			this.miFormat_png.Checked = true;
			this.miFormat_png.CheckState = System.Windows.Forms.CheckState.Checked;
			this.miFormat_png.Name = "miFormat_png";
			this.miFormat_png.Size = new System.Drawing.Size(152, 22);
			this.miFormat_png.Text = "PNG";
			this.miFormat_png.Click += new System.EventHandler(this.MenuItem_Click);
			// 
			// miFormat_jpg
			// 
			this.miFormat_jpg.Name = "miFormat_jpg";
			this.miFormat_jpg.Size = new System.Drawing.Size(152, 22);
			this.miFormat_jpg.Text = "JPG";
			this.miFormat_jpg.Click += new System.EventHandler(this.MenuItem_Click);
			// 
			// miFormat_bmp
			// 
			this.miFormat_bmp.Name = "miFormat_bmp";
			this.miFormat_bmp.Size = new System.Drawing.Size(152, 22);
			this.miFormat_bmp.Text = "BMP";
			this.miFormat_bmp.Click += new System.EventHandler(this.MenuItem_Click);
			// 
			// miReference
			// 
			this.miReference.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout});
			this.miReference.Name = "miReference";
			this.miReference.Size = new System.Drawing.Size(65, 20);
			this.miReference.Text = "Справка";
			// 
			// miAbout
			// 
			this.miAbout.Name = "miAbout";
			this.miAbout.Size = new System.Drawing.Size(152, 22);
			this.miAbout.Text = "О программе";
			this.miAbout.Click += new System.EventHandler(this.MenuItem_Click);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 272);
			this.Controls.Add(this.progress);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbNewFileName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.bOpenDestDirectory);
			this.Controls.Add(this.chbCheckAll);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbDestinationDirectory);
			this.Controls.Add(this.bChooseDestinationDirectory);
			this.Controls.Add(this.tbImagePath);
			this.Controls.Add(this.bChooseImage);
			this.Controls.Add(this.chlbSizes);
			this.Controls.Add(this.bStart);
			this.Controls.Add(this.menu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menu;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(490, 310);
			this.MinimumSize = new System.Drawing.Size(490, 310);
			this.Name = "MainForm";
			this.Text = "ImageResizer";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.TextBox_DragOver);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bStart;
		private System.Windows.Forms.CheckedListBox chlbSizes;
		private System.Windows.Forms.Button bChooseImage;
		private System.Windows.Forms.TextBox tbImagePath;
		private System.Windows.Forms.TextBox tbDestinationDirectory;
		private System.Windows.Forms.Button bChooseDestinationDirectory;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chbCheckAll;
		private System.Windows.Forms.Button bOpenDestDirectory;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbNewFileName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.MenuStrip menu;
		private System.Windows.Forms.ToolStripMenuItem miFile;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.ToolStripMenuItem miSettings;
		private System.Windows.Forms.ToolStripMenuItem miAddSize;
		private System.Windows.Forms.ToolStripMenuItem miRewriteSameFiles;
		private System.Windows.Forms.ToolStripMenuItem miReference;
		private System.Windows.Forms.ToolStripMenuItem miAbout;
		private System.Windows.Forms.ToolStripMenuItem miInterpolationMode;
		private System.Windows.Forms.ToolStripMenuItem miImageFormat;
		private System.Windows.Forms.ToolStripMenuItem miFormat_png;
		private System.Windows.Forms.ToolStripMenuItem miFormat_jpg;
		private System.Windows.Forms.ToolStripMenuItem miFormat_bmp;
	}
}


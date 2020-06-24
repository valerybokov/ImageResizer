using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ImageResizer
{
    public partial class MainForm : Form
    {
        #region fields
        readonly Worker worker = new Worker();
        //worker variables
        string path, st, ext;
        System.Drawing.Imaging.ImageFormat mif;
        readonly List<ImageSize> sizes = new List<ImageSize>();
        bool cancel;
        readonly System.Threading.Thread mainThread = System.Threading.Thread.CurrentThread;
        ///////////////////////////////
        
        OpenFileDialog ofd;
        FolderBrowserDialog fbd;
        Bitmap bmp;
        readonly ToolTip tt = new ToolTip();
        internal static readonly string Title = "ImageResizer", Error = "Ошибка";
        bool checkMode = false, changed = false;
        System.Drawing.Drawing2D.InterpolationMode mode;
        EventHandler gotFocusHandler;
        readonly string[]
            extensions = { ".png", ".bmp", ".jpg", ".jpeg", ".jpe" };
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            gotFocusHandler = Control_GotFocus;
            bOpenDestDirectory.GotFocus += gotFocusHandler;
            bChooseDestinationDirectory.GotFocus += gotFocusHandler;
            bChooseImage.GotFocus += gotFocusHandler;
            bStart.GotFocus += gotFocusHandler;

            tt.SetToolTip(bOpenDestDirectory, "Открыть папку");

            var c = chlbSizes.Items;

            c.Add(new ImageSize(16, 16));
            c.Add(new ImageSize(32, 32));
            c.Add(new ImageSize(48, 48));
            c.Add(new ImageSize(72, 72));

            c.Add(new ImageSize(96, 96));
            c.Add(new ImageSize(144, 144));
            c.Add(new ImageSize(192, 192));
            
            tbDestinationDirectory.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            var ci = miInterpolationMode.DropDownItems;
            string []arr = Enum.GetNames(typeof(System.Drawing.Drawing2D.InterpolationMode));

            string[]description = {
                "Задает режим, используемый по умолчанию.",
                "Задает низкокачественную интерполяцию.",
                "Задает высококачественную интерполяцию.",
                "Задает билинейную интерполяцию. Предварит. фильтрация не выполняется.\nЭтот режим не применяется для сжатия изображения до размера менее 50% от его исходного размера.",
                "Задает бикубическую интерполяцию. Предварит. фильтрация не выполняется.\nЭтот режим не применяется для сжатия изображения до размера менее 25% от его исходного размера.",
                "Задает интерполяцию по ближайшим соседним элементам.",
                "Задает высококач-ю билинейную интерполяцию. Выполняется предварительная фильтрация,\nчтобы гарантировать высококачественное сжатие.",
                "Задает высококач-ю бикубическую интерполяцию. Выполняется предварительная фильтрация,\nчтобы гарантировать высококачественное сжатие.\nЭтот режим создает преобразованные изображения самого высокого качества."
                };

            EventHandler evHandler = MenuItem_Click;
            for (int i = 0; i < arr.Length - 1; ++i)//режим Invalid не добавлять
            {
                var it = new ToolStripMenuItem(arr[i]);
                it.ToolTipText = description[i];
                it.Click += evHandler;
                ci.Add(it);
            }

            ((ToolStripMenuItem)ci[7]).Checked = true;

            if (File.Exists("data"))
                LoadSettings();
            
            worker.Task = CreateImages;
        }

        void Control_GotFocus(object sender, EventArgs e)
        {
            Focus();
        }

        private bool ControlsEnabled {
            set {
                tbDestinationDirectory.Enabled = value;
                tbImagePath.Enabled = value;
                tbNewFileName.Enabled = value;

                bChooseDestinationDirectory.Enabled = value;
                bChooseImage.Enabled = value;
                bOpenDestDirectory.Enabled = value;
                miSettings.Enabled = value;
                
                chlbSizes.Enabled = value;//обязательно отключать!
                chbCheckAll.Enabled = value;//обязательно отключать!
            }
        }

        //главный метод
        private void CreateImages()
        {
            cancel = false;
            progress.Value = 0;
            progress.Maximum = sizes.Count;
            ControlsEnabled = false;

            mainThread.Priority = System.Threading.ThreadPriority.BelowNormal;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.AboveNormal;//worker thread priority

            foreach (ImageSize sz in sizes)
            {
                Image im = ResizeImage(sz.width, sz.height, bmp);
                st = String.Format("{0}{1}x{2}.{3}", path, sz.width, sz.height, ext);

                try
                {
                    im.Save(st, mif);
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show(String.Format("Не удалось сохранить файл {0}. Операция прервана.", st), Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    im.Dispose();
                }

                progress.PerformStep();

                if (cancel)
                {
                    MessageBox.Show("Операция прервана.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ControlsEnabled = true;
                    return;
                }
            }

            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
            mainThread.Priority = System.Threading.ThreadPriority.Normal;//worker thread priority

            MessageBox.Show("Операция завершена.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ControlsEnabled = true;
        }

        private void Button_Click(object s, EventArgs e)
        {
            if (s == bChooseDestinationDirectory) {
                if (fbd == null) {
                    fbd = new FolderBrowserDialog();
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                    tbDestinationDirectory.Text = fbd.SelectedPath;
            }else
            if (s == bChooseImage){
                if (ofd == null) {
                    ofd = new OpenFileDialog();
                    ofd.Filter = "PNG (*.png)|*.png|BMP (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg;*.jpeg;*.jpe";
                    ofd.Title = "Выберите изображение";
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                    SetImageData(ofd.FileName);
            }else
            if (s == bOpenDestDirectory){
                if (tbDestinationDirectory.TextLength == 0)
                {
                    MessageBox.Show("Выберите путь к папке для новых изображений", Title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                try
                {
                    System.Diagnostics.Process.Start(tbDestinationDirectory.Text);
                }
                catch {
                    MessageBox.Show("Не удалось открыть папку", Title);
                }
            }else
            if (s == bStart){
                var checkedItems = chlbSizes.CheckedItems;

                if (checkedItems.Count == 0)
                {
                    MessageBox.Show("Выберите один или несколько размеров для изображения", Title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                if (tbDestinationDirectory.TextLength == 0)
                {
                    MessageBox.Show("Выберите путь к папке для новых изображений", Title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                if (tbImagePath.TextLength == 0)
                {
                    MessageBox.Show("Сначала выберите изображение", Title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                if (tbNewFileName.TextLength == 0)
                {
                    MessageBox.Show("Сначала введите новое имя изображения", Title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                var c_mi = miImageFormat.DropDownItems;
                int i = 0;
                for (; i < c_mi.Count; ++i)
                    if (((ToolStripMenuItem)c_mi[i]).Checked) break;

                switch (i)
                {
                    case 0://png
                        mif = System.Drawing.Imaging.ImageFormat.Png;
                        ext = "png";
                        break;
                    case 1://jpg
                        mif = System.Drawing.Imaging.ImageFormat.Jpeg;
                        ext = "jpg";
                        break;
                    default://bmp
                        mif = System.Drawing.Imaging.ImageFormat.Bmp;
                        ext = "bmp";
                        break;
                }

                path = String.Format("{0}\\{1}", tbDestinationDirectory.Text, tbNewFileName.Text);

                sizes.Clear();
                //перезаписывать файлы с одинаковым именем не спрашивая?
                bool rewriteSameFiles = miRewriteSameFiles.Checked;

                for (i = 0; i < checkedItems.Count; ++i)//если файл существует, что с ним делать
                {
                    var sz = (ImageSize)checkedItems[i];                    

                    if (File.Exists(st = String.Format("{0}{1}x{2}.{3}", path, sz.width, sz.height, ext)))
                    {
                        var changeFile = MessageBox.Show(
                                String.Format("Файл {0} существует. Заменить его?", st),
                                Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rewriteSameFiles || changeFile == DialogResult.Yes)
                            try
                            {
                                File.Delete(st);
                                sizes.Add(sz);
                            }
                            catch (DirectoryNotFoundException)
                            {
                                MessageBox.Show(String.Format("Операция прервана. Папка {0} не найдена!", Path.GetDirectoryName(st)),
                                    Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            catch (PathTooLongException)
                            {
                                MessageBox.Show("Операция прервана. Слишком длинный путь!",
                                    Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            catch (IOException)
                            {
                                DialogResult dr = MessageBox.Show(
                                    String.Format("Не удалось удалить файл {0}! Возможно он открыт в другой программе.", st),
                                    Error, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);

                                switch (dr)
                                {
                                    case DialogResult.Abort: return;
                                    case DialogResult.Ignore: continue;
                                    case DialogResult.Retry:
                                        --i;
                                        continue;
                                }
                            }catch(Exception)
                            {
                                MessageBox.Show("Операция прервана. Не удалось удалить файл!",
                                    Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                    }
                    else
                        sizes.Add(sz);
                }
                
                if (sizes.Count == 0) return;

                if (worker.IsWorking)//note new
                    if (MessageBox.Show(
                        "Остановить выполнение операции?", Title,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        cancel = true;
                    else
                        return;

                worker.StartTask();
            }
            else
            if (s == chbCheckAll)
            {
                if (checkMode) return;

                if (chbCheckAll.Checked)
                {
                    checkMode = true;

                    for (int i = chlbSizes.Items.Count - 1; i > -1; --i)
                        chlbSizes.SetItemChecked(i, chbCheckAll.Checked);

                    checkMode = false;
                }
            }
        }

        private void LoadSettings() 
        {
            BinaryReader br = null;

            try
            {
                var fs = new FileStream("data", FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);

                int count = br.ReadInt32();
                var c = chlbSizes.Items;

                for (int i = 0; i < count; ++i)
                {
                    ImageSize m;
                    m.width = br.ReadInt32();
                    m.height = br.ReadInt32();
                    c.Add(m);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка при попытке чтения файла настроек", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                if (br != null)
                    br.Dispose();
            }
        }

        private void SaveSettings()
        {
            BinaryWriter bw = null;

            try
            {
                var fs = new FileStream("data", FileMode.OpenOrCreate, FileAccess.Write);
                bw = new BinaryWriter(fs);
                var c = chlbSizes.Items;
                //7 - индекс размера, с которого необходимо сохранять (остальные загружаются в OnLoad())
                bw.Write(c.Count - 7);

                for (int i = 7; i < c.Count; ++i){
                    var m = (ImageSize)c[i];
                    bw.Write(m.width);
                    bw.Write(m.height);
                }

                bw.Flush();
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка при попытке сохранения настроек", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                if (bw != null)
                    bw.Dispose();
            }
        }

        private void SetImageData(string filename)
        {
            tbImagePath.Text = filename;
            tbNewFileName.Text = Path.GetFileNameWithoutExtension(filename);

            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }

            bmp = new Bitmap(filename);
            Text = String.Format("ImageResizer (ш: {0} в: {1})", bmp.Width, bmp.Height);
        }

        private Image ResizeImage(int nWidth, int nHeight, Image source)
        {
            var result = new Bitmap(nWidth, nHeight);

            result.SetResolution(source.HorizontalResolution, source.VerticalResolution);
            using (Graphics g = Graphics.FromImage(result))
            {
                //System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic - лучшее качество
                g.InterpolationMode = mode;
                g.DrawImage(source, 0, 0, nWidth, nHeight);
            }

            return result;
        }

        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!checkMode) chbCheckAll.Checked = false;
            else
            if (e.NewValue == CheckState.Unchecked &&
                chlbSizes.CheckedIndices.Count != chlbSizes.Items.Count)
                chbCheckAll.Checked = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (worker.IsWorking)
                if (MessageBox.Show("Остановить выполнение операции?", Title, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    cancel = true;
                else
                {
                    e.Cancel = true;
                    return;
                }

            worker.Stop();
            tt.Dispose();

            var c = miInterpolationMode.DropDownItems;
            ToolStripMenuItem it;
            for (int i = 0; i < c.Count; ++i)
            {
                it = (ToolStripMenuItem)c[i];
                it.Click -= MenuItem_Click;
                it.Dispose();
            }

            if (fbd != null) fbd.Dispose();
            if (ofd != null) ofd.Dispose();
            if (bmp != null) bmp.Dispose();

            if (changed) SaveSettings();

            bOpenDestDirectory.GotFocus -= gotFocusHandler;
            bChooseDestinationDirectory.GotFocus -= gotFocusHandler;
            bChooseImage.GotFocus -= gotFocusHandler;
            bStart.GotFocus -= gotFocusHandler;

            base.OnClosing(e);
        }

        private void TextBox_MouseHover(object s, EventArgs e)
        {
            var tb = (TextBox)s;
            tt.SetToolTip(tb, tb.Text);
        }

        private void TextBox_DragOver(object s, DragEventArgs e)
        {
            var data = e.Data;

            if (s == this) 
            {
                if (data.GetDataPresent(DataFormats.Text)) 
                {
                    if (File.Exists((string)data.GetData(DataFormats.Text)))
                        TextBox_DragOver(tbImagePath, e);
                    else
                    if (Directory.Exists((string)data.GetData(DataFormats.Text)))
                        TextBox_DragOver(tbDestinationDirectory, e);
                }else
                if (data.GetDataPresent(DataFormats.FileDrop)) 
                {
                    //это файл?
                    TextBox_DragOver(tbImagePath, e);

                    if (e.Effect != DragDropEffects.Move)
                        TextBox_DragOver(tbDestinationDirectory, e);                    
                }

                if (e.Effect == DragDropEffects.Move) return;
            }else
            if (s == tbImagePath)
            {
                if (data.GetDataPresent(DataFormats.FileDrop))
                {
                    if (IsImageFile(data) != null)
                    {
                        e.Effect = DragDropEffects.Move;
                        return;
                    }
                }
                else
                if (data.GetDataPresent(DataFormats.Text))
                {
                    if (File.Exists((string)data.GetData(DataFormats.Text)))
                    {
                        e.Effect = DragDropEffects.Move;
                        return;
                    }
                }
            }
            else
            if (s == tbDestinationDirectory)
                if (data.GetDataPresent(DataFormats.Text))
                {
                    if (Directory.Exists((string)data.GetData(DataFormats.Text)))
                    {
                        e.Effect = DragDropEffects.Move;
                        return;
                    }
                }
                else
                if (data.GetDataPresent(DataFormats.FileDrop))
                {
                    var fls = (string[])data.GetData(DataFormats.FileDrop);

                    for (int i = 0; i < fls.Length; ++i)
                        if (Directory.Exists(fls[i]))
                        {
                            e.Effect = DragDropEffects.Move;
                            return;
                        }
                }

            e.Effect = DragDropEffects.None;
        }

        private void TextBox_DragDrop(object s, DragEventArgs e)
        {
            var data = e.Data;

            if (s == this)
            {
                if (data.GetDataPresent(DataFormats.FileDrop))
                {
                    string rez = IsImageFile(data);

                    if (rez == null)
                        TextBox_DragDrop(tbDestinationDirectory, e);
                    else
                        SetImageData(rez);
                }
                else
                if (data.GetDataPresent(DataFormats.Text))
                {
                    string st = (string)data.GetData(DataFormats.Text);

                    if (File.Exists(st))
                        SetImageData(st);
                    else
                        tbDestinationDirectory.Text = st;
                }
            }
            else
            if (s == tbImagePath)
            {
                if (data.GetDataPresent(DataFormats.FileDrop))
                {
                    string st = IsImageFile(data);
                    SetImageData(st);
                }
                else
                if (data.GetDataPresent(DataFormats.Text))
                    SetImageData((string)data.GetData(DataFormats.Text));
            }
            else
            if (s == tbDestinationDirectory)
                if (data.GetDataPresent(DataFormats.Text))
                    tbDestinationDirectory.Text = (string)data.GetData(DataFormats.Text);
                else
                if (data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] fls = (string[])data.GetData(DataFormats.FileDrop);

                    for (int i = 0; i < fls.Length; ++i)
                        if (Directory.Exists(fls[i]))
                        {
                            tbDestinationDirectory.Text = fls[i];
                            return;
                        }
                }
        }

        private string IsImageFile(IDataObject data)
        {
            string[] files = (string[])data.GetData(DataFormats.FileDrop);

            for (int i = 0, j; i < files.Length; ++i)
                for (j = 0; j < extensions.Length; ++j)
                    if (files[i].EndsWith(extensions[j], StringComparison.CurrentCultureIgnoreCase))
                        return files[i];

            return null;
        }

        private void MenuItem_Click(object s, EventArgs e)
        {
            if (s == miAbout)
            {
                using (var a = new AboutBox())
                    a.ShowDialog();
            }else
            if (s == miAddSize)
            {
                int w, h;

                using (var f = new AddSizeForm())
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        f.InitSizes(out w, out h);
                        ImageSize isz;
                        isz.width = w;
                        isz.height = h;
                        chlbSizes.Items.Add(isz);
                        chbCheckAll.Checked = false;
                        changed = true;
                    }
            }else
            if (s == miExit)
                Close();
            else
            if (s == miFormat_png)
                SetCheckedImageFormat(miFormat_png);
            else
            if (s == miFormat_jpg)
                SetCheckedImageFormat(miFormat_jpg);
            else
            if (s == miFormat_bmp)
                SetCheckedImageFormat(miFormat_bmp);
            else
            {
                if (checkMode) return;

                checkMode = true;

                //выбран режим интерполяции
                var c = miInterpolationMode.DropDownItems;
                ToolStripMenuItem it;
                for (int i = 0; i < c.Count; ++i)
                {
                    it = (ToolStripMenuItem)c[i];

                    if (it == s)
                    {                        
                        mode = (System.Drawing.Drawing2D.InterpolationMode)Enum.Parse(typeof(System.Drawing.Drawing2D.InterpolationMode), it.Text);
                        it.Checked = true;
                    }
                    else
                        it.Checked = false;
                }

                checkMode = false;
            }
        }

        private void SetCheckedImageFormat(ToolStripMenuItem mi)
        {
            if (checkMode) return;

            checkMode = true;

            var c = miImageFormat.DropDownItems;

            for (int i = 0; i < c.Count; ++i)
                ((ToolStripMenuItem)c[i]).Checked = c[i] == mi;

            checkMode = false;
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData) 
            {
                case Keys.Control | Keys.Add:
                case Keys.Control | Keys.Oemplus:    MenuItem_Click(miAddSize, EventArgs.Empty);                    break;
                case Keys.Control | Keys.A:            chbCheckAll.Checked = !chbCheckAll.Checked;                    break;
                case Keys.Control | Keys.O:            Button_Click(bChooseDestinationDirectory, EventArgs.Empty);    break;
                case Keys.Control | Keys.I:            Button_Click(bChooseImage, EventArgs.Empty);                break;
                case Keys.Control | Keys.Enter:        Button_Click(bStart, EventArgs.Empty);                        break;
            }
        }
    }
}
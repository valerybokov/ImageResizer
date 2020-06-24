using System;
using System.Windows.Forms;

namespace ImageResizer
{
    public partial class AddSizeForm : Form
    {
        public AddSizeForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object s, EventArgs e)
        {
            if (tbWidth.TextLength == 0 || tbHeight.TextLength == 0)
                MessageBox.Show("Заполните текстовые поля", MainForm.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                DialogResult = DialogResult.OK;
        }

        internal void InitSizes(out int w, out int h)
        {
            w = Convert.ToInt32(tbWidth.Text);
            h = Convert.ToInt32(tbHeight.Text);
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)//27 - escape
            {
                DialogResult = DialogResult.None;
                Close();
            }
            else
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)//8 - backspace
                e.Handled = true;
        }
    }
}
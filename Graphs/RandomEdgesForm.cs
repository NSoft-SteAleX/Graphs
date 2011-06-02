using System;
using System.Windows.Forms;

namespace GraphsRender
{
    public partial class RandomEdgesForm : Form
    {
        public int RandomEdgesCount { get; set; }
        public bool NeedToSave { get; set; }

        public RandomEdgesForm()
        {
            InitializeComponent();
            NeedToSave = false;
        }
        private void generateButton_Click(object sender, EventArgs e)
        {
            if (randomEdgesText.Text.Length <= 0) return;

            RandomEdgesCount = Convert.ToInt32(randomEdgesText.Text);
            NeedToSave = true;
            Close();
        }
        private void randomEdgesText_TextChanged(object sender, EventArgs e)
        {
            if (randomEdgesText.Text.Length > 0)
            {
                try
                {
                    Convert.ToInt32(randomEdgesText.Text);
                }
                catch
                {
                    s.ShowWarning("Введите числовое значение!");
                    randomEdgesText.Text = "0";
                }
            }
        }
    }
}

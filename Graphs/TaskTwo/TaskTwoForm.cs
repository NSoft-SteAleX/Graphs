using System;
using System.Windows.Forms;

namespace GraphsRender.TaskTwo
{
    public partial class TaskTwoForm : Form
    {
        public double EdgeWeight { get; private set; }
        public bool NeedToSave { get; set; }

        public TaskTwoForm()
        {
            InitializeComponent();
            NeedToSave = false;
        }

        private void edgeWeightText_TextChanged(object sender, EventArgs e)
        {
            if (edgeWeightText.Text.Length > 0)
            {
                edgeWeightText.Text = edgeWeightText.Text.Replace('.', ',');
                edgeWeightText.SelectionStart = edgeWeightText.Text.Length;

                try
                {
                    Convert.ToDouble(edgeWeightText.Text);
                }
                catch
                {
                    s.ShowWarning("Введите числовое значение!");
                    edgeWeightText.Text = "";
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (edgeWeightText.Text.Length <= 0) return;

            EdgeWeight = Convert.ToDouble(edgeWeightText.Text);
            NeedToSave = true;
            Close();
        }
    }
}

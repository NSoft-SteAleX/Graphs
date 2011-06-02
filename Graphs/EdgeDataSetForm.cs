using System;
using System.Windows.Forms;
using GraphsRender.Graph;

namespace GraphsRender
{
    public partial class EdgeDataSetForm : Form
    {
        public EdgeDescriptor Descriptor { get; set; }
        public bool NeedToSave { get; set; }

        public EdgeDataSetForm()
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

            Descriptor = new EdgeDescriptor(Convert.ToDouble(edgeWeightText.Text));
            NeedToSave = true;
            Close();
        }
    }
}

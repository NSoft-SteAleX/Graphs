using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace Graphs
{
    public partial class MainForm : Form
    {
        private int edgeSize = 40;
        private int lineWidth = 2;
        private int vertexOne = -1, vertexTwo = -1;
        private Graphics gdi;
        private SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph = new SimpleStaticGraph<VertexDescriptor,EdgeDescriptor>();

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Установка начальных значений при загрузке
        /// формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            typeSelectBox.SelectedItem = typeSelectBox.Items[0];
            orientSelectBox.SelectedItem = orientSelectBox.Items[0];

            gdi = renderFrame.CreateGraphics();
            gdi.SmoothingMode = SmoothingMode.HighQuality;
            gdi.CompositingQuality = CompositingQuality.HighQuality;
            PrintGraph();
        }

        /// <summary>
        /// Генерация графа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateButton_Click(object sender, EventArgs e)
        {
            if (vertexCountText.Text.Length > 0)
            {
                GraphFormat format;
                GraphType type;
                string _format, _type;

                if (typeSelectBox.SelectedIndex == 0)
                {
                    format = GraphFormat.MatrixGraph;
                    _format = "Матрица смежностей";
                }
                else
                {
                    format = GraphFormat.ListGraph;
                    _format = "Список смежностей";
                }

                if (orientSelectBox.SelectedIndex == 0)
                {
                    type = GraphType.NotOriented;
                    _type = "Неориентированный";
                }
                else
                {
                    type = GraphType.Oriented;
                    _type = "Ориентированный";
                }

                //Создаём объект графа
                graph = new SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>(Convert.ToInt32(vertexCountText.Text), type, format);

                //Установка лейбла с инфо
                graphInfoLabel.Text = "Вершин: " + vertexCountText.Text + ", " + _format + ", " + _type;

                PrintGraph();
            }
            else
            {
                MessageBox.Show("Введите число вершин.");
            }
        }

        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEdgeButton_Click(object sender, EventArgs e)
        {
            if (vertexOne != -1 && vertexTwo != -1)
            {
                graph.InsertEdge(vertexOne, vertexTwo);
                vertexOne = vertexTwo = -1;
            }

            PrintGraph();
        }

        /// <summary>
        /// Удаление ребра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteEdgeButton_Click(object sender, EventArgs e)
        {
            if (vertexOne != -1 && vertexTwo != -1)
            {
                graph.DeleteEdge(vertexOne, vertexTwo);
                vertexOne = vertexTwo = -1;
            }

            PrintGraph();
        }

        /// <summary>
        /// Рендеринг графа
        /// </summary>
        private void PrintGraph()
        {
            double divconst = graph.VertexCount() / 2d;
            gdi.Clear(Color.Gray);
            renderFrame.Controls.Clear();
            int currx = 0, curry = 0;

            //Отрисовка вершин
            for (int i = 0; i != graph.VertexCount(); i++)
            {
                Label l = new Label();

                l.Text = i.ToString();
                l.Size = new Size(edgeSize, edgeSize);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.MouseClick += new MouseEventHandler(l_MouseClick);

                Panel p = new Panel();

                p.Controls.Add(l);
                p.Name = i.ToString();

                if (i == vertexOne || i == vertexTwo)
                {
                    p.BackgroundImage = GetImageFromPath("layout\\SelectedEdge.png");
                }
                else
                {
                    p.BackgroundImage = GetImageFromPath("layout\\NormalEdge.png");
                }

                p.BackgroundImageLayout = ImageLayout.Zoom;
                p.BackColor = Color.Transparent;
                p.MouseClick += new MouseEventHandler(p_MouseClick);
                p.Size = new Size(edgeSize, edgeSize);
                currx = (int)(275 + Math.Cos((Math.PI / divconst) * i) * 170);
                curry = (int)(180 - Math.Sin((Math.PI / divconst) * i) * 155);
                p.Location = new Point(currx, curry);

                renderFrame.Controls.Add(p);
            }

            //Отрисовка рёбер
            Pen pen = new Pen(Brushes.White, 1);
            for (int i = 0; i != graph.VertexCount(); i++)
            {
                var iter = new SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.Iterator(ref graph, i);
                iter.Begin();

                while (iter.Current() != -1)
                {
                    Control c1 = renderFrame.Controls[i];
                    Control c2 = renderFrame.Controls[iter.Current()];
                    Point p1 = c1.Location + new Size(c1.Size.Width / 2, c1.Size.Height / 2);
                    Point p2 = c2.Location + new Size(c2.Size.Width / 2, c2.Size.Height / 2);
                    gdi.DrawLine(pen, p1, p2);

                    iter.Next();
                }
            }
        }

        #region Обработка нажатий на вершины
        private void l_MouseClick(object sender, MouseEventArgs e)
        {
            int clickedindex = Convert.ToInt32((sender as Label).Text); // edge id, where mouse has been clicked
            if (vertexOne == -1) { vertexOne = clickedindex; PrintGraph(); return; }
            if (vertexTwo == -1) { vertexTwo = clickedindex; PrintGraph(); return; }
            vertexOne = vertexTwo = -1;
            PrintGraph();
        }
        private void p_MouseClick(object sender, MouseEventArgs e)
        {
            int clickedindex = Convert.ToInt32((sender as Panel).Name); // edge id, where mouse has been clicked
            if (vertexOne == -1) { vertexOne = clickedindex; PrintGraph(); return; }
            if (vertexTwo == -1) { vertexTwo = clickedindex; PrintGraph(); return; }
            vertexOne = vertexTwo = -1;
            PrintGraph();
        }
        #endregion

        /// <summary>
        /// Получение изображения по заданному пути
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private Image GetImageFromPath(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            Image img = Image.FromStream(fs);
            fs.Close();
            return img;
        }

        /// <summary>
        /// Проверка введённого числа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vertexCountText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(vertexCountText.Text);
            }
            catch
            {
                MessageBox.Show("Введите числовое значение!");
                vertexCountText.Text = "0";
            }
        }
    }
}

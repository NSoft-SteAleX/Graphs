using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using GraphsRender.Graph;
using GraphsRender.TaskOne;
using GraphsRender.TaskTwo;

namespace GraphsRender
{
    public partial class s : Form
    {
        /// <summary>
        /// Объект графа
        /// </summary>
        private SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> _graph = new SimpleStaticGraph<VertexDescriptor,EdgeDescriptor>();

        /// <summary>
        /// Объект итератора графа
        /// </summary>
        private SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.Iterator _iterator;

        /// <summary>
        /// Объект графа для бэкапов во время
        /// выполнения задач
        /// </summary>
        private SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> _backupGraph;

        /// <summary>
        /// Графический объект для 2D
        /// </summary>
        private Graphics _gdi;

        /// <summary>
        /// Начальная и конечная точки (выбранные)
        /// </summary>
        private int _vertexOne = -1, _vertexTwo = -1;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public s()
        {
            InitializeComponent();
            _iterator = null;
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

            _gdi = renderFrame.CreateGraphics();
            _gdi.SmoothingMode = SmoothingMode.HighQuality;
            _gdi.CompositingQuality = CompositingQuality.HighQuality;
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
                var format = typeSelectBox.SelectedIndex == 0 ? GraphFormat.MatrixGraph : GraphFormat.ListGraph;
                var type = orientSelectBox.SelectedIndex == 0 ? GraphType.NotOriented : GraphType.Oriented;

                //Создаём объект графа
                _graph = new SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>(Convert.ToInt32(vertexCountText.Text), 
                    type, format);
                
                PrintGraph();
            }
            else
            {
                ShowWarning("Введите число вершин.");
            }
        }

        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEdgeButton_Click(object sender, EventArgs e)
        {
            if (_vertexOne != -1 && _vertexTwo != -1)
            {
                if(!_graph.InsertEdge(_vertexOne, _vertexTwo))
                {
                    ShowWarning("Ребро между вершинами " + _vertexOne + " и " + _vertexTwo + " уже существует!");
                }
                _vertexOne = _vertexTwo = -1;
            }
            else
            {
                ShowWarning("Выберите начальную и конечную вершину.");
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
            if (_vertexOne != -1 && _vertexTwo != -1)
            {
                if(!_graph.DeleteEdge(_vertexOne, _vertexTwo))
                {
                    ShowWarning("Ребра между вершинами "+_vertexOne+" и "+_vertexTwo+" не существует!");
                }
                _vertexOne = _vertexTwo = -1;
            }
            else
            {
                ShowWarning("Выберите начальную и конечную вершину.");
            }

            PrintGraph();
        }

        /// <summary>
        /// Проверка существования ребра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isEdgeButton_Click(object sender, EventArgs e)
        {
            if (_vertexOne == -1 || _vertexTwo == -1)
            {
                ShowWarning("Выберите начальную и конечную вершины");
                return;
            }

            if (_graph.IsEdge(_vertexOne, _vertexTwo))
            {
                MessageBox.Show("Ребро между вершинами " + _vertexOne + " и " + _vertexTwo + " существует");
            }
            else
            {
                MessageBox.Show("Ребра между вершинами " + _vertexOne + " и " + _vertexTwo + " не существует");
            }
        }

        /// <summary>
        /// Установка данных ребра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edgeWeightSetButton_Click(object sender, EventArgs e)
        {
            if(_vertexOne != -1 && _vertexTwo != -1 && _graph.IsEdge(_vertexOne, _vertexTwo))
            {
                var form = new EdgeDataSetForm();
                form.ShowDialog();

                if(form.NeedToSave)
                {
                    _graph.SetEdge(_vertexOne, _vertexTwo, form.Descriptor);
                }

                _vertexOne = _vertexTwo = -1;
                PrintGraph();
            }
            else
            {
                ShowWarning("Выберите ребро.");
            }
        }

        /// <summary>
        /// Рендеринг графа
        /// </summary>
        private void PrintGraph()
        {
            double divconst = _graph.VertexCount() / 2d;
            var points = new List<Rectangle>();
            var weights = new List<KeyValuePair<double, Rectangle>>();

            _gdi.Clear(Color.Gray);
            renderFrame.Controls.Clear();

            //Отрисовка вершин
            for (int i = 0; i != _graph.VertexCount(); i++)
            {
                var l = new Label {
                    Text = i.ToString(),
                    Size = new Size(40, 40),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                l.MouseClick += vertex_Click;

                var p = new Panel();
                p.Controls.Add(l);
                p.Name = i.ToString();

                if (i == _vertexOne || i == _vertexTwo)
                {
                    p.BackgroundImage = GetImageFromPath("layout\\SelectedVertex.png");
                }
                else if(_graph.GetVertex(i).Color == Colors.Red)
                {
                    p.BackgroundImage = GetImageFromPath("layout\\SeparationVertex.png");
                }
                else
                {
                    p.BackgroundImage = GetImageFromPath("layout\\NormalVertex.png");
                }

                p.BackgroundImageLayout = ImageLayout.Zoom;
                p.BackColor = Color.Transparent;
                p.MouseClick += vertex_Click;
                p.Size = new Size(40, 40);
                var currx = (int)(275 + Math.Cos((Math.PI / divconst) * i) * 170);
                var curry = (int)(180 - Math.Sin((Math.PI / divconst) * i) * 155);
                p.Location = new Point(currx, curry);

                renderFrame.Controls.Add(p);
            }

            //Отрисовка рёбер
            var pen = new Pen(Brushes.White, 1);
            var brush = Brushes.AliceBlue;

            for (int i = 0; i != _graph.VertexCount(); i++)
            {
                var iter = new SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.Iterator(_graph, i);
                iter.Begin();

                while (iter.Current() != -1)
                {
                    //Установка цвета ребра
                    if(iter.GetCurrentEdge().Data.Color == Colors.Blue)
                    {
                        pen.Color = Color.Blue;
                    }
                    else
                    {
                        pen.Color = Color.White;
                    }

                    Control c1 = renderFrame.Controls[i];
                    Control c2 = renderFrame.Controls[iter.Current()];
                    Point p1 = c1.Location + new Size(c1.Size.Width / 2, c1.Size.Height / 2);
                    Point p2 = c2.Location + new Size(c2.Size.Width / 2, c2.Size.Height / 2);

                    //Установка полигонов для отображения весов и направлений
                    var r1 = new Rectangle(p2 - new Size((int) (0.2*(p2.X - p1.X)) + 5,
                        (int) (0.2*(p2.Y - p1.Y)) + 5), new Size(10, 10));

                    var distance = 0.5; //Удаление от вершины
                    if(_graph.Direction() == GraphType.Oriented)
                    {
                        distance = 0.4;
                    }

                    var r2 = new Rectangle(p2 - new Size((int)(distance * (p2.X - p1.X)) + 5,
                        (int)(distance * (p2.Y - p1.Y)) + 5), new Size(20, 12));

                    points.Add(r1);
                    weights.Add(new KeyValuePair<double, Rectangle>(iter.GetCurrentEdge().Data.Weight, r2));

                    //Отрисовка линии
                    _gdi.DrawLine(pen, p1, p2);

                    iter.Next();
                }
            }

            //Рисуем точки для указания направления ребра
            //Solution made by Nik
            if (_graph.Direction() == GraphType.Oriented)
            {
                foreach (var r in points)
                {
                    _gdi.FillEllipse(brush, r);
                }
            }

            foreach (var d in weights)
            {
                _gdi.FillRectangle(Brushes.Gray, d.Value);
                _gdi.DrawString(d.Key.ToString(), new Font("Arial", 8), brush, d.Value);
            }

            UpdateGraphLabel();
        }

        /// <summary>
        /// Обработка нажатиия на вершину
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vertex_Click(object sender, MouseEventArgs e)
        {
            int index = Convert.ToInt32(((Label)sender).Text);

            if (_vertexOne == -1) 
            { 
                _vertexOne = index; 
                PrintGraph(); 
                return; 
            }
            if (_vertexTwo == -1) 
            { 
                _vertexTwo = index; 
                PrintGraph(); 
                return; 
            }

            _vertexOne = _vertexTwo = -1;
            PrintGraph();
        }

        /// <summary>
        /// Получение изображения по заданному пути
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Image GetImageFromPath(string path)
        {
            var fs = new FileStream(path, FileMode.Open);
            var img = Image.FromStream(fs);
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
                ShowWarning("Введите числовое значение!");
                vertexCountText.Text = "0";
            }
        }

        /// <summary>
        /// Конвертация типа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convertButton_Click(object sender, EventArgs e)
        {
            _graph.Convert();
            _iterator = null;
            setIteratorVertex.Text = "?";
            PrintGraph();
        }

        /// <summary>
        /// Обноление инфо-лейбла
        /// </summary>
        private void UpdateGraphLabel()
        {
            string _format = _graph.Dense() == GraphFormat.MatrixGraph ? "Матрица смежностей" : "Список смежностей";
            string _type = _graph.Direction() == GraphType.NotOriented ? "Неориентированный" : "Ориентированный";

            //Установка лейбла с инфо о графе
            graphInfoLabel.Text = "K: " + Math.Round(_graph.GetCoefficient(), 2) + ", Вершин: " + _graph.VertexCount() + 
                ", Рёбер: "+_graph.EdgeCount()+", " + _format + ", " + _type;

            //Установка лейбла с инфо о ребре
            if(_vertexOne != -1 && _vertexTwo != -1 && _graph.IsEdge(_vertexOne, _vertexTwo))
            {
                edgeInfoLabel.Text = "Вес ребра: " + _graph.GetEdgeData(_vertexOne, _vertexTwo).Weight;
            }
            else
            {
                edgeInfoLabel.Text = "Выберите ребро";
            }
        }

        #region Обработка эвентов меню
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void genRandomEdgesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_graph.VertexCount() > 0)
            {
                var form = new RandomEdgesForm();
                form.ShowDialog();

                if (form.RandomEdgesCount > 0 && form.NeedToSave)
                {
                    _graph = new SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>(_graph.VertexCount(), form.RandomEdgesCount,
                        _graph.Direction(), _graph.Dense());
                }

                PrintGraph();
            }
            else
            {
                ShowWarning("Создайте граф с ненулевым числом вершин.");
            }
        }
        private void taskOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskOneButton_Click(sender, e);
        }
        private void taskTwoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskTwoButton_Click(sender, e);
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = dlg.FileName;
                    var binFormat = new BinaryFormatter();

                    using (Stream fStream = File.OpenRead(file))
                    {
                        _graph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>) binFormat.Deserialize(fStream);
                    }

                    PrintGraph();
                }
                catch
                {
                    ShowWarning("Некорректный файл графа.");
                }
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog()
            {
                Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*"
            };

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = dlg.FileName;
                    var binFormat = new BinaryFormatter();

                    using (Stream fStream = new FileStream(file, FileMode.Create, 
                        FileAccess.Write, FileShare.None))
                    {
                        binFormat.Serialize(fStream, _graph);
                    }

                    PrintGraph();
                }
                catch (Exception)
                {
                    ShowWarning("Некорректный файл графа.");
                }
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new AboutBox();
            box.Show();
        }
        #endregion

        #region Обработка задач
        /// <summary>
        /// Решение "Задачи 1"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskOneButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetVertexColors();
                _vertexOne = _vertexTwo = -1;
                var taskOne = new TaskOne.TaskOne(_graph);
                _backupGraph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>) _graph.Clone();
                _graph = taskOne.Result();
                BlockUI();
                PrintGraph();
            }
            catch(DataException ex)
            {
                ShowWarning(ex.Message);
            }
        }

        /// <summary>
        /// Решение "Задачи 2"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskTwoButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetVertexColors();
                _vertexOne = _vertexTwo = -1;
                var form = new TaskTwoForm();
                form.ShowDialog();

                if (form.NeedToSave)
                {
                    var taskTwo = new TaskTwo.TaskTwo(_graph, form.EdgeWeight);
                    _backupGraph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>) _graph.Clone();
                    _graph = taskTwo.Result();
                    BlockUI();
                    PrintGraph();
                }
            }
            catch (DataException ex)
            {
                ShowWarning(ex.Message);
            }
        }

        /// <summary>
        /// Блокировка основных элементов интерфейса
        /// </summary>
        private void BlockUI()
        {
            resetButton.Visible = true;
            taskTwoButton.Visible = false;
            iteratorOperationsBox.Enabled = false;
            edgeOperationsBox.Enabled = false;
            taskOneButton.Enabled = false;
            convertButton.Enabled = false;
            generateBox.Enabled = false;
            mainMenu.Enabled = false;
        }

        /// <summary>
        /// Ресет цветов вершин для корректного отображения результатов
        /// </summary>
        private void ResetVertexColors()
        {
            for (int i = 0; i < _graph.VertexCount(); i++)
            {
                if (_graph.GetVertex(i).Color == Colors.Red)
                {
                    _graph.SetVertex(i, new VertexDescriptor(i.ToString(), Colors.Yellow));
                }
            }
        }

        /// <summary>
        /// Ресет для второй задачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetButton_Click(object sender, EventArgs e)
        {
            _graph = _backupGraph;
            _iterator = null;
            setIteratorVertex.Text = "?";
            resetButton.Visible = false;
            taskTwoButton.Visible = true;
            iteratorOperationsBox.Enabled = true;
            edgeOperationsBox.Enabled = true;
            taskOneButton.Enabled = true;
            convertButton.Enabled = true;
            generateBox.Enabled = true;
            mainMenu.Enabled = true;
            PrintGraph();
        }
        #endregion

        #region Работа с итератором
        private void setIteratorButton_Click(object sender, EventArgs e)
        {
            if (_vertexOne != -1)
            {
                ResetVertexColors();
                _iterator = new SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.Iterator(_graph, _vertexOne);
                _iterator.Begin();
                _graph.SetVertex(_iterator.Current(), new VertexDescriptor(_vertexOne.ToString(), Colors.Red));
                setIteratorVertex.Text = _vertexOne.ToString();
                _vertexOne = _vertexTwo = -1;
                PrintGraph();
            }
            else
            {
                ShowWarning("Выберите начальную вершину!");
            }
        }
        private void beginIteratorButton_Click(object sender, EventArgs e)
        {
            if(_iterator != null)
            {
                ResetVertexColors();
                _iterator.Begin();
                _graph.SetVertex(_iterator.Current(), new VertexDescriptor(_vertexOne.ToString(), Colors.Red));
                PrintGraph();
            }
            else
            {
                ShowWarning("Итератор не установлен!");
            }
        }
        private void nextIteratorButton_Click(object sender, EventArgs e)
        {
            if (_iterator != null)
            {
                _graph.SetVertex(_iterator.Current(), new VertexDescriptor(_iterator.Current().ToString(), Colors.Yellow));

                if (_iterator.Next())
                {
                    _graph.SetVertex(_iterator.Current(),
                                     new VertexDescriptor(_iterator.Current().ToString(), Colors.Red));
                }
                else
                {
                    ShowWarning("Итератор достиг последней смежной вершины!");
                }

                PrintGraph();
            }
            else
            {
                ShowWarning("Итератор не установлен!");
            }
        }
        #endregion

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Отображение предупреждения
        /// </summary>
        /// <param name="txt"></param>
        public static void ShowWarning(string txt)
        {
            MessageBox.Show(txt, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
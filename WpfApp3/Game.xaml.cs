using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        MainWindow mainWindow;

        public bool isRunning = false;

        Cell[][] cells;
        int CellLengthField;
        int CellHeightField;

        int LengthField = 700;
        int HeightField = 700;

        double CellLength;
        double CellHeight;

        public Game()
        {
            InitializeComponent();
            isRunning = true;
        }

        public Game(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            isRunning = true;
            RandGenerate();
        }
        
        private void RandGenerate()
        {
            try
            {
                CellLengthField = Convert.ToInt32(mainWindow.LengthField.Text.ToString().Split(" ")[0]);
                CellHeightField = Convert.ToInt32(mainWindow.HeightField.Text.ToString().Split(" ")[0]);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cells = new Cell[LengthField][];
            CellLength = Math.Round((double) (LengthField - 10) / CellLengthField, 4);
            CellHeight = Math.Round((double) (HeightField - 10) / CellHeightField, 4);
            double marginLeft = 10;
            double marginTop = 10;
            Random random = new Random();
            for(int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell[HeightField];

                for(var j = 0; j < cells[0].Length; j++)
                {
                    marginLeft += 10 + CellLength;
                    marginTop += 10 + CellHeight;
                    int rnd = random.Next(0, 2);
                    if (rnd == 0)
                        cells[i][j] = new Cell(this.Field, false, marginLeft, marginTop, CellLength, CellHeight);
                    else
                        cells[i][j] = new Cell(this.Field, true , marginLeft, marginTop, CellLength, CellHeight);
                }
            }
        }

        public Game(MainWindow mainWindow, bool isRunning)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.isRunning = isRunning;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isRunning = false;
            if (mainWindow.isRunning == true)
                mainWindow.Close();
        }

    }
    public class Cell
    {
        SolidColorBrush Green = new SolidColorBrush(Color.FromRgb(21, 243, 202));
        SolidColorBrush Transparent = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        double x;
        double y;
        Rectangle rectangle;
        public bool isWall;
        double RadiusX = 6.75;
        double RadiusY = 6.75;
        public Cell(Canvas field, bool isWall, double x, double y, double height, double width)
        {
            this.x = x;
            this.y = y;
            this.isWall = isWall;
            rectangle = new Rectangle();
            rectangle.Stroke = Green;
            rectangle.Visibility = Visibility.Visible;
            rectangle.RadiusX = RadiusX;
            rectangle.RadiusY = RadiusY;
            field.Children.Add(rectangle);
            rectangle.Margin = new Thickness(x, y, 0, 0);
            rectangle.Height = height;
            rectangle.Width = width;

            if (isWall)
                rectangle.Fill = Green;
            else
                rectangle.Fill = Transparent;
        }
    }
}

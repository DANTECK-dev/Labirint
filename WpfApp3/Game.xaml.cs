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

        static public SolidColorBrush Green = new SolidColorBrush(Color.FromRgb(21, 243, 202));
        static public SolidColorBrush Orange = new SolidColorBrush(Color.FromRgb(243, 163, 21));
        static public SolidColorBrush Red = new SolidColorBrush(Color.FromRgb(243, 21, 21));
        static public SolidColorBrush Transparent = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

        public bool isRunning = false;

        Cell[][] cells;
        int reverseCount;

        int CellLengthField;
        int CellHeightField;

        int LengthField = 700;
        int HeightField = 700;

        double CellLength;
        double CellHeight;
        
        Cell EndCell;
        Player player;

        public Game()
        {
            InitializeComponent();
            isRunning = true;
            player = new Player();
            RandGenerate();
            Field.Focus();
        }

        public Game(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            isRunning = true;
            player = new Player();
            RandGenerate();
            Field.Focus();
        }
        public Game(MainWindow mainWindow, bool isRunning)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.isRunning = isRunning;
            player = new Player();
            RandGenerate();
            Field.Focus();
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
            CellLength = Math.Round((double) (LengthField - 20 - (CellLengthField * 2 -1)) / CellLengthField, 4);
            CellHeight = Math.Round((double) (HeightField - 20 - (CellHeightField * 2 - 1)) / CellHeightField, 4);
            double marginLeft = 10;
            double marginTop = 10;
            Random random = new Random();

            for(int i = 0; i < CellHeightField; i++)
            {
                cells[i] = new Cell[HeightField];

                for(var j = 0; j < CellLengthField; j++)
                {
                    int rnd = random.Next(0, 2);
                    if (rnd == 0)
                        cells[i][j] = new Cell(this.Field, false, marginLeft, marginTop, CellHeight, CellLength);
                    else
                        cells[i][j] = new Cell(this.Field, true , marginLeft, marginTop, CellHeight, CellLength);
                    marginLeft += 2 + CellLength;
                    
                }
                marginTop += 2 + CellHeight;
                marginLeft = 10;
            }
            EndCell = cells[random.Next(CellLengthField / 2, CellLengthField)] [random.Next(CellHeightField / 2, CellHeightField)];
            player.x = random.Next(CellLengthField / 2);
            player.y = random.Next(CellHeightField / 2);
            cells[player.y][player.x].rectangle.Fill = Red;
            EndCell.rectangle.Fill = Orange;
            reverseCount = random.Next((int)CellLengthField / 8) + 2;
            Reverse_Count.Content = "Осталось " + reverseCount + " реверсов";
            cells[player.y][player.x].rectangle.ToolTip = "Игрок";
            EndCell.rectangle.ToolTip = "Выход";
        }

        private void Field_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.R)
            {
                Reverse_Click(sender, e);
                return;
            }
            if (e.Key == Key.W)
            {
                if (cells[player.y == 0 ? CellHeightField - 1: player.y - 1][player.x].rectangle.Fill != Transparent) return;
                cells[player.y][player.x].rectangle.Fill = Transparent;
                cells[player.y][player.x].rectangle.ToolTip = "Проход";

                if (player.y == 0)
                    player.y = CellHeightField - 1;
                else
                    player.y--;
            }
            else if (e.Key == Key.S)
            {
                if (cells[player.y == CellHeightField - 1 ? 0 : player.y + 1][player.x].rectangle.Fill != Transparent) return;
                cells[player.y][player.x].rectangle.Fill = Transparent;
                cells[player.y][player.x].rectangle.ToolTip = "Проход";

                if (player.y == CellHeightField - 1)
                    player.y = 0;
                else
                    player.y++;
            }
            else if (e.Key == Key.A)
            {
                if (cells[player.y][player.x == 0 ? CellLengthField - 1 : player.x - 1].rectangle.Fill != Transparent) return;
                cells[player.y][player.x].rectangle.Fill = Transparent;
                cells[player.y][player.x].rectangle.ToolTip = "Проход";

                if (player.x == 0)
                    player.x = CellLengthField - 1;
                else
                    player.x--;
            }
            else if (e.Key == Key.D)
            {
                if (cells[player.y][player.x == CellLengthField - 1 ? 0 : player.x + 1].rectangle.Fill != Transparent) return;
                cells[player.y][player.x].rectangle.Fill = Transparent;
                cells[player.y][player.x].rectangle.ToolTip = "Проход";

                if (player.x == CellLengthField - 1)
                    player.x = 0;
                else
                    player.x++;
            }
            cells[player.y][player.x].rectangle.Fill = Red;
            cells[player.y][player.x].rectangle.ToolTip = "Игрок";
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isRunning = false;
            if (mainWindow.isRunning == true)
                mainWindow.Close();
        }

        private void Reverse_Click(object sender, RoutedEventArgs e)
        {
            if (reverseCount != 0)
            {
                for (int i = 0; i < CellHeightField; i++)
                {
                    for (int j = 0; j < CellLengthField; j++)
                    {
                        if (cells[i][j].rectangle.Fill == Red) continue;
                        if (cells[i][j].rectangle.Fill == Orange) continue;
                        if (cells[i][j].rectangle.Fill == Transparent)
                            cells[i][j].rectangle.Fill = Green;
                        else
                            cells[i][j].rectangle.Fill = Transparent;
                    }
                }
                reverseCount--;
                Reverse_Count.Content = "Осталось " + reverseCount + " реверсов";
            }
        }

        private void Restart_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Visibility = Visibility.Visible;
            this.Close();
        }
    }
    public class Cell
    {
        public Rectangle rectangle;
        //public bool isWall;
        public double x;
        public double y;
        double RadiusX = 6.75;
        double RadiusY = 6.75;
        public Cell(Canvas field, bool isWall, double x, double y, double height, double width)
        {
            this.x = x;
            this.y = y;
            //this.isWall = isWall;
            rectangle = new Rectangle();
            rectangle.Stroke = Game.Green;
            rectangle.Visibility = Visibility.Visible;
            rectangle.RadiusX = RadiusX;
            rectangle.RadiusY = RadiusY;
            field.Children.Add(rectangle);
            rectangle.Margin = new Thickness(x, y, 0, 0);
            rectangle.Height = height;
            rectangle.Width = width;

            if (isWall)
            {
                rectangle.Fill = Game.Green;
                rectangle.ToolTip = "Стена";
            }
            else
            {
                rectangle.Fill = Game.Transparent;
                rectangle.ToolTip = "Проход";
            }
        }
    }
    public class Player
    {
        public string Name;
        public SolidColorBrush color = Game.Red;
        public int x = 0;
        public int y = 0;
    }
}

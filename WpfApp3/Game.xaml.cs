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

        SolidColorBrush Green = new SolidColorBrush(Color.FromRgb(21, 243, 202));
        SolidColorBrush Orange = new SolidColorBrush(Color.FromRgb(243, 163, 21));
        SolidColorBrush Red = new SolidColorBrush(Color.FromRgb(243, 21, 21));
        SolidColorBrush Transparent = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

        public bool isRunning = false;

        Cell[][] cells;
        int CellLengthField;
        int CellHeightField;

        int LengthField = 700;
        int HeightField = 700;

        double CellLength;
        double CellHeight;

        Cell StartCell;
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
            EndCell.rectangle.Fill = Orange;
        }
        /*private void Recursive_Find(int x, int y)
        {
            Random rnd = new Random();
            switch(rnd.Next(5))
            {
                case 0:
                    if(y > 0 && cells[y--][x] != EndCell)
                    {
                        cells[y--][x].rectangle.Fill = Transparent;
                        cells[y--][x].isWall = false;
                        Recursive_Find(x, y--);
                    }
                    break;
                case 1:
                    if (y < CellHeightField && cells[y++][x] != EndCell)
                    {
                        cells[y++][x].rectangle.Fill = Transparent;
                        cells[y--][x].isWall = false;
                        Recursive_Find(x, y++);
                    }
                    break;
                case 2:
                    if (x > 0 && cells[y][x--] != EndCell)
                    {
                        cells[y][x--].rectangle.Fill = Transparent;
                        cells[y--][x].isWall = false;
                        Recursive_Find(x--, y);
                    }
                    break;
                case 3:
                    if (x < CellLengthField && cells[y][x++] != EndCell)
                    {
                        cells[y][x++].rectangle.Fill = Transparent;
                        cells[y--][x].isWall = false;
                        Recursive_Find(x++, y);
                    }
                    break;
            }
        }*/
        private void Field_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                
                if (player.y == 0)
                {
                    if (cells[CellHeightField - 1][player.x].rectangle.Fill == player.color) return;
                }
                else
                {

                }
                if (cells[player.y - 1][player.x].isWall == true)
                    return;
                cells[player.y][player.x].rectangle.Fill = Transparent;
                if (player.y == 0)
                    player.y = CellHeightField--;
                else
                    player.y--;
                cells[player.y][player.x].rectangle.Fill = Red;
                return;
            }
            if (e.Key == Key.S && player.y < CellHeightField && cells[player.y + 1][player.x].isWall == false)
            {
                cells[player.y][player.x].rectangle.Fill = Transparent;
                player.y++;
                cells[player.y][player.x].rectangle.Fill = Red;
                return;
            }
            if (e.Key == Key.A && player.x > 0 && cells[player.y][player.x - 1].isWall == false)
            {
                cells[player.y][player.x].rectangle.Fill = Transparent;
                player.x--;
                cells[player.y][player.x].rectangle.Fill = Red;
                return;
            }
            if (e.Key == Key.D && player.x < CellLengthField && cells[player.y][player.x + 1].isWall == false)
            {
                cells[player.y][player.x].rectangle.Fill = Transparent;
                player.x++;
                cells[player.y][player.x].rectangle.Fill = Red;
                return;
            }
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
    public class Player
    {
        public string Name;
        public SolidColorBrush color = new SolidColorBrush(Color.FromRgb(243, 21, 21));
        public int x = 0;
        public int y = 0;
    }
}

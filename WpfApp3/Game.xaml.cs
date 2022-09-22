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
        int LengthField;
        int HeightField;

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
                LengthField = Convert.ToInt32(mainWindow.LengthField.Text.ToString().Split(" ")[0]);
                HeightField = Convert.ToInt32(mainWindow.HeightField.Text.ToString().Split(" ")[0]);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cells = new Cell[LengthField][];
            Random random = new Random();
            for(int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell[HeightField];

                for(var j = 0; j < cells[0].Length; j++)
                {
                    int rnd = random.Next(0, 2);
                    if (rnd == 0)
                        cells[i][j].isWall = false;
                    else 
                        cells[i][j].isWall = true;
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
        public bool isWall;
    }
}

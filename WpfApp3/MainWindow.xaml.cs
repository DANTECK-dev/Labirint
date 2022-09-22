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
using System.Windows.Navigation;
using System.Threading;

using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MyDelegate();
        Thread thread; 

        SolidColorBrush Green = new SolidColorBrush(Color.FromRgb(21, 243, 202));
        SolidColorBrush Orange = new SolidColorBrush(Color.FromRgb(243, 163, 21));
        SolidColorBrush Red = new SolidColorBrush(Color.FromRgb(243, 21, 21));

        Game game;
        public bool isRunning = false;
        
        public MainWindow()
        {
            InitializeComponent();
            //thread = new Thread(LF_Fliking);
            //thread.IsBackground = true;
            //thread.Start();
            isRunning = true;
        }

        public MainWindow(Game game)
        {
            InitializeComponent();
            this.game = game;
            isRunning = true;
        }

        public MainWindow(Game game, bool isRunning)
        {
            InitializeComponent();
            this.game = game;
            this.isRunning = isRunning;
        }

        private async void StartGame_Click(object sender, RoutedEventArgs e)
        {
            //Delegate @delegate = new Delegate();
            //@delegate.DynamicInvoke(new AsyncCallback(LengthField_Fliking), null);  

            if (!Int32.TryParse(HeightField.Text, out _) && !Int32.TryParse(LengthField.Text, out _))
            {
                //await HeightField_Fliking();
                //await LengthField_Fliking();
                MessageBox.Show("Некоректно введены поля длины и высоты", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Int32.TryParse(HeightField.Text, out _))
            {
                //await HeightField_Fliking();
                MessageBox.Show("Некоректно введена высота поля", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Int32.TryParse(LengthField.Text, out _))
            { 
                //await LengthField_Fliking();
                MessageBox.Show("Некоректно введена длина поля", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int length = Int32.Parse(LengthField.Text.Replace(" ", string.Empty));
            if (length > 100 || length < 20)
            {
                MessageBox.Show("Длина поля должна быть не меньше 10 и не больше 30", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int height = Int32.Parse(HeightField.Text.Replace(" ", string.Empty));
            if (height > 100 || height < 20)
            {
                MessageBox.Show("Высота поля должна быть не меньше 10 и не больше 30", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(game == null)
                game = new Game(this);
            game.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        /*        
        public void LengthField_Fliking(IAsyncResult aRes)
        {
            for (int i = 0; i < 2; i++)
            {
                LengthField.BorderBrush = Orange;
                LengthField.Foreground = Orange;
                Thread.Sleep(50);
                LengthField.BorderBrush = Red;
                LengthField.Foreground = Red;
                Thread.Sleep(50);
            }
        }

        public void HeightField_Fliking()
        {
            for (int i = 0; i < 2; i++)
            {
                HeightField.BorderBrush = Orange;
                HeightField.Foreground = Orange;
                Thread.Sleep(50);
                HeightField.BorderBrush = Red;
                HeightField.Foreground = Red;
                Thread.Sleep(50);
            }
        }
        */

        private void LengthField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = LengthField.Text;
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", "\"", "\n", "\t", "!", "#", "№", "$", "%", "^", "&", "?", "*", "(", ")", "[", "]", "{", "}"
            , "-", "_", "=", "+", "/", "*", "\\", "|", ">", "<", "~", "`", " "};
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            if (Int32.TryParse(str, out _))
            {
                LengthField.Text = str;
                int length = Int32.Parse(str.Replace(" ", string.Empty));
                if (length > 100 || length < 20)
                {
                    LengthField.BorderBrush = Red;
                    LengthField.Foreground = Red;
                    LengthField.SelectionStart = LengthField.Text.Length;
                    return;
                }
                LengthField.BorderBrush = Green;
                LengthField.Foreground = Green;
                LengthField.SelectionStart = LengthField.Text.Length;
            }
            else
            {
                LengthField.BorderBrush = Red;
                LengthField.Foreground = Red;
                LengthField.SelectionStart = LengthField.Text.Length;
            }
        }

        private void LengthField_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LengthField.Text = " ";
        }

        private void HeightField_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = HeightField.Text;
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", "\"", "\n", "\t", "!", "#", "№", "$", "%", "^", "&", "?", "*", "(", ")", "[", "]", "{", "}"
            , "-", "_", "=", "+", "/", "*", "\\", "|", ">", "<", "~", "`", " "};
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            if (Int32.TryParse(str, out _))
            {
                HeightField.Text = str;
                int height = Int32.Parse(str.Replace(" ", string.Empty));
                if (height > 100 || height < 20)
                {
                    HeightField.BorderBrush = Red;
                    HeightField.Foreground = Red;
                    HeightField.SelectionStart = HeightField.Text.Length;
                    return;
                }
                HeightField.BorderBrush = Green;
                HeightField.Foreground = Green;
                HeightField.SelectionStart = HeightField.Text.Length;
            }
            else
            {
                HeightField.BorderBrush = Red;
                HeightField.Foreground = Red;
                HeightField.SelectionStart = HeightField.Text.Length;
            }
        }

        private void HeightField_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HeightField.Text = " ";
        }

        /*private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isRunning = false;
            if(game.isRunning == true)
                game.Close();
        }*/
    }
}

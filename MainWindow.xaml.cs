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
using System.Windows.Shapes;

namespace WpfAppXODemo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] buttons;
        Field gField = new Field();

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[,]
            {
                { AZero, AOne, ATwo },
                { BZero, BOne, BTwo },
                { CZero, COne, CTwo }
            };
            gField.NewGame();
            buttonVisual();
        }

        private void buttonVisual()
        {
            for (int i = 0; i < gField.XOField.GetLength(0); i++)
            {
                for (int j = 0; j < gField.XOField.GetLength(1); j++)
                {
                    buttons[i, j].Foreground = new SolidColorBrush(gField.XOColor[i, j]);
                    buttons[i, j].Content = gField.XOField[i, j];
                }
            }
        }

        private void Pen()
        {
            gameField.Children.OfType<Button>().ToList().ForEach(b =>
            {
                b.Cursor = Cursors.Pen;
            });
        }

        private void xClick(object sender, RoutedEventArgs e)
        {
            byte a = 0;
            byte b = 0;

            if (gField.WhoWalks == false && gField.GameOver == false)
            {
                Button button = sender as Button;
                switch (button.Name[0].ToString())
                {
                    case "A":
                        {
                            a = 0;
                            break;
                        }
                    case "B":
                        {
                            a = 1;
                            break;
                        }
                    case "C":
                        {
                            a = 2;
                            break;
                        }
                }

                switch (button.Name[1].ToString())
                {
                    case "Z":
                        {
                            b = 0;
                            break;
                        }
                    case "O":
                        {
                            b = 1;
                            break;
                        }
                    case "T":
                        {
                            b = 2;
                            break;
                        }
                }

                if (gField.XOField[a, b] == '-')
                {
                    buttons[a, b].Cursor = Cursors.No;
                    gField.Walks(a, b);
                    buttonVisual();
                }
            }

            a = 0;
            b = 0;

            if (gField.WhoWalks == true && gField.GameOver == false)
            {
                bool succes = false;
                while (succes == false)
                {
                    Random random = new Random();
                    a = Convert.ToByte(random.Next(0, 3));
                    b = Convert.ToByte(random.Next(0, 3));

                    if (gField.XOField[a, b] == '-')
                    {
                        buttons[a, b].Cursor = Cursors.No;
                        gField.Walks(a, b);
                        buttonVisual();
                        succes = true;
                        break;
                    }
                }
            }

            if (gField.GameOver == true)
            {
                MessageBoxResult Result = MessageBox.Show($"{gField.FindingWin()}", "Game Over!");
                if (Result == MessageBoxResult.OK)
                {
                    gField.NewGame();
                    Pen();
                    buttonVisual();
                }
            }
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            gField.NewGame();
            Pen();
            buttonVisual();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AboutAuthorsClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Горбатюк Денис и Земерова Анна ПР-31", "Authors", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

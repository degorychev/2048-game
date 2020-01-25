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

namespace _2048_game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        game gm = new game();
        public MainWindow()
        {
            InitializeComponent();
            render();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            gm.Shift(e.Key);
            render();
        }


        private void render()
        {
            bord00.Background = new SolidColorBrush(gm.GetColor(0, 0));
            bord01.Background = new SolidColorBrush(gm.GetColor(0, 1));
            bord02.Background = new SolidColorBrush(gm.GetColor(0, 2));
            bord03.Background = new SolidColorBrush(gm.GetColor(0, 3));

            bord10.Background = new SolidColorBrush(gm.GetColor(1, 0));
            bord11.Background = new SolidColorBrush(gm.GetColor(1, 1));
            bord12.Background = new SolidColorBrush(gm.GetColor(1, 2));
            bord13.Background = new SolidColorBrush(gm.GetColor(1, 3));

            bord20.Background = new SolidColorBrush(gm.GetColor(2, 0));
            bord21.Background = new SolidColorBrush(gm.GetColor(2, 1));
            bord22.Background = new SolidColorBrush(gm.GetColor(2, 2));
            bord23.Background = new SolidColorBrush(gm.GetColor(2, 3));

            bord30.Background = new SolidColorBrush(gm.GetColor(3, 0));
            bord31.Background = new SolidColorBrush(gm.GetColor(3, 1));
            bord32.Background = new SolidColorBrush(gm.GetColor(3, 2));
            bord33.Background = new SolidColorBrush(gm.GetColor(3, 3));
        
            lab00.Content = gm.getContent(0, 0);
            lab01.Content = gm.getContent(0, 1);
            lab02.Content = gm.getContent(0, 2);
            lab03.Content = gm.getContent(0, 3);

            lab10.Content = gm.getContent(1, 0);
            lab11.Content = gm.getContent(1, 1);
            lab12.Content = gm.getContent(1, 2);
            lab13.Content = gm.getContent(1, 3);

            lab20.Content = gm.getContent(2, 0);
            lab21.Content = gm.getContent(2, 1);
            lab22.Content = gm.getContent(2, 2);
            lab23.Content = gm.getContent(2, 3);

            lab30.Content = gm.getContent(3, 0);
            lab31.Content = gm.getContent(3, 1);
            lab32.Content = gm.getContent(3, 2);
            lab33.Content = gm.getContent(3, 3);
        }
    }

    class game
    {
        int[,] pole;
        Random rnd;
        int size = 4;
        public game()
        {
            rnd = new Random();
            pole = new int[size, size];
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    pole[x, y] = 0;
            RandomAdd();
        }

        private Color ToMediaColor(System.Drawing.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public Color GetColor(int x, int y)
        {
            switch (pole[x, y])
            {
                case 0:
                    return Color.FromArgb(0, 0, 0, 0);
                case 2:
                    return ToMediaColor(Colors.Default.color2);
                case 4:
                    return ToMediaColor(Colors.Default.color4);
                case 8:
                    return ToMediaColor(Colors.Default.color8);
                case 16:
                    return ToMediaColor(Colors.Default.color16);
                case 32:
                    return ToMediaColor(Colors.Default.color32);
                case 64:
                    return ToMediaColor(Colors.Default.color64);
                case 128:
                    return ToMediaColor(Colors.Default.color128);
                case 256:
                    return ToMediaColor(Colors.Default.color256);
                case 512:
                    return ToMediaColor(Colors.Default.color512);
                case 1024:
                    return ToMediaColor(Colors.Default.color1024);
                case 2048:
                    return ToMediaColor(Colors.Default.color2048);
                default:
                    return Color.FromRgb(0, 0, 0);
            }
        }

        public string getContent(int x, int y)
        {
            if (pole[x, y] == 0)
                return " ";
            return pole[x, y].ToString();
        }

        public void Shift(Key k)
        {
            int[,] dump = new int[size, size];
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    dump[x, y] = pole[x, y];
            switch (k)
            {
                case Key.Left:
                    ShiftLeft();
                    break;
                case Key.Right:
                    ShiftRight();
                    break;
                case Key.Up:
                    ShiftUp();
                    break;
                case Key.Down:
                    ShiftDown();
                    break;
            }
            if (!compare(pole, dump))
                RandomAdd();
        }

        private bool compare(int[,] A, int[,] B)
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (A[x, y] != B[x, y])
                        return false;
            return true;
        }

        private void ShiftRight()
        {
            for (int x = size-1; x >=0; x--)
                for (int y = size-1; y >0; y--)
                {
                    if (pole[x, y] == 0)
                    {
                        for (int i = y-1; i >=0; i--)
                            if (pole[x, i] != 0)
                            {
                                pole[x, y] = pole[x, i];
                                pole[x, i] = 0;
                                break;
                            }
                    }
                    else
                    {
                        for (int i = y - 1; i >= 0; i--)
                            if (pole[x, i] == pole[x, y])
                            {
                                pole[x, y] += pole[x, i];
                                pole[x, i] = 0;
                                break;
                            }
                    }

                }
        }

        private void ShiftDown()
        {
            for (int x = size - 1; x >= 0; x--)
                for (int y = size - 1; y > 0; y--)
                {
                    if (pole[y, x] == 0)
                    {
                        for (int i = y - 1; i >= 0; i--)
                            if (pole[i, x] != 0)
                            {
                                pole[y, x] = pole[i, x];
                                pole[i, x] = 0;
                                break;
                            }
                    }
                    else
                    {
                        for (int i = y - 1; i >= 0; i--)
                            if (pole[i, x] == pole[y, x])
                            {
                                pole[y, x] += pole[i, x];
                                pole[i, x] = 0;
                                break;
                            }
                    }

                }
        }

        private void ShiftUp()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size - 1; y++)
                {
                    if (pole[y, x] == 0)
                    {
                        for (int i = y + 1; i < size; i++)
                            if (pole[i, x] != 0)
                            {
                                pole[y, x] = pole[i, x];
                                pole[i, x] = 0;
                                break;
                            }
                    }
                    else
                    {
                        for (int i = y + 1; i < size; i++)
                            if (pole[i, x] == pole[y, x])
                            {
                                pole[y, x] += pole[i, x];
                                pole[i, x] = 0;
                                break;
                            }
                    }

                }
        }

        private void ShiftLeft()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size-1; y++)
                {
                    if (pole[x, y] == 0)
                    {
                        for (int i = y+1; i < size; i++)
                            if (pole[x, i] != 0)
                            {
                                pole[x, y] = pole[x, i];
                                pole[x, i] = 0;
                                break;
                            }
                    }
                    else
                    {
                        for (int i = y+1; i < size; i++)
                            if (pole[x, i] == pole[x, y])
                            {
                                pole[x, y] += pole[x, i];
                                pole[x, i] = 0;
                                break;
                            }
                    }

                }
        }

        private bool Finish()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (pole[x, y] == 0)
                        return false;
            return true;
        }

        public void RandomAdd()
        {
            if (Finish())
            {
                MessageBox.Show("Konec igri");
                return;
            }

            int x, y;
            do
            {
                x = rnd.Next(size);
                y = rnd.Next(size);
            } while (pole[x, y] != 0);
            pole[x, y] = 2;
        }
    }
}

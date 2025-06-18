using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        private char[,] level =
        {
            { '>', '.', '.', '.', '.', },
            { '.', '.', '.', '.', '.', },
            { '.', '.', '.', '.', '.', },
            { '.', '.', '.', '.', '.', },
            { '.', '.', '.', '.', '.', },
        };

        private Dictionary<(int, int), Canvas> view = new Dictionary<(int, int), Canvas>();

        public MainWindow()
        {
            InitializeComponent();
            //timer.Interval = TimeSpan.FromMilliseconds(1000);
            //timer.Tick += Timer_Tick;
            //timer.Start();

            //var cellWidth = DrawArea.Width / level.GetLength(1);
            //var cellHeight = DrawArea.Height / level.GetLength(0);


            //for (var x = 0; x < level.GetLength(1); x++)
            //{

            //    for (var y = 0; y < level.GetLength(0); y++)
            //    {

            //        var cell = new Canvas();
            //        if (level[x, y] == '.')
            //        {
            //            cell.Background = Brushes.Blue;
            //        }
            //        else
            //        {
            //            cell.Background = Brushes.Green;
            //        }

            //        cell.Height = cellHeight;
            //        cell.Width = cellWidth;
            //        Canvas.SetTop(cell, y * cellWidth);
            //        Canvas.SetLeft(cell, x * cellHeight);

            //        DrawArea.Children.Add(cell);
            //        view[(x, y)] = cell;
            //    }
            //}

        }

        void Timer_Tick(object sender, EventArgs e)
        {
            foreach ((var (x, y), var canvas) in view)
            {
                if (canvas.Background == Brushes.Blue)
                {
                    canvas.Background = Brushes.Green;
                }
                else if (canvas.Background == Brushes.Green)
                {
                    canvas.Background = Brushes.Blue;
                }
            }
        }

        public void start_button_clicked(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("wowie zowie!");
        }
    }
}
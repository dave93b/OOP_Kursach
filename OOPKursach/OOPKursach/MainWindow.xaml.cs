using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace OOPKursach
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _random = new Random();
        private DispatcherTimer _timer = new DispatcherTimer();

        private int _totalPoints = 0, _itemsOnBoard = 0;

        public MainWindow()
        {
            InitializeComponent();
            pointsLabel.Content = _totalPoints;
            _timer.Tick += DispatcherTimer_Tick;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            ShowItem();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            canvas.IsEnabled = true;
            loseLabel.Visibility=Visibility.Collapsed;
            _totalPoints = 0;
            _itemsOnBoard = 0;

            pointsLabel.Content = _totalPoints;
            imagesLabel.Content = _itemsOnBoard;
            
            startButton.IsEnabled = false;

            _timer.Interval = TimeSpan.FromSeconds(0.02);
            _timer.Start();
        }

        private void ShowItem()
        {
            Image image=new Image();
            BitmapImage bi=new BitmapImage();

            int size = _random.Next(30, 75);

            bi.BeginInit();
            bi.UriSource=new Uri("1.png",UriKind.Relative);
            bi.EndInit();
            image.Height = size;
            image.Width = size;
            image.Margin=new Thickness(_random.Next(0,1115),_random.Next(0,660),0,0);
            image.Source = bi;

            image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
            
            if (++_itemsOnBoard<=1000)
            {
                canvas.Children.Add(image);
                imagesLabel.Content = _itemsOnBoard;
            }
            else
            {
                _timer.Stop();
                canvas.IsEnabled = false;
                loseLabel.Visibility = Visibility.Visible;
                startButton.IsEnabled = true;
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imagesLabel.Content = --_itemsOnBoard;

            pointsLabel.Content = ++_totalPoints;
            canvas.Children.Remove((Image)sender);
        }
    }
}
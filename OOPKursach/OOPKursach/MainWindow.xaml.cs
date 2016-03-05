using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
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
        private double _appearingInterval;
        private int _totalAppearedItems = 0;

        private int _totalPoints = 0, _itemsOnBoard = 0;

        public MainWindow()
        {
            InitializeComponent();
            pointsLabel.Content = _totalPoints;
            _timer.Tick += DispatcherTimer_Tick;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            _totalAppearedItems++;
            if (_totalAppearedItems%10 == 0)
            {
                _appearingInterval *= 0.9;
                _timer.Interval = TimeSpan.FromSeconds(_appearingInterval);
            }
            ShowItem();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            _appearingInterval = 1.0;
            canvas.Children.Clear();
            canvas.IsEnabled = true;
            loseLabel.Visibility=Visibility.Collapsed;
            _totalPoints = 0;
            _itemsOnBoard = 0;

            pointsLabel.Content = _totalPoints;
            imagesLabel.Content = _itemsOnBoard;
            
            startButton.IsEnabled = false;

            _timer.Interval = TimeSpan.FromSeconds(_appearingInterval);
            _timer.Start();
        }

        private void ShowItem()
        {
            DerivedShape2 shape=new DerivedShape2();
            shape.GetShape().MouseLeftButtonDown += Shape_MouseLeftButtonDown;
            
            if (++_itemsOnBoard<=10)
            {
                canvas.Children.Add(shape.GetShape());
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

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Автор:\nБейлинов Давид\nГруппа: ИКИТ-425");
        }

        private void Shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imagesLabel.Content = --_itemsOnBoard;

            pointsLabel.Content = ++_totalPoints;
            canvas.Children.Remove((Shape)sender);
        }
    }

    abstract class BaseShape
    {
        protected Shape[] shapes = {new Ellipse(), new Rectangle()};

        public abstract Shape GetShape();
    }

    class DerivedShape1:BaseShape
    {
        Random random=new Random();

        public override Shape GetShape()
        {
            Shape shape = shapes[new Random().Next(0, 2)];

            shape.Fill = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));

            shape.Stroke = Brushes.Black;
            shape.StrokeThickness = 3;
            int size = random.Next(40, 80);
            shape.Height = size;
            shape.Width = size;
            shape.Margin = new Thickness(random.Next(0, 1115), random.Next(0, 550), 0, 0);
            return shape;
        }
    }

    class DerivedShape2:DerivedShape1
    {
        public override Shape GetShape()
        {
            return base.GetShape();
        }
    }
}
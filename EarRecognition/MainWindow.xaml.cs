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

namespace EarRecognition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int clickedPointButton;
        private Point? leftPoint;
        private Point? rightPoint;
        private Point? topPoint;
        private Point? bottomPoint;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            clickedPointButton = -1;
        }

        private ICommand _AddImage;
        public ICommand AddImage
        {
            get
            {
                if (_AddImage == null)
                {
                    _AddImage = new RelayCommand(
                        param => this.AddImageClick(), 
                        param => { return true; });
                }
                return _AddImage;
            }
        }

        public void AddImageClick()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();            dlg.DefaultExt = ".jpg";            dlg.Filter = "(*.jpg)|*.jpg";            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                image.Source = new TransformedBitmap(new BitmapImage(new Uri(filename)),
                    new ScaleTransform(0.35, 0.35));
            }
        }

        private ICommand _LeftLearningPoint;
        private ICommand _RightLearningPoint;
        private ICommand _TopLearningPoint;
        private ICommand _BottomLearningPoint;

        public ICommand LeftLearningPoint
        {
            get
            {
                if (_LeftLearningPoint == null)
                {
                    _LeftLearningPoint = new RelayCommand(
                        param => { clickedPointButton = 0; },
                        param => this.CanLearningPoint());
                }
                return _LeftLearningPoint;
            }
        }

        public ICommand RightLearningPoint
        {
            get
            {
                if (_RightLearningPoint == null)
                {
                    _RightLearningPoint = new RelayCommand(
                        param => { clickedPointButton = 1; },
                        param => this.CanLearningPoint());
                }
                return _RightLearningPoint;
            }
        }

        public ICommand TopLearningPoint
        {
            get
            {
                if (_TopLearningPoint == null)
                {
                    _TopLearningPoint = new RelayCommand(
                        param => { clickedPointButton = 2; },
                        param => this.CanLearningPoint());
                }
                return _TopLearningPoint;
            }
        }

        public ICommand BottomLearningPoint
        {
            get
            {
                if (_BottomLearningPoint == null)
                {
                    _BottomLearningPoint = new RelayCommand(
                        param => { clickedPointButton = 3; },
                        param => this.CanLearningPoint());
                }
                return _BottomLearningPoint;
            }
        }

        public bool CanLearningPoint()
        {
            return image.Source != null;
        }

        private ICommand _AddToDatabase;
        public ICommand AddToDatabase
        {
            get
            {
                if (_AddToDatabase == null)
                {
                    _AddToDatabase = new RelayCommand(
                        param => this.AddToDatabaseClick(),
                        param => this.CanAddToDatabase());
                }
                return _AddToDatabase;
            }
        }

        public void AddToDatabaseClick()
        {
            //TODO
        }

        public bool CanAddToDatabase()
        {
            return image.Source != null && leftPoint != null && rightPoint != null 
                && topPoint != null && bottomPoint != null 
                && learningName.Text != null && learningSurname.Text != null;
        }

        public void DrawPoint(object sender, MouseButtonEventArgs e)
        {
            if (clickedPointButton == -1)
                return;

            if (e.ButtonState == MouseButtonState.Pressed)
            {
                Ellipse ellipse = new Ellipse()
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Red
                };
                canvas.Children.Add(ellipse);
                Point center = e.GetPosition(canvas);
                Canvas.SetLeft(ellipse, center.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, center.Y - ellipse.Height / 2);

                switch (clickedPointButton)
                {
                    case 0:
                        lefLearningPoint.Content = $"({center.X},{center.Y})";
                        leftPoint = center;
                        break;
                    case 1:
                        rightLearningPoint.Content = $"({center.X},{center.Y})";
                        rightPoint = center;
                        break;
                    case 2:
                        topLearningPoint.Content = $"({center.X},{center.Y})";
                        topPoint = center;
                        break;
                    case 3:
                        bottomLearningPoint.Content = $"({center.X},{center.Y})";
                        bottomPoint = center;
                        break;
                }
            }        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
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
        private System.Windows.Point? leftPoint;
        private System.Windows.Point? rightPoint;
        private System.Windows.Point? topPoint;
        private System.Windows.Point? bottomPoint;

        private Bitmap bitmap;

        private ICommand _OpenDatabase;
        private ICommand _CreateDatabase;
        private ICommand _AddImage;
        private ICommand _LeftLearningPoint;
        private ICommand _RightLearningPoint;
        private ICommand _TopLearningPoint;
        private ICommand _BottomLearningPoint;
        private ICommand _ClearPoints;
        private ICommand _AddToDatabase;
        private ICommand _Recognize;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            clickedPointButton = -1;
            recognition.IsEnabled = false;
        }

        #region ICommands

        public ICommand OpenDatabase
        {
            get
            {
                if (_OpenDatabase == null)
                {
                    _OpenDatabase = new RelayCommand(
                        param => OpenDatabaseClick(),
                        param => { return true; });
                }
                return _OpenDatabase;
            }
        }
    
        public ICommand CreateDatabase
        {
            get
            {
                if (_CreateDatabase == null)
                {
                    _CreateDatabase = new RelayCommand(
                        param => CreateDatabaseClick(),
                        param => { return !mode.IsChecked.Value; });
                }
                return _CreateDatabase;
            }
        }

        public ICommand AddImage
        {
            get
            {
                if (_AddImage == null)
                {
                    _AddImage = new RelayCommand(
                        param => AddImageClick(), 
                        param => { return true; });
                }
                return _AddImage;
            }
        }

        public ICommand LeftLearningPoint
        {
            get
            {
                if (_LeftLearningPoint == null)
                {
                    _LeftLearningPoint = new RelayCommand(
                        param => { clickedPointButton = 0; },
                        param => { return image.Source != null && leftPoint == null; });
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
                        param => { return image.Source != null && rightPoint == null; });
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
                        param => { return image.Source != null && topPoint == null; });
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
                        param => { return image.Source != null && bottomPoint == null; });
                }
                return _BottomLearningPoint;
            }
        }

        public ICommand ClearPoints
        {
            get
            {
                if (_ClearPoints == null)
                {
                    _ClearPoints = new RelayCommand(
                        param => ClearPointsClick(),
                        param => {
                            return leftPoint != null || rightPoint != null
                                || topPoint != null || bottomPoint != null;
                        });
                }
                return _ClearPoints;
            }
        }

        public ICommand AddToDatabase
        {
            get
            {
                if (_AddToDatabase == null)
                {
                    _AddToDatabase = new RelayCommand(
                        param => AddToDatabaseClick(),
                        param => CanAddToDatabase());
                }
                return _AddToDatabase;
            }
        }

        public ICommand Recognize
        {
            get
            {
                if (_Recognize == null)
                {
                    _Recognize = new RelayCommand(
                        param => RecognizeClick(),
                        param => CanRecognize());
                }
                return _Recognize;
            }
        }

        #endregion

        public void OpenDatabaseClick()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();            dlg.DefaultExt = ".xml";            dlg.Filter = "(*.xml)|*.xml";            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                DataAccessLayer.databasePath = dlg.FileName;
                databaseName.Content = DataAccessLayer.databasePath.Split('\\')
                    [DataAccessLayer.databasePath.Split('\\').Count() - 1];
            }
        }

        public void CreateDatabaseClick()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();            dlg.FileName = "nowa baza";            dlg.DefaultExt = ".xml";            dlg.Filter = "(*.xml)|*.xml";            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                DataAccessLayer.CreateNewDatabase(dlg.FileName);
                DataAccessLayer.databasePath = dlg.FileName;
                databaseName.Content = DataAccessLayer.databasePath.Split('\\')
                    [DataAccessLayer.databasePath.Split('\\').Count() - 1];
            }
        }

        public void AddImageClick()
        {
            image.Source = null;
            bitmap = null;
            learningName.Text = null;
            learningSurname.Text = null;
            recognizedName.Content = null;
            recognizedSurname.Content = null;
            ClearMarkedPoints();

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();            dlg.DefaultExt = ".jpg";            dlg.Filter = "(*.jpg)|*.jpg";            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                Bitmap originalBitmap = new Bitmap(filename);
                bitmap = new Bitmap(originalBitmap, 
                    new System.Drawing.Size((int)(originalBitmap.Width * 0.35), (int)(originalBitmap.Height * 0.35)));
                image.Source = new TransformedBitmap(new BitmapImage(new Uri(filename)),
                    new ScaleTransform(0.35, 0.35));
            }
        }

        public void ClearPointsClick()
        {
            ClearMarkedPoints();
        }

        public void AddToDatabaseClick()
        {
            Person person = new Person()
            {
                name = learningName.Text,
                surname = learningSurname.Text,
                earHeight = (int)(bottomPoint.Value.Y - topPoint.Value.Y),
                earWidth = (int)(rightPoint.Value.X - leftPoint.Value.X)
            };
            person.CalculateDarkPixelsCount(bitmap, leftPoint.Value, topPoint.Value);

            if (DataAccessLayer.AddPerson(person))
            {
                MessageBox.Show("Dodano do bazy danych", "Informacja");
                image.Source = null;
                bitmap = null;
                learningName.Text = null;
                learningSurname.Text = null;
                recognizedName.Content = null;
                recognizedSurname.Content = null;
                ClearMarkedPoints();
            }
            else
                MessageBox.Show("Wystąpił błąd", "Informacja");
        }

        public void RecognizeClick()
        {
            Person person = new Person()
            {
                earHeight = (int)(bottomPoint.Value.Y - topPoint.Value.Y),
                earWidth = (int)(rightPoint.Value.X - leftPoint.Value.X)
            };
            person.CalculateDarkPixelsCount(bitmap, leftPoint.Value, topPoint.Value);

            try
            {
                Person foundPerson = DataAccessLayer.FindPerson(person);
                recognizedName.Content = foundPerson.name;
                recognizedSurname.Content = foundPerson.surname;
            }
            catch(ArgumentException e)
            {
                MessageBox.Show("Baza danych jest pusta", "Informacja");
            }
        }

        public bool CanAddToDatabase()
        {
            return image.Source != null && leftPoint != null && rightPoint != null 
                && topPoint != null && bottomPoint != null 
                && learningName.Text != string.Empty && learningSurname.Text != string.Empty;
        }

        public bool CanRecognize()
        {
            return image.Source != null && leftPoint != null && rightPoint != null
               && topPoint != null && bottomPoint != null;
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
                    Fill = System.Windows.Media.Brushes.Red
                };
                canvas.Children.Add(ellipse);
                System.Windows.Point center = e.GetPosition(canvas);
                Canvas.SetLeft(ellipse, center.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, center.Y - ellipse.Height / 2);

                switch (clickedPointButton)
                {
                    case 0:
                        if(mode.IsChecked.Value)
                            leftRecognitionPoint.Content = $"({center.X},{center.Y})";
                        else
                            leftLearningPoint.Content = $"({center.X},{center.Y})";
                        leftPoint = center;
                        break;
                    case 1:
                        if (mode.IsChecked.Value)
                            rightRecognitionPoint.Content = $"({center.X},{center.Y})";
                        else
                            rightLearningPoint.Content = $"({center.X},{center.Y})";
                        rightPoint = center;
                        break;
                    case 2:
                        if (mode.IsChecked.Value)
                            topRecognitionPoint.Content = $"({center.X},{center.Y})";
                        else
                            topLearningPoint.Content = $"({center.X},{center.Y})";
                        topPoint = center;
                        break;
                    case 3:
                        if (mode.IsChecked.Value)
                            bottomRecognitionPoint.Content = $"({center.X},{center.Y})";
                        else
                            bottomLearningPoint.Content = $"({center.X},{center.Y})";
                        bottomPoint = center;
                        break;
                }
            }            clickedPointButton = -1;        }

        public void ChangeMode(object sender, RoutedEventArgs e)
        {
            recognition.IsEnabled = !recognition.IsEnabled;
            learning.IsEnabled = !learning.IsEnabled;
            image.Source = null;
            learningName.Text = null;
            learningSurname.Text = null;
            recognizedName.Content = null;
            recognizedSurname.Content = null;
            ClearMarkedPoints();
        }

        private void ClearMarkedPoints()
        {
            leftPoint = null;
            rightPoint = null;
            topPoint = null;
            bottomPoint = null;
            leftLearningPoint.Content = "Współrzędne";
            rightLearningPoint.Content = "Współrzędne";
            topLearningPoint.Content = "Współrzędne";
            bottomLearningPoint.Content = "Współrzędne";
            leftRecognitionPoint.Content = "Współrzędne";
            rightRecognitionPoint.Content = "Współrzędne";
            topRecognitionPoint.Content = "Współrzędne";
            bottomRecognitionPoint.Content = "Współrzędne";
            canvas.Children.RemoveRange(1, canvas.Children.Count - 1);
        }
    }
}

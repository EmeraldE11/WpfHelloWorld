using System.Text;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfHelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool showing = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        bool isEpressed = false;
        bool isRpressed = false;

        private void Window_KeyDown(object sender, KeyEventArgs k)
        {
            if (k.Key == Key.E)
                isEpressed = true;
            if (k.Key == Key.R)
                isRpressed = true;

            if (isEpressed && isRpressed)
            {
                FindButton_Click(this, new RoutedEventArgs());
                isEpressed = false;
                isRpressed = false;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs k)
        {
            if (k.Key == Key.E)
                isEpressed = false;
            if (k.Key == Key.R)
                isRpressed = false;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "IMG Files | *.jpg; *jpeg; *.png";


            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                //image selected
                string fileName = fileDialog.FileName;

                Info.Text = fileName;

                //convert filename to image type
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(fileName, UriKind.Absolute);
                img.CacheOption = BitmapCacheOption.OnLoad; // Ensures file is freed after loading
                img.EndInit();


                HelloWorld.Source = img;


                HelloWorld.Visibility = Visibility.Visible;
            }
            else
            {
                // didn't select an image
                HelloWorld.Opacity = 0.5;
            }

        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            HelloWorld.Visibility = Visibility.Collapsed;
            HelloWorld.Opacity = 1;
        }
    }
}
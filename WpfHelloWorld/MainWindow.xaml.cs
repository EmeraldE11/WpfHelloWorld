﻿using System.Text;
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
using Soti.Reporting;
using System.Reflection;

namespace WpfHelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Catch and log crashes.
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(WriteUnhandledExceptionToFile);
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
            fileDialog.Filter = "IMG Files | *jpg; *jpeg; *png; *gif";


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
                HelloWorld.Opacity = 1;
            }
            else
            {
                // didn't select an image
                HelloWorld.Opacity = 0.5;
                CauseCrash();
            }

        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            HelloWorld.Visibility = Visibility.Collapsed;
            HelloWorld.Opacity = 1;
        }

        void CauseCrash()
        {
            throw new InvalidOperationException("This is a test exception for error logging.");
        }

        // EVERYTHING below this comment --- Copied from SOTI Reporting README
        // For unhandled UI exceptions.
        private void WriteUnhandledExceptionToFile(object sender, UnhandledExceptionEventArgs args)
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            string message = LogWriter.LogUnhandledExceptionToFile(appName, args.ExceptionObject as Exception);
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
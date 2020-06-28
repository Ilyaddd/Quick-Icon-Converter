using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Quick_Icon_Converter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }
        private String[] imageToConvertPath;
               
        static int w;
        static int h;
        public void convert()
        {
            if (imageToConvertPath != null)
            {
                Bitmap thumb = (Bitmap)Image.FromFile(imageToConvertPath[0]);
                //    thumb.Width = w; //Указыем размер иконки
                thumb.MakeTransparent();
                               
                //Generate save file dialog
                SaveFileDialog sfd = new SaveFileDialog();
                //Set dialog filter
                sfd.Filter = "Icon (*.ico)|*.ico|All files (*.*)|*.*";
                if (sfd.ShowDialog() == true)
                {
                    Stream IconStream = File.Create(sfd.FileName);
                    Icon icon = ImageHelper.Convert(thumb, new System.Drawing.Size(w, h));
                    icon.Save(IconStream);
                    IconStream.Close();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select an image by dragging it into the window or by clicking 'Browse...'");
            }
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            try
            { DragMove(); }
            catch { }
        }

        private void Minimized(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        void Brows_Img()
        {
            OpenFileDialog obj = new OpenFileDialog();
            obj.Multiselect = false;
            if (obj.ShowDialog() == true)
            {
                try
                {
                    Uri fileUri = new Uri(obj.FileName);
                    Img.Source = new BitmapImage(fileUri);
                }
                catch (Exception i)
                {
                    MessageBox.Show("Invalid image file. Please select a valid file. Exception: " + i.Message);
                }

                imageToConvertPath = new String[1]; //Must instantiate array to prevent null reference exception
                imageToConvertPath[0] = obj.FileName;
            }
            S6.IsChecked = true;
        }
        private void Browse(object sender, RoutedEventArgs e)
        {
            Brows_Img();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            convert();
        }

        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Brows_Img();
        }

        private void TBack_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void BBack_Checked(object sender, RoutedEventArgs e)
        {  

        }

        private void WBack_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void S6_Checked(object sender, RoutedEventArgs e)
        {
            w = 256;
            h = 256;
        }

        private void S5_Checked(object sender, RoutedEventArgs e)
        {
            w = 128;
            h = 128;
        }

        private void S4_Checked(object sender, RoutedEventArgs e)
        {
            w = 64;
            h = 64;
        }

        private void S3_Checked(object sender, RoutedEventArgs e)
        {
            w = 48;
            h = 48;
        }

        private void S2_Checked(object sender, RoutedEventArgs e)
        {
            w = 32;
            h = 32;
        }

        private void S1_Checked(object sender, RoutedEventArgs e)
        {
            w = 16;
            h = 16;
        }
    }
}
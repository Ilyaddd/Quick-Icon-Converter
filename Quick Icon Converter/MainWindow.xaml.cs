using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

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
        public void convert()
        {
            if (imageToConvertPath != null)
            {
                Bitmap thumb = (Bitmap)Image.FromFile(imageToConvertPath[0]);
                int w;
                int h;
                    w = 16;
                    h = 16;
                    thumb = ResizeImage(thumb, w, h); //Указыем размер иконки
                thumb.MakeTransparent();

                Graphics g = Graphics.FromImage(thumb); // allow drawing to it
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; // make the image shrink nicely by using HighQualityBicubic mode
                g.DrawImage(thumb, 0, 0, w, h);
                g.Flush();

                //Generate save file dialog
                SaveFileDialog sfd = new SaveFileDialog();
                //Set dialog filter
                sfd.Filter = "Icon (*.ico)|*.ico|All files (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Stream IconStream = System.IO.File.Create(sfd.FileName);

                    Icon icon = ImageHelper.Convert(thumb, new Size(w, h));
                    this.Icon = icon;
                    this.Icon.Save(IconStream);
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
        }

        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon(); //Иконка для трея
        void TrayOff(object sender, EventArgs e)
        {
            Show();
            nIcon.Visible = false;

        }
        private void Tray_Minimized(object sender, RoutedEventArgs e)
        {
            if (nIcon.Visible != true)
            {
                nIcon.Icon = new Icon(@"C:\Users\ryletd\Desktop\favicon.ico");
                nIcon.Visible = true;
                nIcon.Click += TrayOff;
                Hide();
            }
            else
                Show();
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    Img.Source = Image.FromFile(ofd.FileName);
                }
                catch (Exception i)
                {
                    System.Windows.MessageBox.Show("Invalid image file. Please select a valid file. Exception: " + i.Message);
                }


                imageToConvertPath = new String[1]; //Must instantiate array to prevent null reference exception
                imageToConvertPath[0] = ofd.FileName;
            }
        }
    }
}

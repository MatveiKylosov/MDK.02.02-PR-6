using RegIN_Kylosov.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegIN_Kylosov.Pages
{
    public partial class Recovery : Page
    {


        string OldLogin;
        bool IsCapture = false;

        private void CorrectLogin()
        {
            if (OldLogin != TbLogin.Text)
            {
                Tools.SetNotification(LNameUser, "Hi, " + MainWindow.mainWindow.UserLogIn.Name, Brushes.Black);
                try
                {
                    BitmapImage biImg = new BitmapImage();
                    MemoryStream ms = new MemoryStream(MainWindow.mainWindow.UserLogIn.Image);
                    biImg.BeginInit();
                    biImg.StreamSource = ms;
                    biImg.EndInit();
                    ImageSource imgSrc = biImg;
                    Tools.AnimateImageChange(IUser, imgSrc);
                }
                catch (Exception exp)
                {
                    Debug.WriteLine(exp.Message);
                };
                OldLogin = TbLogin.Text;
                SendNewPassword();
            }
        }

        private void InCorrectLogin()
        {
            if (LNameUser.Content != "")
            {
                LNameUser.Content = "";
                ImageSource defaultUserImage = new BitmapImage(new Uri("pack://application:,,,/Images/ic_user.png"));
                Tools.AnimateImageChange(IUser, defaultUserImage);
            }

            if (TbLogin.Text.Length > 0)
                Tools.SetNotification(LNameUser,"Login is incorrect", Brushes.Red);
        }

        private void SetLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);
        }
        private void SetLogin(object sender, RoutedEventArgs e) =>
MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);

        public void SendNewPassword()
        {
            if (IsCapture)
            {
                if (MainWindow.mainWindow.UserLogIn.Password != String.Empty)
                {
                    ImageSource mailIcon = new BitmapImage(new Uri("pack://application:,,,/Images/ic-mail.png"));
                    Tools.AnimateImageChange(IUser, mailIcon);

                    Tools.SetNotification(LNameUser,"An email has been sent to your email.", Brushes.Black);
                    MainWindow.mainWindow.UserLogIn.CrateNewPassword();
                }
            }
        }

        private void OpenLogin(object sender, MouseButtonEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Login());
        }

        private void CorrectCapture()
        {
            Capture.IsEnabled = false;
            IsCapture = true;
            SendNewPassword();
        }

        public Recovery()
        {
            InitializeComponent();
            MainWindow.mainWindow.UserLogIn.HandlerCorrectLogin += CorrectLogin;
            MainWindow.mainWindow.UserLogIn.HandlerInCorrectLogin += InCorrectLogin;
            Capture.HandlerCorrectCapture += CorrectCapture;
        }
    }
}

﻿using RegIN_Kylosov.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    public partial class Login : Page
    {
        string OldLogin;
        int CountSetPassword = 2;
        bool IsCapture = false;
        bool AuthPincode = false;

        public void InCorrectLogin()
        {

            if (LNameUser.Content != "")
            {
                LNameUser.Content = "";
                Tools.AnimateImageChange(IUser, new BitmapImage(new Uri("pack://application:,,,/Images/ic_user.png")));
            }


            if (TbLogin.Text.Length > 0)
                Tools.SetNotification(LNameUser,"Login is incorrect", Brushes.Red);
        }


        public void CorrectLogin()
        {
            if (AuthPincode)
                MainWindow.mainWindow.OpenPage(new AuthPin());
            if (OldLogin != TbLogin.Text)
            {
                Tools.SetNotification(LNameUser,"Hi, " + MainWindow.mainWindow.UserLogIn.Name, Brushes.Black);
                if (!string.IsNullOrEmpty(MainWindow.mainWindow.UserLogIn.Pincode))
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Log in via pincode?",
                        "Confirmation of action",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                    );

                    AuthPincode = result == MessageBoxResult.Yes;
                }

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
            }
        }

        public void CorrectCapture()
        {
            Capture.IsEnabled = false;
            IsCapture = true;
        }

        private void SetPassword(object sender, KeyEventArgs e)
        {
            if (AuthPincode)
                MainWindow.mainWindow.OpenPage(new AuthPin());
            if (e.Key == Key.Enter)
                SetPassword();
        }

        public void SetPassword()
        {
            if (AuthPincode)
                MainWindow.mainWindow.OpenPage(new AuthPin());
            if (MainWindow.mainWindow.UserLogIn.Password != String.Empty)
            {
                if (IsCapture)
                {
                    if (MainWindow.mainWindow.UserLogIn.Password == TbPassword.Password)
                    {
                        MainWindow.mainWindow.OpenPage(new Confirmation(Confirmation.TypeConfirmation.Login));
                    }
                    else
                    {
                        if (CountSetPassword > 0)
                        {
                            Tools.SetNotification(LNameUser,$"Password is incorrect, {CountSetPassword} attempts left", Brushes.Red);
                            CountSetPassword--;
                        }
                        else
                        {
                            Thread TBlockAutorization = new Thread(BlockAutorization);
                            TBlockAutorization.Start();

                            SendMail.SendMessage("An attempt was made to log into your account.", MainWindow.mainWindow.UserLogIn.Login);
                        }
                    }
                }
                else
                    Tools.SetNotification(LNameUser,$"Enter capture", Brushes.Red);
            }
        }

        public void BlockAutorization()
        {
            DateTime StartBlock = DateTime.Now.AddMinutes(3);
            Dispatcher.Invoke(() =>
            {
                TbLogin.IsEnabled = false;
                TbPassword.IsEnabled = false;
                Capture.IsEnabled = false;
            });
            for (int i = 0; i < 180; i++)
            {
                TimeSpan TimeIdle = StartBlock.Subtract(DateTime.Now);
                string s_minutes = TimeIdle.Minutes.ToString();
                if (TimeIdle.Minutes < 10)
                    s_minutes = "0" + TimeIdle.Minutes;
                string s_seconds = TimeIdle.Seconds.ToString();
                if (TimeIdle.Seconds < 10)
                    s_seconds = "0" + TimeIdle.Seconds;
                Dispatcher.Invoke(() =>
                {
                    Tools.SetNotification(LNameUser,$"Reauthorization available in: {s_minutes}:{s_seconds}", Brushes.Red);
                });
                Thread.Sleep(1000);
            }
            Dispatcher.Invoke(() =>
            {
                Tools.SetNotification(LNameUser,"Hi, " + MainWindow.mainWindow.UserLogIn.Name, Brushes.Black);
                TbLogin.IsEnabled = true;
                TbPassword.IsEnabled = true;
                Capture.IsEnabled = true;
                Capture.CreateCapture();
                IsCapture = false;
                CountSetPassword = 2;
            });
        }


        private void SetLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);

                if(AuthPincode)
                    MainWindow.mainWindow.OpenPage(new AuthPin());

                if (TbPassword.Password.Length > 0)
                    SetPassword();
            }
        }
        private void SetLogin(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);
            if (TbPassword.Password.Length > 0)
                SetPassword();
        }


        private void RecoveryPassword(object sender, MouseButtonEventArgs e) => MainWindow.mainWindow.OpenPage(new Recovery());

        private void OpenRegin(object sender, MouseButtonEventArgs e) => MainWindow.mainWindow.OpenPage(new Regin());

        public Login()
        {
            InitializeComponent();
            MainWindow.mainWindow.UserLogIn.HandlerCorrectLogin += CorrectLogin;
            MainWindow.mainWindow.UserLogIn.HandlerInCorrectLogin += InCorrectLogin;
            Capture.HandlerCorrectCapture += CorrectCapture;
        }
    }
}

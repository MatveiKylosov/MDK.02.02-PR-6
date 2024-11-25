using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegIN_Kylosov.Classes;

namespace RegIN_Kylosov.Pages
{
    /// <summary>
    /// Логика взаимодействия для PincodePage.xaml
    /// </summary>
    public partial class PincodePage : Page
    {
        bool FirstPin = false;
        bool SetUpPin = false;

        public PincodePage()
        {
            InitializeComponent();
        }

        private void FirstPincode(object sender, KeyEventArgs e)
        {
            if (SetUpPin)
                return;

            if(new Regex(@"^\d{4}$").Match(TbCodeFirst.Text).Success)
            {
                FirstPin = true;
                Tools.SetNotification(LNameUser, "", Brushes.Black);
            }
            else
            {
                FirstPin = false;
                Tools.SetNotification(LNameUser, "Incorrect pincode", Brushes.Red);
            }
        }

        private void SecondPincode(object sender, KeyEventArgs e)
        {
            if (SetUpPin)
                return;

            if (new Regex(@"^\d{4}$").Match(TbCodeSecond.Text).Success && FirstPin)
            {
                MainWindow.mainWindow.UserLogIn.Pincode = TbCodeSecond.Text;
                MainWindow.mainWindow.UserLogIn.SetPincode();
                Tools.SetNotification(LNameUser, "", Brushes.Black);
                MessageBox.Show("The PIN code has been set", "Information");
                TbCodeFirst.IsEnabled = TbCodeSecond.IsEnabled = false;
            }
            else
            {
                Tools.SetNotification(LNameUser, "Incorrect pincode", Brushes.Red);
            }
        }

        private void OpenLogin(object sender, MouseButtonEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Login());
        }
    }
}

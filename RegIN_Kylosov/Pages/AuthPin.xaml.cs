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

namespace RegIN_Kylosov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPin.xaml
    /// </summary>
    public partial class AuthPin : Page
    {
        public AuthPin()
        {
            InitializeComponent();
        }

        private void FirstPincode(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            { 
                if(TbCodeFirst.Password != MainWindow.mainWindow.UserLogIn.Pincode)
                {
                    MessageBox.Show("Incorrect password.", "Error");
                }
                else
                {
                    MessageBox.Show("Авторизация пользователя успешно подтверждена.");
                    TbCodeFirst.IsEnabled = false;
                }
            }
        }

        private void OpenLogin(object sender, MouseButtonEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Login());
        }
    }
}

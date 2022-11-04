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
using TravelPal.Enums;
using TravelPal.Manage;

namespace TravelPal
{
   
    public partial class MainWindow : Window
    {
        private UserManager _userManager = new();


        public MainWindow()
        {
            InitializeComponent();
        }

        //Knapp som öppnar registerWindow
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Window registerWindow = new RegisterWindow(_userManager);
            registerWindow.Show();

        }
        // Knapp för att signa in user 
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUsername.Text;
            string password = pswPassword.Password;

            bool isFoundUser = false;

            foreach (IUser user in _userManager.Users)
            {
                if (user.Username == username && user.Password == password)
                {
                    isFoundUser = true;

                    if (user.GetType() == typeof(User))
                    {
                        Window travelWindow = new TravelWindow(_userManager, user, this);
                        travelWindow.Show();
                        Hide();
                        break;
                    }
                    else if (user.GetType() == typeof(Admin))
                    {
                        Window travelWindow = new TravelWindow(_userManager, user, this);
                        travelWindow.Show();
                        Hide();
                        break;

                    }
                }
            }

            // En ruta som dyker upp om user skriver något felaktigt 
            if (!isFoundUser)
            {
                MessageBox.Show("Username or password is incorrect", "Warning");
            }

        }


    }
}



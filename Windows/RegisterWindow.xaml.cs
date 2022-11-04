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
using System.Windows.Shapes;
using TravelPal.Enums;
using TravelPal.Manage;

namespace TravelPal
{
    public partial class RegisterWindow : Window
    {

        UserManager userManager = new(); 


        // En knapp för att registrera sig och visa land
        public RegisterWindow(UserManager userManager)
        {
            InitializeComponent();

            this.userManager = userManager;
            string[] location = Enum.GetNames(typeof(Countries));

            cbCountry.ItemsSource = location;
            cbCountry.SelectedIndex = 212;    
            
        }

        // En knapp för att registrera user 
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pswPassword.Password;
            string location = cbCountry.SelectedItem as string; 
            

                
            if (location != null && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Countries country = (Countries)Enum.Parse(typeof(Countries), location);
                this.userManager.addUser(username, password, country);

            }
            else
            {
                MessageBox.Show("You're missing inputs!");
            }
            //hej
            Close();
        }
    }
}

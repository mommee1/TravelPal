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

namespace TravelPal.Windows
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        private readonly UserManager _userManager;
        private readonly User _user;
        private readonly TravelWindow _travelWindow;
        public UserDetailsWindow(UserManager userManager, User user, TravelWindow travelWindow)
        {
            InitializeComponent();
            _userManager = userManager;
            _user = user;
            _travelWindow = travelWindow;
            cbSettingCountry.ItemsSource = Enum.GetValues(typeof(Countries));
            cbSettingCountry.SelectedIndex = (int)user.Location;
            tbUserSettingsUserName.Text = user.Username;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUserSettingsUserName.Text;
            string password = pbSettingsPassword.Password;
            Countries country = (Countries)cbSettingCountry.SelectedIndex;

            if (string.IsNullOrEmpty(tbUserSettingsUserName.Text))
            {
                MessageBox.Show("Username cannot be empty", "Warning");
                return;

            }

            if (username == _user.Username)
            {
                MessageBox.Show("You cant change to the same username", "Warning");
                return;
            }

            if (_userManager.Users.Any(x => x.Username == username))
            {
                MessageBox.Show("The username already exists, pick another one. ");
                return;
            }
            if (pbSettingsConfirmPassword.Password != pbSettingsPassword.Password)
            {
                MessageBox.Show("The passwords do not match");
                return;
            }

            _userManager.UpdateUserName(_user, username);
            _userManager.UpdatePassword(_user, password);
            _userManager.UpdateCountry(_user, country);
            _travelWindow.UpdateGUI();

            MessageBox.Show("Settings has been changed", "Settings");
            Close();
        }
    }
}

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
using TravelPal.Manage;
using TravelPal.Windows;

namespace TravelPal
{

    public partial class TravelWindow : Window
    {


        private List<Travel> travels = new();

        private readonly UserManager _userManager;

        private readonly User _user;
        private readonly Admin _admin;
        private bool _isAdmin = false;
        private readonly MainWindow _mainWindow;

        public TravelWindow()
        {
            InitializeComponent();
            DisplayTravels();
        }



        public TravelWindow(UserManager userManager, IUser user, MainWindow mainWindow)
        {
            InitializeComponent();
            if (user.GetType() == typeof(User))
            {
                _user = (User)user;
                travels = _user.Travels;
                lblUsername.Content = _user.Username;

            }
            else
            {
                btnAddTravel.IsEnabled = false;
                btnUserDetailsWindow.IsEnabled = false;
                _admin = (Admin)user;
                lblUsername.Content = "ADMIN";
                _isAdmin = true;
            }
            _userManager = userManager;
            _mainWindow = mainWindow;

            DisplayTravels();
        }


        // Öppna Travel Window
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            Window addTravelWindow = new AddTravelWindow(_user, this);
            addTravelWindow.Show();
        }

     

        // Logga ut
        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (!_isAdmin)
                MessageBox.Show($"Thank you for using this app, {_user.Username}. ");
            _mainWindow.Show();


        }

       

        // Öppna TravelDetailsWindow
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (listviewTravel.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a travel first!", "Warning");
                return;
            }
            Travel selectedTravel = travels[listviewTravel.SelectedIndex];
            if (selectedTravel.GetType().Equals(typeof(Vacation)))
            {
                Window travelDetailsWindow = new TravelDetailsWindow((Vacation)selectedTravel);
                travelDetailsWindow.Show();
            }
            else
            {
                Window travelDetailsWindow = new TravelDetailsWindow((Trip)selectedTravel);
                travelDetailsWindow.Show();
            }

        }
        public void DisplayTravels()
        {
            listviewTravel.Items.Clear();

            if (_isAdmin)
            {
                List<IUser> usersList = _userManager.Users.Where(x => x.GetType() == typeof(User)).ToList();

                foreach (User user in usersList)
                {
                    foreach (Travel travel in user.Travels)
                    {
                        travels.Add(travel);
                        ListViewItem listViewItem = new ListViewItem();

                        listViewItem.Tag = travel;
                        listViewItem.Content = $"User: {user.Username} Trip: {travel.Destination} | Country: {travel.Country.ToString()} | Travelers: {travel.Travellers.ToString()}";

                        listviewTravel.Items.Add(listViewItem);
                    }
                }
            }
            else
            {
                travels = _user.Travels;
                foreach (Travel travel in travels)
                {

                    ListViewItem listViewItem = new ListViewItem();

                    listViewItem.Tag = travel;
                    listViewItem.Content = $"Trip: {travel.Destination} | Country: {travel.Country.ToString()} | Travelers: {travel.Travellers.ToString()}";

                    listviewTravel.Items.Add(listViewItem);
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            listviewTravel.Focus();

            if (listviewTravel.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a travel first!", "Warning!");
                listviewTravel.SelectedIndex = -1;
                return;
            }


            Travel selectedTravel = travels[listviewTravel.SelectedIndex];

            if (_isAdmin)
            {
                string userName = (string)listviewTravel.SelectedItems[0].ToString().Split(" ")[2];
                _userManager.AdminRemoveTravel(listviewTravel.SelectedIndex, userName);
                UpdateGUI();
                return;
            }

            if (selectedTravel.GetType().Equals(typeof(Vacation)))
                travels.Remove((Vacation)selectedTravel);

            if (selectedTravel.GetType().Equals(typeof(Trip)))
                travels.Remove((Trip)selectedTravel);

            UpdateGUI();
        }

        public void UpdateGUI()
        {
            if (!_isAdmin)
                lblUsername.Content = _user.Username;

            DisplayTravels();
        }


        // Öppna UserDetailsWindow
        private void btnUserDetailsWindow_Click_(object sender, RoutedEventArgs e)
        {
            Window userDetailswindow = new UserDetailsWindow(_userManager, _user, this);
            userDetailswindow.Show();
        }


        // Öppna InfoWindow
        private void btnAsk_Click(object sender, RoutedEventArgs e)
        {
            Window infoWindow = new InfoWindow();
            infoWindow.Show();
        }
    }
}

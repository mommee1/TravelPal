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
    
    public partial class AddTravelWindow : Window
    {

        private readonly User _user;
        private readonly TravelWindow _travelWindow;

        public AddTravelWindow(User user, TravelWindow travelWindow)
        {
            InitializeComponent();
            _user = user;
            _travelWindow = travelWindow;
            cbAddCountry.ItemsSource = Enum.GetValues(typeof(Countries));
            cbAddCountry.SelectedIndex = 0;


            cbAddTripType.ItemsSource = Enum.GetValues(typeof(TripTypes));
            cbAddTripType.Visibility = Visibility.Hidden;
            lblTripType.Visibility = Visibility.Hidden;

            cbAddTravel.SelectedIndex = 0;



        }
        // Cancel knapp 
        private void btnAddTravelCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        // Spara användarens val 
        private void btnAddTravelSave_Click(object sender, RoutedEventArgs e)
        {
            string destination = tbDestination.Text;
            Countries country = (Countries)cbAddCountry.SelectedIndex;

            try
            {

                if (cbAddTravel.SelectedIndex == 0) // Ifall selected index är Vacation
                {
                    bool allInclusive = (bool)chkBoxAllIncl.IsChecked;
                    int travelers = int.Parse(tbTravelers.Text);

                    Vacation travel = new Vacation(allInclusive, destination, country, travelers);
                    _user.Travels.Add(travel);
                    _travelWindow.DisplayTravels();
                    Close();
                }
                else
                {
                    TripTypes tripType = (TripTypes)cbAddTripType.SelectedIndex;
                    int travelers = int.Parse(tbTravelers.Text);

                    Trip travel = new Trip(tripType, destination, country, travelers);
                    _user.Travels.Add(travel);
                    _travelWindow.DisplayTravels();
                    Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Fill in all the boxes please!");
            }

        }

        private void cbAddTravel_DropDownClosed(object sender, EventArgs e)
        {
            if (cbAddTravel.SelectedIndex == 1)
            {
                chkBoxAllIncl.Visibility = Visibility.Hidden;
                lblTripType.Visibility = Visibility.Visible;
                cbAddTripType.Visibility = Visibility.Visible;
            }
            else
            {
                chkBoxAllIncl.Visibility = Visibility.Visible;
                lblTripType.Visibility = Visibility.Hidden;
                cbAddTripType.Visibility = Visibility.Hidden;
            }

        }

        private void chkBoxAllIncl_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}

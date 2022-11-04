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

namespace TravelPal.Windows
{

    public partial class TravelDetailsWindow : Window
    {
        
        public TravelDetailsWindow(Vacation vacation)
        {
            InitializeComponent();
            lblTripType.Visibility = Visibility.Hidden;
            tbTripType.Visibility = Visibility.Hidden;
            tbTravelDestination.Text = vacation.Destination;
            tbTravelCountry.Text = vacation.Country.ToString();
            tbTravelTravelers.Text = vacation.Travellers.ToString();
            tbTrip.Text = "Vacation";
            checkboxAllInclusive.IsChecked = vacation.AllInclusive;

        }

        public TravelDetailsWindow(Trip trip)
        {
            InitializeComponent();
            checkboxAllInclusive.Visibility = Visibility.Hidden;
            tbTravelDestination.Text = trip.Destination;
            tbTravelCountry.Text = trip.Country.ToString();
            tbTravelTravelers.Text = trip.Travellers.ToString();
            tbTrip.Text = "Trip";
            tbTripType.Text = trip.TripType.ToString();

        }

        private void btnTravelBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

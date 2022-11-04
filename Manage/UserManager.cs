using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using TravelPal.Enums;
using TravelPal.Manage;

namespace TravelPal.Manage
{
    public class UserManager
    {
        public List<IUser> Users { get; set; }
        public IUser SignedInUser { get; set; }

        public UserManager()
        {
            Users = new List<IUser>();  
            Users.Add(
            new User
            {
                Username = "Gandalf",
                Password = "password",
                Location = Countries.Bosnia_and_Herzegovina,
                Travels = new List<Travel>()
                {
                    new Trip(TripTypes.Work, "Helsingborg", Countries.Sweden, 3),
                new Vacation(false, "Västerås", Countries.Sweden, 2)
                }

            });

            AddAdminUser();


        }

       


        public bool addUser(string username, string password, Countries country)
        {
            if (ValidateUsername(username))
            {
                User newUser = new User
                {
                    Username = username,
                    Password = password,
                    Location = country,
                    Travels = new()
                };

                Users.Add(newUser);

                return true;
            }
            return false;
        }
        // Lägga till admin
        public void AddAdminUser()
        {
            Admin admin = new();

            admin.Username = "admin";
            admin.Password = "password";

            Users.Add(admin);
        }

        // Ta bort användare 
        public bool removeUser(User user)
        {
            if (Users.Any(x => x.Username == user.Username))
            {
                Users.Remove(user);

                return true;
            }

            return false;
        }

        public bool UpdateUserName(User user, string updatedUserName)
        {
            user.Username = updatedUserName;
            return true;
        }

        private bool ValidateUsername(string username)
        {
            bool usernameIsValid = true;

            if (string.IsNullOrEmpty(username) || Users.Any(x => x.Username == username))
                usernameIsValid = false;

            return usernameIsValid;
        }

        public bool SignInUser(string username, string password)
        {
            return true;
        }

        public bool UpdatePassword(User user, string updatedPassword)
        {
            user.Password = updatedPassword;
            return true;
        }

        public bool UpdateCountry(User user, Countries country)
        {
            user.Location = country;
            return true;
        }

        public bool AdminRemoveTravel(int index, string userName)
        {
            List<Travel> travelList = new();

            foreach (User user in Users.Where(x => x.GetType() == typeof(User)).ToList())
            {
                foreach (Travel travel in user.Travels)
                    travelList.Add(travel);
            }

            Travel travelToRemove = travelList.ElementAt(index);

            User userWithTravelToDelete = (User)Users.FirstOrDefault(x => x.Username == userName);

            userWithTravelToDelete.Travels.Remove(travelToRemove);

            return true;
        }

    }
}

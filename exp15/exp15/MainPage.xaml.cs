using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace exp15
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


        }

        static Database database;


        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contacts.db3"));
                }
                return database;
            }
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Name.Text) && !string.IsNullOrWhiteSpace(LastName.Text) && !string.IsNullOrWhiteSpace(Telephone.Text))
            {
                await Database.SavePersonAsync(new Contact
                {
                    Name = Name.Text,
                    LastName = LastName.Text,
                    Telephone = Telephone.Text
                });

                Name.Text = LastName.Text = Telephone.Text = string.Empty;
                ContactListView.ItemsSource = await Database.GetPeopleAsync();
            }
        }
        public async void GetEmployees()
        {
            using (var client = new HttpClient())
            {
                // send a GET request  
                var uri = "https://jsonplaceholder.typicode.com/users";
                var result = await client.GetStringAsync(uri);

                //handling the answer  
                var prod = JsonConvert.DeserializeObject<List<Contact>>(result);

                await Database.SavePersonAsync(prod);






            }
        }
    }
}




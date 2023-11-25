using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using WpfApp1.ApplicationData;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Client _client;
        public AddEditPage(Client client)
        {
            InitializeComponent();
            GenderComboBox.Items.Add("Пол");
            foreach(var gender in DbWorker.GetContext().Gender.Select(x => x.Name))
            {
                GenderComboBox.Items.Add(gender);
            }
            
            if (client.ID == 0)
            {
                GenderComboBox.SelectedIndex = 0;
            }
            else
            {
                GenderComboBox.SelectedIndex = client.GenderCode;
            }

            _client = client;
            DataContext = _client;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _client.GenderCode = GenderComboBox.SelectedIndex;
                DbWorker.GetContext().Client.AddOrUpdate(_client);
                DbWorker.GetContext().SaveChanges();
                MessageBox.Show("Succesful save");
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new ClientsPage());
        }
    }
}

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
using WpfApp1.ApplicationData;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();

            FilterGenderComboBox.Items.Add("Пол");
            foreach(var gender in DbWorker.GetContext().Gender.Select(x => x.Name))
            {
                FilterGenderComboBox.Items.Add(gender);
            }
            FilterGenderComboBox.SelectedIndex = 0;

            SortComboBox.Items.Add("Сортировка");
            SortComboBox.Items.Add("Фамилия");
            SortComboBox.Items.Add("Посещения");
            SortComboBox.Items.Add("Кол-во пос.");
            SortComboBox.SelectedIndex = 0;
        }

        private void updateList()
        {
            List<Client> clients = DbWorker.GetContext().Client
                .Where(x => x.LastName.Contains(FinderTextBox.Text)
                || x.FirstName.Contains(FinderTextBox.Text)
                || x.Patronymic.Contains(FinderTextBox.Text)
                || x.Email.Contains(FinderTextBox.Text)
                || x.Phone.Contains(FinderTextBox.Text)).ToList();

            if (FilterGenderComboBox.SelectedIndex > 0)
            {
                clients = clients.Where(x => x.GenderCode == FilterGenderComboBox.SelectedIndex).ToList();
            }
            if (SortComboBox.SelectedIndex > 0)
            {
                if (SortComboBox.SelectedItem.Equals("Фамилия"))
                {
                    clients = clients.OrderBy(x => x.LastName).ToList();
                }
                else if (SortComboBox.SelectedItem.Equals("Посещения"))
                {
                    clients = clients.OrderByDescending(x => x.LastVisitDate).ToList();  
                }
                else if (SortComboBox.SelectedItem.Equals("Кол-во пос."))
                {
                    clients = clients.OrderByDescending(x => x.VisitCount).ToList();
                }
            }

            ClientsDataGrid.ItemsSource = clients;
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new AddEditPage(new Client()));
        }

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client selectedClient = ClientsDataGrid.SelectedItem as Client;
            AppFrame.mainFrame.Navigate(new AddEditPage(selectedClient));
        }

        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client selectedClient = ClientsDataGrid.SelectedItem as Client;
            try
            {
                DbWorker.GetContext().Client.Remove(selectedClient);
                DbWorker.GetContext().SaveChanges();
                MessageBox.Show("succesful deleting");
            }
            catch
            {
                MessageBox.Show("error");
            }
            updateList();
        }

        private void FinderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateList();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateList();
        }

        private void FilterGenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateList();
        }
    }
}

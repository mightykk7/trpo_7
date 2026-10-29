using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для VacancyPage.xaml
    /// </summary>
    public partial class VacancyPage : Page
    {
        public Vacancy vacancy { get; set; } = new Vacancy();
        HttpClient httpClient;
        public VacancyPage(HttpClient client)
        {
            InitializeComponent();
            httpClient = client;
            DataContext = vacancy;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(vacancy.Title))
            {
                MessageBox.Show("Введите название");
                return;
            }
            if (string.IsNullOrWhiteSpace(vacancy.Company))
            {
                MessageBox.Show("Введите компанию");
                return;
            }
            if (string.IsNullOrWhiteSpace(vacancy.City))
            {
                MessageBox.Show("Введите город");
                return;
            }
            if (vacancy.Salary <= 0)
            {
                MessageBox.Show("Введите зарплату");
                return;
            }
            try
            {
                HttpResponseMessage responce = await httpClient.PostAsJsonAsync("api/v1/Vacancies", vacancy);
                MessageBox.Show("Успешно добавлено");
                responce.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

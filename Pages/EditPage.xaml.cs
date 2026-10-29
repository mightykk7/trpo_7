using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        HttpClient httpClient;
        public Vacancy vacancy { get; set; }
        public EditPage(HttpClient client, Vacancy vacancy)
        {
            InitializeComponent();
            httpClient = client;
            this.vacancy = new Vacancy
            {
                Id = vacancy.Id,
                Title = vacancy.Title,
                Company = vacancy.Company,
                Salary = vacancy.Salary,
                City = vacancy.City,
                IsActive = vacancy.IsActive
            };
            DataContext = this.vacancy;

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
            if (vacancy.Salary <= 0)
            {
                MessageBox.Show("Введите зарплату");
                return;
            }
            if (string.IsNullOrWhiteSpace(vacancy.City))
            {
                MessageBox.Show("Введите город");
                return;
            }
            try
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"api/v1/Vacancies/{vacancy.Id}", vacancy);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("Рецепт успешно изменен!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

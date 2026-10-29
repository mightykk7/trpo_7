using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public ObservableCollection<Vacancy> vacancies { get; set; } = new ObservableCollection<Vacancy>();
        public string FilterCity { get; set; } = "";
        public Vacancy? SelectedVacancy { get; set; }
        public string FilterMinSalary { get; set; } = "";
        public string filter = "?";
        public Vacancy newVacancy { get; set; } = new Vacancy();
        HttpClient httpClinet = new HttpClient();
        public Page1()
        {
            InitializeComponent();
            httpClinet.BaseAddress = new Uri("https://localhost:7043/");
            LoadList();
        }
        public async Task LoadList()
        {
            try
            {

                if (!string.IsNullOrEmpty(FilterCity)) filter += $"city={FilterCity}";
                if (!string.IsNullOrEmpty(FilterMinSalary))
                {
                    if (filter != "?") filter += "&";
                    filter += $"minSalary={FilterMinSalary}";
                }
                string url = "api/v1/Vacancies";
                if (filter != "?") url += filter;


                var response = await httpClinet.GetFromJsonAsync<List<Vacancy>>(url);

                vacancies.Clear();
                if (response != null)
                {
                    
                    foreach (var vacancy in response)
                    {
                        vacancies.Add(vacancy);
                    }
                }
                filter = "?";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Список не загружен","Ошибка");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVacancy == null)
            {
                MessageBox.Show("Выберите из списка", "Ошибка");
                return;
            }
            NavigationService.Navigate(new EditPage(httpClinet, SelectedVacancy));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (SelectedVacancy == null)
            {
                MessageBox.Show("Выберите из списка", "Ошибка");
                return;
            }
            HttpResponseMessage response = await httpClinet.DeleteAsync($"api/v1/Vacancies/{SelectedVacancy.Id}");
        }

        private  void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VacancyPage(httpClinet));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LoadList();
        }
    }
}

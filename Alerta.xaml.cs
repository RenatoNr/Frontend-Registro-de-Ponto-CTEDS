﻿using Frontend_Registro_de_Ponto_CTEDS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
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

namespace Frontend_Registro_de_Ponto_CTEDS
{
    public partial class Alerta : Window
    {
        DateTime _date = DateTime.Parse("16 / 10 / 2022");

        private string uri = "https://localhost:7222";
        private List<User> Employees { get; set; } = new List<User>();
        private List<Clock> Clocks { get; set; } = new List<Clock>();
        public Alerta()
        {

            InitializeComponent();
            GetClock();
            GetUsers();
       
            this.Loaded += Alertas_Loaded;
        }
        private async void GetUsers()
        {
            using (HttpClient api = new HttpClient())
            {
                api.BaseAddress = new Uri(uri);
                api.DefaultRequestHeaders.Add("Accept", "application/json");

                var getEmployees = await api.GetAsync("/api/Employee");

                var response = await getEmployees.Content.ReadAsStringAsync();

                var jsonObject = JsonConvert.DeserializeObject<List<User>>(response);

                foreach (var item in jsonObject)
                {
                    Employees.Add(item);
                }
                foreach (var employee in Employees)
                {
                    int diastrabalhados = 0;
                    var dias = (DateTime.Now.Date - _date).ToString("dd");
                    var diasuteis = Int32.Parse(dias) + 1;
                    var clocks = Clocks.Where(x => x.EmployeeId == employee.Id);
                    foreach (var clock in clocks)
                    {
                        if (clock.ClockOut.ToString() == "")
                        {
                           
                        }
                        else
                        {
                           diastrabalhados++;
                        }
                       
                    }
                    var faltas = (diasuteis - diastrabalhados);
                    employee.faltas = faltas;
                }

            }
        }
        private async void GetClock()
        {
            using (HttpClient api = new HttpClient())
            {
                api.BaseAddress = new Uri(uri);
                api.DefaultRequestHeaders.Add("Accept", "application/json");

                var getClocks = await api.GetAsync("/api/Clock");

                var response = await getClocks.Content.ReadAsStringAsync();

                var jsonObject = JsonConvert.DeserializeObject<List<Clock>>(response);

                foreach (var item in jsonObject)
                {
                    Clocks.Add(item);
                }

            }
        }

        async void Alertas_Loaded(object sender, RoutedEventArgs e)
        {
            
            reportAlerta.ItemsSource = Employees;

        }

    }
}

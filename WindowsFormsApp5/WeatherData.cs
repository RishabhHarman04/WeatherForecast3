using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp5.Properties;
using static WindowsFormsApp5.SettingForm;

namespace WindowsFormsApp5
{
    public partial class WeatherData : Form
    {
        
        private WeatherService weatherService;
        private int panelIndex = 0;
        private int totalCities = 0;
        private WeatherSettings settings;

        public WeatherData(WeatherSettings settings)
        {
            InitializeComponent();
            Form_Size();
            this.settings = settings;
            this.weatherService = new WeatherService(settings);
            totalCities = settings.Cities.Count;       
            UpdateLabels();
            timer1.Interval = Int32.Parse(settings.RefreshTime) * 1000;
            timer1.Start();
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            panelIndex++;

            if (panelIndex >= totalCities)
            {
                panelIndex = 0;
            }
            Console.WriteLine(MyStrings.Tick + panelIndex);

            UpdateLabels();

        }
        public async void UpdateLabels()
        {
            try
            {
                    var city = settings.Cities.ElementAt(panelIndex);
                    Console.WriteLine(panelIndex.ToString());
                    var weatherData = await weatherService.GetWeatherData(city);
                    dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(weatherData);
                    double temperature = json.main.temp;
                    double humidity = json.main.humidity;
                    double pressure = json.main.pressure;
                    label1.Text = city;
                    lblHumidity.Text = String.Format(MyStrings.HumidityText, humidity);
                    lblAtmosphere.Text = String.Format(MyStrings.AtmoshphereText, pressure);
                    lblTemperature.Text = String.Format(MyStrings.TemperatureText, temperature);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
      
        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var newSettingsForm = new SettingForm();
                newSettingsForm.ShowDialog();

                if (newSettingsForm.Settings != null)
                {
                    weatherService = new WeatherService(newSettingsForm.Settings);
                    settings = newSettingsForm.Settings;
                    if (settings.Cities != null)
                    {
                        totalCities = settings.Cities.Count;

                    }
                    timer1.Interval = Int32.Parse(newSettingsForm.Settings.RefreshTime) * 1000;
                    UpdateLabels();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }     
        private void Form_Size()
        {
            this.Width = 700;
            this.Height = 550;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
        }

       
    }
}



      


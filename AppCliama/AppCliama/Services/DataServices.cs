using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using AppCliama.Model;
using System.Globalization;
using Xamarin.Essentials;

namespace AppCliama.Services
{
    class DataServices
    {
        public static async Task<Tempo> GetPrevisaoDoTempo(string Cidade)
        {
            string appId = "5fbd7703e1ee7811835e5ee1ac109adb";

            string QueryString = "http://api.openweathermap.org/data/2.5/weather?q=" + Cidade + "&unitis=metric" + "&AppId=" + appId;
            dynamic resultado = await GetDataFromService(QueryString).ConfigureAwait(false);

            if (resultado["weather"] != null)
            {
                Tempo previsao = new Tempo();
                previsao.Title = (string)resultado["name"];
                previsao.Temperature = (string)resultado["main"]["temp"] + " C";
                previsao.Wind = (string)resultado["wind"]["speed"] + " mph";
                previsao.Humidity = (string)resultado["main"]["humidity"] + " %";
                previsao.Visibility = (string)resultado["weather"][0]["main"];
                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)resultado["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)resultado["sys"]["sunset"]);
                previsao.Sunrise = string.Format("{0:d/mm/yyyy} HH:mm:ss", sunrise);
                previsao.Sunset = string.Format("{0:d/mm/yyyy} HH:mm:ss", sunset);
                return previsao;
            }
            else 
            { 
                return null; 
            }
        }

        private static async Task<dynamic> GetDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);
            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }

        private static async Task<dynamic> GetDataFromServiceByCity(string city)
        {
            string appId = "5fbd7703e1ee7811835e5ee1ac109adb";

            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", city.Trim(), appId);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }
    }
}
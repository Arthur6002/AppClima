using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using AppCliama.Model;
using System.Globalization;

namespace AppCliama.Services
{
    class DataServices
    {
        public static async Task<Tempo> GetPrevisaoDoTempo(string Cidade)
        {
            string appId = "5fbd7703e1ee7811835e5ee1ac109adb";

            string QueryString = "http://api.openweathermap.org/data/w.5/weather?q=" + Cidade + "&unitis=metric" + "&AppId=" + appId;
            dynamic resultado = await GetDataFromService(QueryString).ConfigureAwait(false);
        }
    }
}
using Newtonsoft.Json.Linq;
using MauiAppTempoAgora.Models;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo> GetTempo(string cidade)
        {
            Tempo? t = null;

            string chave = "a81608e330afee8ba11be210531bce7f";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new())
            {
                HttpResponseMessage resp = await client.GetAsync(url);
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime tempo = new();
                    DateTime sunrise = tempo.AddSeconds((double)rascunho["sys"]["sunrise"]);
                    DateTime sunset = tempo.AddSeconds((double)rascunho["sys"]["sunset"]);

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["main"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        temp_min = (double)rascunho["main"]["temp_min"],

                        temp = (double)rascunho["main"]["temp"],
                        feels_like = (double)rascunho["main"]["feels_like"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise,
                        sunset = sunset,
                        timezone = (int)rascunho["timezone"],
                        icon = (string)rascunho["weather"][0]["icon"]
                    };
                }
            }

            return t;
        }
    }
}

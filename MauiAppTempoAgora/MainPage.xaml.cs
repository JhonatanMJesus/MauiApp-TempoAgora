using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(txt_cidade.Text != "")
                {
                    Tempo? t = await DataService.GetTempo(txt_cidade.Text);
                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n " +
                                        $"Longitude: {t.lon} \n " +
                                        $"Temperatura: {t.temp}";
                        lbl_previsao.Text = dados_previsao;

                        string mapa = $"https://embed.windy.com/embed.html?" +
                            $"type=map&location=coordinates&metricRain=mm&" +
                            $"metricTemp=ºC&metricWind=km/h&zoom=5&overlay=wind&" +
                            $"product=ecmwf&level=surface&" +
                            $"lat={t.lat.ToString().Replace(",", ".")}&" +
                            $"lon={t.lon.ToString().Replace(",", ".")}";

                        wv_mapa.Source = mapa;
                    }
                }
                else
                {
                    throw new Exception("Informe a cidade.");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Ops", ex.Message, "Ok");
            }
        }
    }
}

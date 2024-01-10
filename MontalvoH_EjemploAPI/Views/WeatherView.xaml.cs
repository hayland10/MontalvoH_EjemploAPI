using Newtonsoft.Json;
using MontalvoH_EjemploAPI.Models;


namespace MontalvoH_EjemploAPI.Views;

public partial class WeatherView : ContentPage
{
	public WeatherView()
	{
		InitializeComponent();
	}

    private async void Consultar_Clicked(object sender, EventArgs e)
    {
		string latitud = lblLatitud.Text;
		string longitud = lblLongitud.Text;

		if (Connectivity.NetworkAccess == NetworkAccess.Internet) {
			using (var client = new HttpClient())
			{
				string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitud}&lon={longitud}&appid=bcb8594db7430c9c004c82b7d489d1f5";
				var response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode) {
					var json = await response.Content.ReadAsStringAsync();
					var clima = JsonConvert.DeserializeObject<Root>(json);
					lblclima.Text = clima.weather[0].main;
					lblPais.Text = clima.sys.country;
					lblciudad.Text = clima.name;
				}
			}
		}
    }
}
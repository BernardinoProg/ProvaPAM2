using System.Runtime.InteropServices.Marshalling;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Enderecos;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
namespace AppRpgEtec.ViewModels.Usuarios;

public class LocalizacaoViewModel_ : ContentPage
{
	private Map meuMapa;

    private EnderecoService enderecoService;
	private string cep = "02110-010";
    public string Cep
    {
        get => cep;
        set
        {
            cep = value;
            OnPropertyChanged();
        }
    }
    public Map MeuMapa
	{
		get => meuMapa;
		set
		{
			if (value != null)
			{
				meuMapa = value;
				OnPropertyChanged();
			}
		}
	}

	public async void InicializarMapa()
	{
		try
		{
			Location location = new Location(-26.5200241d, -46.596498d);
			Pin pinEtec = new Pin() { 
			Type=PinType.Place,
			Label = "Etec Horácio",
			Address = "Rua alcântara, 113,Vila Guilherme",
			Location = location
			};

			Map map = new Map();
			MapSpan mapSpan = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(5));
			map.Pins.Add(pinEtec);
			map.MoveToRegion(mapSpan);
			MeuMapa = map;
			
		}
		catch (Exception ex)
		{
			await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
		}
	}

	public async void ObterEnderecoCep()
	{
		try
		{
			Endereco endereco = await enderecoService.GetEnderecoCep(cep);

            Location location = new Location(Double.Parse(endereco.lat), Double.Parse(endereco.lon));

            Pin pinEtec = new Pin()
            {
                Type = PinType.Place,
                Label = endereco.name,
                Address = endereco.display_name,
                Location = location
            };

        }
		catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
        }
	}

	public ICommand ObterEnderecoCommand { get; set; }

    public void InicializarCommands()
    {
		ObterEnderecoCommand = new Command(async () => ObterEnderecoCep());
    }

    public LocalizacaoViewModel_()
	{
		InicializarCommands();

        Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}
using AppRpgEtec.ViewModels.Usuarios;

namespace AppRpgEtec.Views.Usuarios;

public partial class LocalizacaoView : ContentPage
{
    LocalizacaoViewModel_ viewModel;
    public LocalizacaoView()
	{
		InitializeComponent();

		viewModel = new LocalizacaoViewModel_();
		viewModel.InicializarMapa();

		BindingContext = viewModel;
	}
}
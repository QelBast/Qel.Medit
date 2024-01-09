using Qel.Medit.Addons;

namespace Qel.Medit.ApplicationService;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private void ToAuthorizationModalStack(object sender, EventArgs e)
    {
		Navigation.PushModalAsync(new LoginPage(), true);
    }

    private async void OnTimerNavClicked(object sender, EventArgs e)
    {
        await Navigation.PageSwap<TimerPage>(this);
    }

    private async void OnHomeNavClicked(object sender, EventArgs e)
    {
        await Navigation.PageSwap<MainPage>(this);
    }

    private void OnProfileNavClicked(object sender, EventArgs e)
    {
        
    }
}
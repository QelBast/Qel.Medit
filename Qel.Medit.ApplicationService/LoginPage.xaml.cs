using Qel.Medit.Addons.Services;
using Qel.Medit.Dal;

namespace Qel.Medit.ApplicationService;

public partial class LoginPage : ContentPage
{
    private AuthService _authService;

    public LoginPage()
    {
        InitializeComponent();
        _authService = new AuthService(DbContextMain.CreateContext());
    }

    private void RegisterButton_Clicked(object sender, EventArgs e)
    {
        _authService.RegisterUser(UsernameEntry.Text, PasswordEntry.Text);
        DisplayAlert("Success", "Registration successful", "OK");
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        bool isAuthenticated = _authService.ValidateUserCredentials(UsernameEntry.Text, PasswordEntry.Text);

        if (isAuthenticated)
        {
            var result = new TaskCompletionSource(DisplayAlert("Success", "Login successful", "OK"));
            if(result.TrySetResult())
            {
                Navigation.RemovePage(this);
            }
        }
        else
        {
            await DisplayAlert("Error", "Invalid credentials", "OK");
        }
    }
}
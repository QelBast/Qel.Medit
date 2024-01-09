//using Android.Media;

using Qel.Medit.Addons;
using System.Linq;

namespace Qel.Medit.ApplicationService;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    public async void PlayAudio()
    {
        //if (this.audioPlayer == null)
        //{
        //    this.audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("audio.mp3"));
        //    audioPlayer.Loop = true;
        //}
        //audioPlayer.Play();
    }

    private async void OnTimerNavClicked(object sender, EventArgs e)
    {
        await Navigation.PageSwap<TimerPage>(this);
    }

    private void OnHomeNavClicked(object sender, EventArgs e)
    {

    }

    private async void OnProfileNavClicked(object sender, EventArgs e)
    {
        await Navigation.PageSwap<ProfilePage>(this);
    }
}

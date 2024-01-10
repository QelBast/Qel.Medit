using Qel.Medit.Addons;

namespace Qel.Medit.ApplicationService;

public partial class TimerPage : ContentPage
{
    //readonly IAudioManager audioManager;

    //private IAudioPlayer audioPlayer;
    private bool isMeditating = false; // Flag to track meditation status
    private TimeSpan remainingTime; // Remaining time for the meditation session

    private StackLayout stackLayout = new StackLayout();

    private IDispatcherTimer timer;

    public TimerPage()
	{
        //this.audioManager = AudioManager.Current;
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(1000);
        InitializeComponent();
        bool isAuthenticated = false;
        if (!isAuthenticated)
        {
            // If not authenticated, navigate to the LoginPage
            //Navigation.PushAsync(new LoginPage());
        }
        timer.Tick += (s, e) =>
        {
            if (isMeditating)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                countdownLabel.Text = $"Time Remaining: {remainingTime.ToString(@"mm\:ss")}";
            }


            if (remainingTime <= TimeSpan.Zero)
            {
                StopMeditation(false);
            }

        };
    }

    private async void OnMeditationDurationClicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;

        btn5min.IsEnabled = false;
        btn10min.IsEnabled = false;
        btn15min.IsEnabled = false;
        btnCustom.IsEnabled = false;
        switch (clickedButton.Text)
        {
            case "5 min":
                await StartMeditationSession(TimeSpan.FromMinutes(5));
                break;

            case "10 min":
                await StartMeditationSession(TimeSpan.FromMinutes(10));
                break;

            case "15 min":
                await StartMeditationSession(TimeSpan.FromMinutes(15));
                break;

            case "Custom":
                string userInput = await DisplayPromptAsync("Custom Duration", "Enter the duration in minutes", maxLength: 3, keyboard: Keyboard.Numeric);

                if (int.TryParse(userInput, out int customMinutes) && customMinutes > 0)
                {
                    await StartMeditationSession(TimeSpan.FromMinutes(customMinutes));
                }
                else
                {
                    EnableDurationButtons();
                    await DisplayAlert("Invalid Input", "Please enter a valid positive duration.", "OK");
                }
                break;
        }
    }

    private async void StopMeditation(bool halted = false)
    {
        stackLayout.Children.Remove(pauseButton);
        stackLayout.Children.Remove(stopButton);
        //this.audioPlayer.Stop();
        isMeditating = false;
        EnableDurationButtons();
        this.timer?.Stop();
        if (!halted)
        {
            await DisplayAlert("Congratulations!", "You have successfully completed your meditation... or whatever.", "OK");
        }
        else
        {
            //await DisplayAlert("You are weak.", "OK");
        }
    }

    private async Task StartMeditationSession(TimeSpan meditationTime)
    {
        isMeditating = true;
        remainingTime = meditationTime;
        //PlayAudio();

        statusLabel.Text = "Meditation in Progress";
        countdownLabel.Text = $"Time Remaining: {remainingTime.ToString(@"mm\:ss")}";

        pauseButton.IsEnabled = true;
        pauseButton.IsVisible = true;
        stopButton.IsEnabled = true;
        stopButton.IsVisible = true;

        this.timer.Start();
    }


    private void OnPauseClicked(object sender, EventArgs e)
    {
        isMeditating = false;
        //this.audioPlayer.Pause();
        Button pauseButton = (Button)sender;
        pauseButton.Text = "Resume";
        pauseButton.Clicked -= OnPauseClicked;
        pauseButton.Clicked += OnResumeClicked;
    }

    private void OnResumeClicked(object sender, EventArgs e)
    {
        isMeditating = true;
        //this.audioPlayer.Play();
        Button resumeButton = (Button)sender;
        resumeButton.Text = "Pause";
        resumeButton.Clicked -= OnResumeClicked;
        resumeButton.Clicked += OnPauseClicked;
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        StopMeditation(true);
    }

    private void EnableDurationButtons()
    {
        btn5min.IsEnabled = true;
        btn10min.IsEnabled = true;
        btn15min.IsEnabled = true;
        btnCustom.IsEnabled = true;
        pauseButton.IsEnabled = false;
        pauseButton.IsVisible = false;
        stopButton.IsEnabled = false;
        stopButton.IsVisible = false;
    }

    private void OnTimerNavClicked(object sender, EventArgs e)
    {
        
    }

    private async void OnHomeNavClicked(object sender, EventArgs e)
    {
        await Navigation.PageSwap<MainPage>();
    }

    private async void OnProfileNavClicked(object sender, EventArgs e)
    {
        await Navigation.PageSwap<ProfilePage>();
    }
}
namespace Project;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        int highScore = Preferences.Get("HighScore", 0);
        HighScoreDisplay.Text = highScore.ToString();
    }

    private async void OnResetButtonClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Reset High Score?",
            "Are you sure you want to reset your high score to 0?",
            "Yes, Reset",
            "Cancel");

        if (confirm)
        {
            Preferences.Set("HighScore", 0);
            HighScoreDisplay.Text = "0";

            await DisplayAlert("Success", "High score has been reset!", "OK");
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
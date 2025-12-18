namespace Project;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainMenuPage())
        {
            BarBackgroundColor = Colors.Black,
            BarTextColor = Colors.White
        };
    }
}
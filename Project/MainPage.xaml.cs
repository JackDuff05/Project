namespace Project;

public partial class MainPage : ContentPage
{
    private RoadDrawing myRoad;

    private IDispatcherTimer animationTimer;

    private bool isAnimating = false;

    private int currentLane = 1;

    private const double LANE_SPACING = 100.0; 

    public MainPage()
    {
        InitializeComponent();

        myRoad = new RoadDrawing();

        RoadCanvas.Drawable = myRoad;

        animationTimer = Dispatcher.CreateTimer();
        animationTimer.Interval = TimeSpan.FromMilliseconds(16); 
        animationTimer.Tick += OnTimerTick;

        isAnimating = true;
        animationTimer.Start();

        UpdateCarPosition();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        myRoad.Update();

        RoadCanvas.Invalidate();
    }

    private void OnLeftButtonClicked(object sender, EventArgs e)
    {
        if (currentLane > 0)
        {
            currentLane--;
            UpdateCarPosition();
        }
    }

    private void OnRightButtonClicked(object sender, EventArgs e)
    {
        if (currentLane < 2)
        {
            currentLane++;
            UpdateCarPosition();
        }
    }

    private void UpdateCarPosition()
    {
        double horizontalOffset = (currentLane - 1) * LANE_SPACING;

        PlayerCar.TranslationX = horizontalOffset;
    }
}
namespace Project;

public partial class MainPage : ContentPage
{
    
    private RoadDrawing myRoad;
    private IDispatcherTimer animationTimer;
    private bool isAnimating = false;

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
    }

    private void OnTimerTick(object sender, EventArgs e)
    { 
        myRoad.Update();
        
        RoadCanvas.Invalidate();
    }
}
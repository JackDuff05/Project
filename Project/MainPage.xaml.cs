using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Layouts;

namespace Project;

public partial class MainPage : ContentPage
{
    private RoadDrawing myRoad;

    private IDispatcherTimer animationTimer;
    private IDispatcherTimer enemySpawnTimer;

    private bool isAnimating = false;

    private int currentLane = 1;

    private const double LANE_SPACING = 100.0;

    private List<EnemyCar> enemyCars = new List<EnemyCar>();
    private Random random = new Random();


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

        enemySpawnTimer = Dispatcher.CreateTimer();
        enemySpawnTimer.Interval = TimeSpan.FromMilliseconds(2000); 
        enemySpawnTimer.Tick += SpawnEnemyCar;
        enemySpawnTimer.Start();

        UpdateCarPosition();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        myRoad.Update();

        RoadCanvas.Invalidate();

        
        for (int i = enemyCars.Count - 1; i >= 0; i--)
        {
            enemyCars[i].Update();

            if (i == 0)
            {
                System.Diagnostics.Debug.WriteLine($"Enemy 0 position: {enemyCars[i].yPosition}");
            }

            if (enemyCars[i].yPosition > 800)
            {
                GameLayout.Children.Remove(enemyCars[i].carImage);

                enemyCars.RemoveAt(i);
                System.Diagnostics.Debug.WriteLine($"Removed enemy car");
            }
        }
    }
    private void SpawnEnemyCar(object sender, EventArgs e)
    {
        if (isAnimating)
        {
            int randomLane = random.Next(0, 3); 

            string[] carImages = { "bluecar.png", "greencar.png", "yellowcar.png", "policecar.png", "truckenemy.png" };
            string randomCarImage = carImages[random.Next(carImages.Length)];

            Image enemyCarImage = new Image
            {
                Source = randomCarImage,  
                WidthRequest = 100,
                HeightRequest = 150
            };

            AbsoluteLayout.SetLayoutBounds(enemyCarImage, new Rect(0, 0, 100, 150));
            AbsoluteLayout.SetLayoutFlags(enemyCarImage, AbsoluteLayoutFlags.None);

            GameLayout.Children.Add(enemyCarImage);

            EnemyCar newEnemy = new EnemyCar(randomLane, enemyCarImage);
            enemyCars.Add(newEnemy);
        }
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
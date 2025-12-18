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


    //   private BoxView playerCollisionBox;
    //  private List<BoxView> enemyCollisionBoxes = new List<BoxView>();

    /*   private void SetupCollisionDebug()      //added this collition debugging box for player car
       {
           playerCollisionBox = new BoxView
           {
               Color = Colors.Red,
               Opacity = 0.3,
               WidthRequest = 70,  
               HeightRequest = 100  
           };

           AbsoluteLayout.SetLayoutBounds(playerCollisionBox, new Rect(165, 415, 70, 100));
           AbsoluteLayout.SetLayoutFlags(playerCollisionBox, AbsoluteLayoutFlags.None);
           GameLayout.Children.Add(playerCollisionBox);
       } */

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
       // SetupCollisionDebug();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        myRoad.Update();
        RoadCanvas.Invalidate();

        if (CheckCollision())
        {
            animationTimer.Stop();
            enemySpawnTimer.Stop();
            isAnimating = false;

            ShowGameOver();
            return;
        }

        for (int i = enemyCars.Count - 1; i >= 0; i--)
        {
            enemyCars[i].Update();

            if (enemyCars[i].yPosition > 800)
            {
                GameLayout.Children.Remove(enemyCars[i].carImage);

                
                if (enemyCars[i].collisionBox != null)
                {
                    GameLayout.Children.Remove(enemyCars[i].collisionBox);
                }

                enemyCars.RemoveAt(i);
            }
        }
    }

    private async void ShowGameOver()
    {
        bool restart = await DisplayAlert("Game Over!", "You crashed!", "Restart", "Main Menu");

        if (restart)
        {
            RestartGame();
        }
        else
        {
            await Navigation.PopAsync();
        }
    }

    private void RestartGame()
    {
        for (int i = enemyCars.Count - 1; i >= 0; i--)
        {
            GameLayout.Children.Remove(enemyCars[i].carImage);

            if (enemyCars[i].collisionBox != null)
            {
                GameLayout.Children.Remove(enemyCars[i].collisionBox);
            }
        }
        enemyCars.Clear();

        currentLane = 1;
        UpdateCarPosition();

        myRoad = new RoadDrawing();
        RoadCanvas.Drawable = myRoad;

        isAnimating = true;
        animationTimer.Start();
        enemySpawnTimer.Start();
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


            /*   BoxView enemyBox = new BoxView
               {
                   Color = Colors.Blue,
                   Opacity = 0.3,
                   WidthRequest = 70,  
                   HeightRequest = 100 
               }; */  //this one is for debugging collision box for enemy cars

            AbsoluteLayout.SetLayoutBounds(enemyCarImage, new Rect(0, 0, 100, 150));
            AbsoluteLayout.SetLayoutFlags(enemyCarImage, AbsoluteLayoutFlags.None);
            GameLayout.Children.Add(enemyCarImage);

          //  AbsoluteLayout.SetLayoutBounds(enemyBox, new Rect(0, 0, 70, 100));
          //  AbsoluteLayout.SetLayoutFlags(enemyBox, AbsoluteLayoutFlags.None);
          //  GameLayout.Children.Add(enemyBox);

            EnemyCar newEnemy = new EnemyCar(randomLane, enemyCarImage);
         // newEnemy.collisionBox = enemyBox;
            enemyCars.Add(newEnemy);
         // enemyCollisionBoxes.Add(enemyBox);
        }
    }

    private bool CheckCollision()
    {
        double baseX = 165;  

        double playerX = baseX + PlayerCar.TranslationX;
        double playerY = 415;  
        double playerWidth = 70;   
        double playerHeight = 100;

        foreach (var enemy in enemyCars)
        {
            float enemyX = enemy.xPosition + 15;  
            float enemyY = enemy.yPosition + 25;

            bool overlapX = playerX < enemyX + enemy.collisionWidth &&
                           playerX + playerWidth > enemyX;

            bool overlapY = playerY < enemyY + enemy.collisionHeight &&
                           playerY + playerHeight > enemyY;

            if (overlapX && overlapY)
            {
                return true;
            }
        }

        return false;
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

       // if (playerCollisionBox != null)
        {
         //   playerCollisionBox.TranslationX = horizontalOffset;
        }
    }
}
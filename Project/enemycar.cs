namespace Project;

public class EnemyCar
{
    public float xPosition;
    public float yPosition;
    public int lane;
    public float speed = 5f;
    public float carWidth = 100f;
    public float carHeight = 150f;
    public float collisionWidth = 70f;  
    public float collisionHeight = 100f;
    public Image carImage;
    public BoxView collisionBox;

    public EnemyCar(int laneNumber, Image imageElement)
    {
        lane = laneNumber;
        carImage = imageElement;

        if (lane == 0)
            xPosition = 45f;
        else if (lane == 1)
            xPosition = 145f;
        else
            xPosition = 245f;

        yPosition = -150f;

        UpdateImagePosition();
    }

    public void Update()
    {
        yPosition += speed;
        UpdateImagePosition();
    }

    private void UpdateImagePosition()
    {
        AbsoluteLayout.SetLayoutBounds(carImage, new Rect(xPosition, yPosition, carWidth, carHeight));

        if (collisionBox != null)
        {
            float collisionX = xPosition + 15; 
            float collisionY = yPosition + 25; 
            AbsoluteLayout.SetLayoutBounds(collisionBox, new Rect(collisionX, collisionY, collisionWidth, collisionHeight));
        }
    }
}
namespace Project;

public class EnemyCar
{
    public float xPosition;
    public float yPosition;
    public int lane; 
    public float speed = 5f;
    public float carWidth = 100f;
    public float carHeight = 150f;
    public Image carImage; 

    public EnemyCar(int laneNumber, Image imageElement)
    {
        lane = laneNumber;
        carImage = imageElement;

        if (lane == 0)
            xPosition = 50f;   
        else if (lane == 1)
            xPosition = 150f; 
        else
            xPosition = 250f; 

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
    }
}

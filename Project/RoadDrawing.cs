using Microsoft.Maui.Graphics;

namespace Project;

public class RoadDrawing : IDrawable
{
    public float roadWidth = 300f;
   
    public int numberOfLanes = 3;
    
    public float laneWidth;
    
    public float lineLength = 40f;  
    public float gapLength = 20f;   
    public float speed = 5f;        

    public float offset = 0f;

    public RoadDrawing()
    {
        laneWidth = roadWidth / numberOfLanes;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {

        float middleOfScreen = dirtyRect.Width / 2;

        float leftEdgeOfRoad = middleOfScreen - roadWidth / 2;

        canvas.FillColor = Colors.Green.WithAlpha(0.3f);
        canvas.FillRectangle(dirtyRect);

        canvas.FillColor = Colors.DarkGray;
        canvas.FillRectangle(leftEdgeOfRoad, 0, roadWidth, dirtyRect.Height);

        canvas.StrokeColor = Colors.White;
        canvas.StrokeSize = 3;

        canvas.DrawLine(leftEdgeOfRoad, 0, leftEdgeOfRoad, dirtyRect.Height);

        canvas.DrawLine(leftEdgeOfRoad + roadWidth, 0, leftEdgeOfRoad + roadWidth, dirtyRect.Height);

        canvas.StrokeColor = Colors.Yellow;
        canvas.StrokeSize = 2;

        canvas.StrokeDashPattern = new float[] { lineLength, gapLength };

        canvas.StrokeDashOffset = offset;

        float firstDividerX = leftEdgeOfRoad + laneWidth;
        canvas.DrawLine(firstDividerX, 0, firstDividerX, dirtyRect.Height);

        float secondDividerX = leftEdgeOfRoad + (laneWidth * 2);
        canvas.DrawLine(secondDividerX, 0, secondDividerX, dirtyRect.Height);

        canvas.StrokeDashPattern = null;
    }

    public void Update()
    {   
        offset = offset - speed;
        if (offset < 0)
        {
            offset = lineLength + gapLength;
        }
    }
}

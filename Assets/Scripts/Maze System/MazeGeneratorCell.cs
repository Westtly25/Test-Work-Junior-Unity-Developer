public class MazeGeneratorCell
{
    private int x;
    private int y;

    private bool wallLeft = true;
    private bool wallBottom = true;
    private bool floor = true;

    private bool isVisited = false;
    private int distanceFromStart;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    
    public bool WallLeft { get => wallLeft; set => wallLeft = value; }
    public bool WallBottom { get => wallBottom; set => wallBottom = value; }
    public bool Floor { get => floor; set => floor = value; }

    public bool IsVisited { get => isVisited; set => isVisited = value; }

    public int DistanceFromStart { get => distanceFromStart; set => distanceFromStart = value; }
}
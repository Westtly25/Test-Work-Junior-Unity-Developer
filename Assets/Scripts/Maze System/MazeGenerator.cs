using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    public MazeGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public MazeData GenerateMaze()
    {
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[width, height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeGeneratorCell { X = x, Y = y };
            }
        }

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, height - 1].WallLeft = false;
            cells[x, height - 1].Floor = false;
        }

        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[width - 1, y].WallBottom = false;
            cells[width - 1, y].Floor = false;
        }

        RemoveWallsWithBacktracker(cells);

        MazeData mazeData = new MazeData
        {
            Cells = cells,
            StartPosition = PlaceStart(),
            FinishPosition = PlaceFinish(cells),
            CellsPathToFinish = FindPathToFinish(cells)
        };

        return mazeData;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.IsVisited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();

        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].IsVisited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].IsVisited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < width - 2 && !maze[x + 1, y].IsVisited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < width - 2 && !maze[x, y + 1].IsVisited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

                RemoveWall(current, chosen);

                chosen.IsVisited = true;
                stack.Push(chosen);
                chosen.DistanceFromStart = current.DistanceFromStart + 1;
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }


        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell current, MazeGeneratorCell chosen)
    {
        if ( current.X == chosen.X )
        {
            if ( current.Y > chosen.Y )
            {
                current.WallBottom = false;
            } else chosen.WallBottom = false;
        }
        else
        {
            if ( current.X > chosen.X )
            {
                current.WallLeft = false;
            } else chosen.WallLeft = false;
        }
    }

    private Vector3Int PlaceStart()
    {
        return new Vector3Int(0, 0, 0);
    }

    private Vector3Int PlaceFinish(MazeGeneratorCell[,] cells)
    {
        return new Vector3Int(cells.GetLength(0) - 2, 0, cells.GetLength(1) - 2);
    }

    private List<Vector3> FindPathToFinish(MazeGeneratorCell[,] cells)
    {
        Vector3Int currentPosition = PlaceFinish(cells);
        List<Vector3> positions = new List<Vector3>();

        int x = currentPosition.x;
        int y = currentPosition.z;

        while ((x != 0 || y != 0) && positions.Count < 100000)
        {
            positions.Add(new Vector3(x + 0.5f, 0.1f, y + 0.5f));

            MazeGeneratorCell currentCell = cells[x, y];

            if (x > 0 &&
                !currentCell.WallLeft &&
                cells[x - 1, y].DistanceFromStart < currentCell.DistanceFromStart)
            {
                x--;
            }
            else if (y > 0 &&
                    !currentCell.WallBottom &&
                    cells[x, y - 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                y--;
            }
            else if (x < cells.GetLength(0) - 1 &&
                    !cells[x + 1, y].WallLeft &&
                    cells[x + 1, y].DistanceFromStart < currentCell.DistanceFromStart)
            {
                x++;
            }
            else if (y < cells.GetLength(1) - 1 &&
                    !cells[x, y + 1].WallBottom &&
                    cells[x, y + 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                y++;
            }
        }

        positions.Add(new Vector3(0.5f, 0.1f, 0.5f));

        return positions;
    }
}
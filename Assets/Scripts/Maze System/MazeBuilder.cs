using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeBuilder : MonoBehaviour
{
    [SerializeField] private MazeCell mazeCellPrefab;

    [Header("Maze Data")]
    [SerializeField] [Range(10, 50)]
    private int mazeWidth;
    [SerializeField][Range(10, 50)]
    private int mazeHeight;

    [SerializeField]
    private MazeData maze;
    public MazeData Maze => maze;

    public event Action<List<Vector3>> OnMazeGenerated;

    public void Initialize()
    {
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        MazeGenerator generator = new MazeGenerator(mazeWidth, mazeHeight);
        maze = generator.GenerateMaze();

        for (int x = 0; x < maze.Cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.Cells.GetLength(1); y++)
            {
                MazeCell cell = Instantiate(mazeCellPrefab, new Vector3(x * mazeCellPrefab.MazeCellSize.x,
                                                                        y * mazeCellPrefab.MazeCellSize.y,
                                                                        y * mazeCellPrefab.MazeCellSize.z), Quaternion.identity,
                                                                        transform);
                cell.WallLeft.SetActive(maze.Cells[x, y].WallLeft);
                cell.WallBottom.SetActive(maze.Cells[x, y].WallBottom);
                cell.Floor.SetActive(maze.Cells[x, y].Floor);
            }
        }

        OnMazeGenerated?.Invoke(maze.CellsPathToFinish);
    }

    public void DisposeMaze()
    {
        maze = null;

        for (int i = 0; i < transform.childCount ; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
}

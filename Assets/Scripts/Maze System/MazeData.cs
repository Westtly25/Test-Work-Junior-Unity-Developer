using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeData
{
    private MazeGeneratorCell[,] cells;
    private List<Vector3> cellsPathToFinish;
    private Vector3Int startPosition;
    private Vector3Int finishPosition;

    public MazeGeneratorCell[,] Cells { get => cells; set => cells = value; }
    public Vector3Int StartPosition { get => startPosition; set => startPosition = value; }
    public Vector3Int FinishPosition { get => finishPosition; set => finishPosition = value; }
    public List<Vector3> CellsPathToFinish { get => cellsPathToFinish; set => cellsPathToFinish = value; }
}
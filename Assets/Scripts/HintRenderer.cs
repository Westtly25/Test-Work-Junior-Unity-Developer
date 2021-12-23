using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HintRenderer : MonoBehaviour
{
    [SerializeField] private MazeBuilder mazeBuilder;

    [SerializeField] private LineRenderer lineRenderer;

    private void OnEnable() => lineRenderer = GetComponent<LineRenderer>();
    private void OnDisable() => mazeBuilder.OnMazeGenerated -= DrawPath;

    public void Initialize(MazeBuilder mazeBuilder)
    {
        this.mazeBuilder = mazeBuilder;

        DrawPath(this.mazeBuilder.Maze.CellsPathToFinish);
    }

    private void DrawPath(List<Vector3> positions)
    {
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }

    public void ClearDrawPath()
    {
        lineRenderer.positionCount = 0;
        mazeBuilder.OnMazeGenerated -= DrawPath;
    }
}

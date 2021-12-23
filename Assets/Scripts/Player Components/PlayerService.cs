using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [SerializeField] private PlayerController playerPrefab;

    private PlayerController activePlayer;
    private Vector3 positionToSpawnWithOffset;

    public void Initialize(MazeBuilder mazeBuilder, IViewService viewService)
    {
        if (activePlayer != null) { return; }

        positionToSpawnWithOffset = new Vector3(mazeBuilder.Maze.StartPosition.x + 0.5f, 0, mazeBuilder.Maze.StartPosition.z + 0.5f);

        activePlayer = Instantiate(playerPrefab, positionToSpawnWithOffset, Quaternion.identity);
        activePlayer.Initialize(mazeBuilder.Maze.CellsPathToFinish, viewService);
    }

    public void RespawnPlayer()
    {
        activePlayer.transform.position = positionToSpawnWithOffset;
        activePlayer.ResetPathPosition();
    }

    public void DisposePlayer()
    {
        Destroy(activePlayer.gameObject);
        activePlayer = null;
    }
}
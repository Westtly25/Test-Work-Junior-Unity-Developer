using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour, IGameService
{
    [SerializeField] private ViewService viewService;
    [SerializeField] private MazeBuilder mazeBuilder;
    [SerializeField] private ZoneService zoneService;
    [SerializeField] private HintRenderer hintRenderer;
    [SerializeField] private PlayerService playerService;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        viewService.Initialize();
        mazeBuilder.Initialize();
        zoneService.Initialize(mazeBuilder, playerService, this);
        hintRenderer.Initialize(mazeBuilder);
        playerService.Initialize(mazeBuilder, viewService);
        
    }

    public void RestartGame()
    {
        mazeBuilder.DisposeMaze();
        zoneService.Dispose();
        hintRenderer.ClearDrawPath();
        playerService.DisposePlayer();

        viewService.ActivateFader();
        Initialize();
    }
}


public interface IGameService
{
    public void RestartGame();
}
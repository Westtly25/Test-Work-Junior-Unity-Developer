using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneService : MonoBehaviour
{
    private List<DamageZone> damageZones;
    private FinishZone finish;

    [Header("Prefabs")]
    [SerializeField] private DamageZone damageZone;
    [SerializeField] private FinishZone finishZone;
    
    [Header("Damage Zone Count to Instantiate")] [Range(1, 10)]
    [SerializeField] private int damageZoneCount = 4;

    [Header("Cached Components")]
    private MazeBuilder mazeBuilder;
    private PlayerService playerService;
    private IGameService gameService;

    public void Initialize(MazeBuilder mazeBuilder, PlayerService playerService, IGameService gameService)
    {
        this.mazeBuilder = mazeBuilder;
        this.playerService = playerService;
        this.gameService = gameService;

        PlaceDamageZones();
        PlaceFinishZone();
    }

    private void PlaceDamageZones()
    {
        damageZones = new List<DamageZone>();

        var path = mazeBuilder.Maze.CellsPathToFinish;
        List<int> selectedIndexes = new List<int>();

        for (int i = 0; i < damageZoneCount; i++)
        {
            int index = Random.Range(1, path.Count - 1);
            selectedIndexes.Add(index);

            DamageZone zone = Instantiate(damageZone, path[index], Quaternion.identity, transform);
            damageZones.Add(zone);

            zone.OnPlayerDamaged += OnPlayerRespawn;
        }
    }

    private void OnPlayerRespawn()
    {
        playerService.RespawnPlayer();
    }

    private void OnPlayerFinishedLevel()
    {
        gameService.RestartGame();
    }

    private void PlaceFinishZone()
    {
        Vector3 positionToSpawnWithOffset = new Vector3(mazeBuilder.Maze.FinishPosition.x + 0.5f, 0.1f, mazeBuilder.Maze.FinishPosition.z + 0.5f);

        finish = Instantiate(finishZone, positionToSpawnWithOffset, Quaternion.identity, transform);
        finish.OnPlayerFinished += OnPlayerFinishedLevel;

    }

    public void Dispose()
    {
        UnSubscribe();
        Destroy(finish.gameObject);

        for (int i = 0; i < transform.childCount ; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        damageZones = null;
        finish = null;
    }

    private void UnSubscribe()
    {
        for (int i = 0; i < damageZones.Count; i++)
        {
            damageZones[i].OnPlayerDamaged -= OnPlayerRespawn;
        }

        finish.OnPlayerFinished -= OnPlayerFinishedLevel;
    }
}
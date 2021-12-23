using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStateChanger))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool canMove = false;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int currentPosition = 0;
    [SerializeField] private List<Vector3> pathToMove;

    [SerializeField] private PlayerStateChanger playerStateChanger;

    public void Initialize(List<Vector3> pathToMove, IViewService viewService)
    {
        SetPathToMove(pathToMove);

        playerStateChanger.Initialize(viewService);

        canMove = true;
    }

    private void Update()
    {
        if (canMove == false || pathToMove == null) { return; }

        MoveAlongPath();
    }

    private void SetPathToMove(List<Vector3> pathToMove)
    {
        this.pathToMove = pathToMove;
        this.pathToMove.Reverse();
    }

    public void ResetPathPosition()
    {
        currentPosition = 0;
    }

    private void MoveAlongPath()
    {
        float speedMove =  speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, pathToMove[currentPosition], speedMove);

        if (Vector3.Distance(transform.position, pathToMove[currentPosition]) < 0.1f)
        {
            currentPosition++;
            if (currentPosition >= pathToMove.Count)
            {
                canMove = false;
                return;
            }
        }
    }
}

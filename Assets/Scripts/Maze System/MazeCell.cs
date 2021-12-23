using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallBottom;
    [SerializeField] private GameObject floor;
    [SerializeField] private Vector3 mazeCellSize;

    public GameObject WallLeft => wallLeft;
    public GameObject WallBottom => wallBottom;
    public GameObject Floor => floor;
    public Vector3 MazeCellSize => mazeCellSize;

}

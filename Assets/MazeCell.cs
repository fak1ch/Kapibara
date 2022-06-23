using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public GameObject WallLeft;
    public GameObject WallBottom;

    [SerializeField] private Material _greenMaterial;

    public void MakeWallsGreenExitFromMaze(bool isLeftWall)
    {
        if (isLeftWall == true)
        {
            WallLeft.SetActive(true);
            WallLeft.GetComponent<MeshRenderer>().material = _greenMaterial;
            WallLeft.AddComponent<MazePassed>();
        }
        else
        {
            WallBottom.SetActive(true);
            WallBottom.GetComponent<MeshRenderer>().material = _greenMaterial;
            WallBottom.AddComponent<MazePassed>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _mazeCell;
    [SerializeField] private RealtimeNavMeshBaker _environment;

    private void Start()
    {
        MazeGenerator mazeGenerator = new MazeGenerator();

        MazeGeneratorCell[,] maze = mazeGenerator.GenerateMaze();

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                MazeCell cell = Instantiate(_mazeCell, new Vector3(i * 1.81f - 0.5f, 0, j * 1.81f - 0.5f), Quaternion.Euler(0, 180, 0)).GetComponent<MazeCell>();

                cell.WallLeft.SetActive(maze[i, j].WallLeft);
                cell.WallBottom.SetActive(maze[i, j].WallBottom);

                cell.transform.SetParent(_environment.transform);
            }
        }

        _environment.BuildNavMesh(false);
    }
}

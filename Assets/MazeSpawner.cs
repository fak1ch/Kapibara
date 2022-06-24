using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _mazeCell;
    [SerializeField] private RealtimeNavMeshBaker _environment;
    [SerializeField] private MeshRenderer _ground;

    [SerializeField] private int _width;
    [SerializeField] private int _height;

    private MazeCell _exitCell;

    public void AfterSceneLoader(int width, int height)
    {
        _width = width;
        _height = height;

        _ground.material = StaticClass._groundMaterial;
        _mazeCell.GetComponent<MazeCell>().WallLeft.GetComponent<MeshRenderer>().material = StaticClass._wallMaterial;
        _mazeCell.GetComponent<MazeCell>().WallBottom.GetComponent<MeshRenderer>().material = StaticClass._wallMaterial;

        MazeGenerator mazeGenerator = new MazeGenerator(_width, _height);

        MazeGeneratorCell[,] maze = mazeGenerator.GenerateMaze();

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                MazeCell cell = Instantiate(_mazeCell, new Vector3(i * 1.81f - 0.5f, 0, j * 1.81f - 0.5f), Quaternion.Euler(0, 180, 0)).GetComponent<MazeCell>();

                cell.WallLeft.SetActive(maze[i, j].WallLeft);
                cell.WallBottom.SetActive(maze[i, j].WallBottom);

                cell.transform.SetParent(_environment.transform);

                if (maze[i, j] == mazeGenerator.ExitCell)
                    _exitCell = cell;
            }
        }

        _exitCell.MakeWallsGreenExitFromMaze(mazeGenerator._isLeftWall);

        _environment.BuildNavMesh(false);
    }
}

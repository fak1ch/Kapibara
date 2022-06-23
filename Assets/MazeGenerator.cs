using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator
{
    public MazeGeneratorCell ExitCell;
    public bool _isLeftWall = false;

    private int _width = 20;
    private int _height = 20;
     
    public MazeGenerator(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[_width, _height];

        for(int i=0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = new MazeGeneratorCell(i, j);
            }
        }

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            maze[i, _height - 1].WallLeft = false;
        }

        for (int i = 0; i < maze.GetLength(1); i++)
        {
            maze[_width - 1, i].WallBottom = false;
        }

        RemoveWallsWithBacktracker(maze);
        PlaceMazeExit(maze);

        return maze;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();

        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited)
                unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited)
                unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < _width - 2 && !maze[x + 1, y].Visited)
                unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < _height - 2 && !maze[x, y + 1].Visited)
                unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(current);
                current = chosen;
                chosen.DistanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell current, MazeGeneratorCell chosen)
    {
        if (current.X == chosen.X)
        {
            if (current.Y > chosen.Y)
                current.WallBottom = false;
            else
                chosen.WallBottom = false;
        }
        else
        {
            if (current.X > chosen.X)
                current.WallLeft = false;
            else
                chosen.WallLeft = false;
        }
    }

    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[0, 0];

        for(int i = 0; i < maze.GetLength(0); i++)
        {
            if (maze[i, _height - 2].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[i, _height - 2];
            if (maze[i, 0].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[i, 0];
        }

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            if (maze[_width - 2, i].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[_width - 2, i];
            if (maze[0, i].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[0, i];
        }

        if (furthest.X == 0)
        {
            furthest.WallLeft = false;
            ExitCell = furthest;
            _isLeftWall = true;
        }
        else if (furthest.Y == 0)
        {
            furthest.WallBottom = false;
            ExitCell = furthest;
            _isLeftWall = false;
        }
        else if (furthest.X == _width - 2)
        {
            maze[furthest.X + 1, furthest.Y].WallLeft = false;
            ExitCell = maze[furthest.X + 1, furthest.Y];
            _isLeftWall = true;
        }
        else if (furthest.Y == _height - 2)
        {
            maze[furthest.X, furthest.Y + 1].WallBottom = false;
            ExitCell = maze[furthest.X, furthest.Y + 1];
            _isLeftWall = false;
        }
    }
}

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool Visited = false;
    public int DistanceFromStart;

    public MazeGeneratorCell(int x, int y)
    {
        X = x;
        Y = y;
    }
}

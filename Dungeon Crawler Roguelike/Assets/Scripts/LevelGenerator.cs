using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [HideInInspector]
    public int width;
    [HideInInspector]
    public int height;
    [HideInInspector]
    public int minRooms;
    [HideInInspector]
    public int maxRooms;
    [HideInInspector]
    public int minRoomSize;
    [HideInInspector]
    public int maxRoomSize;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public int fillPercent;

    public GameObject[] floorTiles;
    public GameObject[] botMidWallTiles;
    public GameObject[] vertWallTiles;
    public GameObject[] obstacles;
    public GameObject botRightCorner;
    public GameObject botLeftCorner;
    public GameObject topRightCorner;
    public GameObject topLeftCorner;
    public GameObject exit;

    private int[,] map;
    private Transform levelHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    public void CreateLevel(int width, int height, int fillPercent)
    {
        this.width = width;
        this.height = height;
        this.fillPercent = fillPercent;
        map = new int[width + 2, height + 2];

        InitializeList();
        CreateFloor();
        PlaceTiles();
	
	}

    private void InitializeList()
    {
        gridPositions.Clear();

        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                gridPositions.Add(new Vector3(i, j, 0f));

            }
        }

    }

    private void CreateFloor()
    {
        RandomlyGenerateFloor();

        for (int i = 0; i < 4; i++)
        {
            SmoothFloor();
        }
    }

    private void RandomlyGenerateFloor()
    {
        //Debug.Log((map.GetLength(0) / 5).ToString() + " " + (map.GetLength(1) / 5).ToString());
        int[,] tempMap = new int[map.GetLength(0) - 2 / 5, map.GetLength(1) - 2 / 5];

        System.Random rand = new System.Random(System.DateTime.Now.GetHashCode());
        for (int i = 0; i < tempMap.GetLength(0); i++)
        {
            for(int j = 0; j < tempMap.GetLength(1); j++)
            {
                tempMap[i, j] = (rand.Next(0, 100) < fillPercent) ? 1 : 0;
            }
        }

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (i == 0 || i == map.GetLength(0) - 1 || j == 0 || j == map.GetLength(1) - 1)
                {
                    map[i, j] = 1;
                }
                else
                {
                    //Debug.Log((i ).ToString() + " " + (j ).ToString());
                    map[i, j] = tempMap[(i - 1) / 5, (j - 1) / 5];
                }
            }
        }
    }

    private void SmoothFloor()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int neighborWallTiles = GetSurroundingWallCount(i, j);

                if (neighborWallTiles > 4)
                {
                    map[i, j] = 1;
                }
                else if (neighborWallTiles < 4)
                {
                    map[i, j] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int x, int y)
    {
        int wallCount = 0;

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < map.GetLength(0) && j >= 0 && j < map.GetLength(1))
                {
                    if (i != x || j != y)
                    {
                        wallCount += map[i, j];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void PlaceTiles()
    {
        levelHolder = new GameObject("Level").transform;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for(int j = 0; j < map.GetLength(1); j++)
            {
                GameObject toInstantiate = new GameObject();
                if(map[i, j] == 0)
                {
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(levelHolder);
                }
                else if(map[i, j] == 1)
                {
                    int wallType = GetWallType(i, j);

                    switch(wallType)
                    {
                        case 1:
                            toInstantiate = botMidWallTiles[Random.Range(0, botMidWallTiles.Length)];
                            break;
                        case 2:
                            toInstantiate = vertWallTiles[Random.Range(0, vertWallTiles.Length)];
                            break;
                        case 3:
                            toInstantiate = topRightCorner;
                            break;
                        case 4:
                            toInstantiate = topLeftCorner;
                            break;
                        case 5:
                            toInstantiate = botRightCorner;
                            break;
                        case 6:
                            toInstantiate = botLeftCorner;
                            break;
                        default:
                            break;
                    }
                    GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(levelHolder);
                }
            }
        }

    }

    private int GetWallType(int i, int j)
    {
        // Completely Surrounded Wall
        if (GetSurroundingWallCount(i, j) == 8)
        {
            return 0;
        }
        // Top Right Corner
        if ((i == map.GetLength(0) - 1 && j == map.GetLength(1) - 1) || (i > 1 && j > 1 && i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1) 
            && (map[i - 1, j] == 1 && map[i, j - 1] == 1 
            && ((map[i - 1, j - 1] == 0) || (map[i + 1, j] == 0 && map[i, j + 1] == 0))))
        {
            return 3;
        }
        // Top Left Corner
        if ((i == 0 && (j == map.GetLength(1) - 1) || (i < map.GetLength(0) - 1 && j > 0 && map[i + 1, j] == 1 && map[i, j - 1] == 1 && (map[i + 1, j - 1] == 0))) 
            || (i > 1 && j > 1 && i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1) 
            && (map[i + 1, j] == 1 && map[i, j - 1] == 1 
            && ((map[i + 1, j - 1] == 0) || (map[i - 1, j] == 0 && map[i, j + 1] == 0 ))))
        {
            return 4;
        }
        // Bottom Right Corner
        if ((i == map.GetLength(0) - 1 && j == 0) || (i > 1 && j > 1 && i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1)
            && (map[i - 1, j] == 1 && map[i, j + 1] == 1 
            && ((map[i - 1, j + 1] == 0) || (map[i + 1, j] == 0 && map[i, j - 1] == 0))))
        {
            return 5;
        }
        // Bottom Left Corner
        if ((i == 0 && j == 0) || (i > 1 && j > 1 && i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1)
            && (map[i + 1, j] == 1 && map[i, j + 1] == 1 
            && ((map[i + 1, j + 1] == 0) || (map[i - 1, j] == 0 && map[i, j - 1] == 0))))
        {
            return 6;
        }
        // Bottom Mid Wall
        if ((i > 0 && j == 0) || (i > 0 && j == map.GetLength(1) - 1) 
            || (i > 1 && j > 1 && i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1)
            && map[i - 1, j] == 1 && map[i + 1, j] == 1 && (j == map.GetLength(1) - 1 || map[i, j - 1] == 0 || map[i, j + 1] == 0))
        {
            return 1;
        }
        // Vertical Wall
        if ((i > 1 && j > 1 && i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1)
            && map[i, j + 1] == 1 && map[i, j - 1] == 1 && (i == 0 || i == map.GetLength(1) - 1 || map[i - 1, j] == 0 || map[i + 1, j] == 0))
        {
            return 2;
        }

        return 0;
    }
}

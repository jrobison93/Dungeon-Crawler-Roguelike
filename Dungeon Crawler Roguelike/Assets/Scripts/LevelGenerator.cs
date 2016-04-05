using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelGenerator : MonoBehaviour
{
    [HideInInspector]
    public int width;
    [HideInInspector]
    public int height;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public int fillPercent;

    public GameObject[] floorTiles;
    public GameObject[] botMidWallTiles;
    public GameObject[] vertWallTiles;
    public GameObject[] obstacles;
    public GameObject[] potions;
    public GameObject[] powerUps;
    public GameObject botRightCorner;
    public GameObject botLeftCorner;
    public GameObject topRightCorner;
    public GameObject topLeftCorner;
    public GameObject background;
    public GameObject exit;

    private int[,] map;
    private GameObject player;
    private Transform levelHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    private List<Room> rooms;
    private System.Random rand = new System.Random(System.DateTime.Now.GetHashCode());
    private Coord playerLoc;
    private GameManager manager;
    private MovingObjectFactory EnemyFactory = new EnemyFactory();

    public void CreateLevel(int width, int height, int fillPercent, GameObject player)
    {
        this.width = width;
        this.height = height;
        this.fillPercent = fillPercent;
        this.player = player;
        map = new int[width + 2, height + 2];
        manager = GameManager.instance;
        manager.movingObjects = new bool[width, height];

        InitializeList();
        CreateFloor();
        AddPlayerAndCreatures();
        AddItems();
        AddExit();
	
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

        for (int i = 0; i < 2; i++)
        {
            SmoothFloor();
        }

        rooms = GetRooms();
        rooms.Sort();
        rooms[rooms.Count - 1].isMainRoom = true;
        rooms[rooms.Count - 1].isAccessibleFromMainRoom = true;

        ConnectClosestRooms(rooms);
        for (int i = 0; i < 2; i++)
        {
            SmoothFloor();
        }
        PlaceFloorTiles();
    }

    private void RandomlyGenerateFloor()
    {
        //Debug.Log((map.GetLength(0) / 5).ToString() + " " + (map.GetLength(1) / 5).ToString());
        int[,] tempMap = new int[map.GetLength(0) - 2 / 5, map.GetLength(1) - 2 / 5];

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
        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
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

    bool IsInMapRange(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    List<Coord> GetRegionTiles(int startX, int startY)
    {
        List<Coord> tiles = new List<Coord>();
        int[,] mapFlags = new int[width, height];
        int tileType = map[startX, startY];

        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(new Coord(startX, startY));
        mapFlags[startX, startY] = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();
            tiles.Add(tile);

            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
            {
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                {
                    if (IsInMapRange(x, y) && (y == tile.tileY || x == tile.tileX))
                    {
                        if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                        {
                            mapFlags[x, y] = 1;
                            queue.Enqueue(new Coord(x, y));
                        }
                    }
                }
            }
        }

        return tiles;
    }

    List<Room> GetRooms()
    {
        List<Room> rooms = new List<Room>();
        int[,] mapFlags = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (mapFlags[i, j] == 0 && map[i, j] == 0)
                {
                    List<Coord> newRegion = GetRegionTiles(i, j);
                    rooms.Add(new Room(newRegion, map));

                    foreach (Coord tile in newRegion)
                    {
                        mapFlags[tile.tileX, tile.tileY] = 1;
                    }
                }
            }
        }

        return rooms;
    }


    void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false)
    {
        List<Room> roomListA = new List<Room>();
        List<Room> roomListB = new List<Room>();

        if (forceAccessibilityFromMainRoom)
        {
            foreach (Room room in allRooms)
            {
                if (room.isAccessibleFromMainRoom)
                {
                    roomListB.Add(room);

                }
                else
                {
                    roomListA.Add(room);
                }
            }
        }
        else
        {
            roomListA = allRooms;
            roomListB = allRooms;
        }

        int bestDistance = 0;
        Coord bestTileA = new Coord();
        Coord bestTileB = new Coord();
        Room bestRoomA = new Room();
        Room bestRoomB = new Room();
        bool possibleConnectionFound = false;

        foreach (Room roomA in roomListA)
        {
            if (!forceAccessibilityFromMainRoom)
            {
                possibleConnectionFound = false;
                if (roomA.connectedRooms.Count > 0)
                {
                    continue;
                }
            }
            foreach (Room roomB in roomListB)
            {
                if (roomA == roomB || roomA.connectedRooms.Contains(roomB))
                {
                    continue;
                }
                for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
                {
                    Coord tileA = roomA.edgeTiles[tileIndexA];
                    for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++)
                    {
                        Coord tileB = roomB.edgeTiles[tileIndexB];
                        int distanceBetweenRooms = (int)(Mathf.Pow(tileA.tileX - tileB.tileX, 2) + Mathf.Pow(tileA.tileY - tileB.tileY, 2));

                        if (distanceBetweenRooms < bestDistance || !possibleConnectionFound)
                        {
                            bestDistance = distanceBetweenRooms;
                            possibleConnectionFound = true;
                            bestTileA = tileA;
                            bestTileB = tileB;
                            bestRoomA = roomA;
                            bestRoomB = roomB;
                        }
                    }
                }
            }

            if (possibleConnectionFound && !forceAccessibilityFromMainRoom)
            {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            }
        }

        if (possibleConnectionFound && forceAccessibilityFromMainRoom)
        {
            CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            ConnectClosestRooms(allRooms, true);
        }

        if (!forceAccessibilityFromMainRoom)
        {
            ConnectClosestRooms(allRooms, true);
        }
    }

    void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB)
    {
        Room.ConnectRooms(roomA, roomB);

        List<Coord> line = GetLine(tileA, tileB);
        foreach (Coord c in line)
        {
            DrawCircle(c, 1);
        }

    }
    
    Vector3 CoordToWorldPoint(Coord tile)
    {
        return new Vector3(-width / 2 + 0.5f + tile.tileX, 2, -height / 2 + 0.5f + tile.tileY);
    }

    void DrawCircle(Coord c, int r)
    {
        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r)
                {
                    int drawX = c.tileX + x;
                    int drawY = c.tileY + y;
                    if (IsInMapRange(drawX, drawY))
                    {
                        map[drawX, drawY] = 0;
                    }
                }
            }
        }
    }

    List<Coord> GetLine(Coord from, Coord to)
    {
        List<Coord> line = new List<Coord>();

        int x = from.tileX;
        int y = from.tileY;

        int dx = to.tileX - x;
        int dy = to.tileY - y;

        bool inverted = false;
        int step = Math.Sign(dx);
        int gradientStep = Math.Sign(dy);

        int longest = Math.Abs(dx);
        int shortest = Math.Abs(dy);

        if (longest < shortest)
        {
            inverted = true;
            longest = Math.Abs(dy);
            shortest = Math.Abs(dx);

            step = Math.Sign(dy);
            gradientStep = Math.Sign(dx);
        }

        int gradientAccumulation = longest / 2;
        for (int i = 0; i < longest; i++)
        {
            line.Add(new Coord(x, y));

            if (inverted)
            {
                y += step;
            }
            else
            {
                x += step;
            }

            gradientAccumulation += shortest;
            if (gradientAccumulation >= longest)
            {
                if (inverted)
                {
                    x += gradientStep;
                }
                else
                {
                    y += gradientStep;
                }

                gradientAccumulation -= longest;
            }
        }

        return line;
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

    private void PlaceFloorTiles()
    {
        levelHolder = new GameObject("Level").transform;
        for (int i = -10; i < map.GetLength(0) + 10; i++)
        {
            for(int j = -10; j < map.GetLength(1) + 10; j++)
            {
                GameObject toInstantiate = new GameObject();
                if(i < 0 || i >= map.GetLength(0) || j < 0 || j >= map.GetLength(1))
                {
                    toInstantiate = background;
                }
                else if(map[i, j] == 0)
                {
                    int randVal = rand.Next(0, 100);
                    if(randVal < 50)
                    {
                        toInstantiate = floorTiles[0];
                    }
                    else if(randVal < 90)
                    {
                        toInstantiate = floorTiles[1];
                    }
                    else if(randVal < 95)
                    {
                        toInstantiate = floorTiles[2];
                    }
                    else
                    {
                        toInstantiate = floorTiles[3];
                    }
                }
                else if(map[i, j] == 1)
                {
                    int wallType = GetWallType(i, j);

                    switch(wallType)
                    {
                        case 0:
                            toInstantiate = background;
                            break;
                        case 1:
                            toInstantiate = botMidWallTiles[UnityEngine.Random.Range(0, botMidWallTiles.Length)];
                            break;
                        case 2:
                            toInstantiate = vertWallTiles[UnityEngine.Random.Range(0, vertWallTiles.Length)];
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
                            toInstantiate = floorTiles[0];
                            break;
                    }
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(levelHolder);
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
        if ((i == map.GetLength(0) - 1 && j == map.GetLength(1) - 1) 
            || (i > 0 && j > 0) && (map[i - 1, j] == 1 && map[i, j - 1] == 1 
            && ((map[i - 1, j - 1] == 0) || (i < map.GetLength(0) - 1 && map[i + 1, j] == 0 && map[i, j + 1] == 0))))
        {
            return 3;
        }
        // Top Left Corner
        if ((i == 0 && (j == map.GetLength(1) - 1) 
            || (i < map.GetLength(0) - 1 && j > 0 && map[i + 1, j] == 1 && map[i, j - 1] == 1 && (map[i + 1, j - 1] == 0))) 
            || (i > 1 && j > 0 && i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1) 
            && (map[i + 1, j] == 1 && map[i, j - 1] == 1 
            && ((map[i + 1, j - 1] == 0) || (map[i - 1, j] == 0 && map[i, j + 1] == 0 ))))
        {
            return 4;
        }
        // Bottom Right Corner
        if ((i == map.GetLength(0) - 1 && j == 0) 
            || i > 0 && j < map.GetLength(1) - 1 && (map[i - 1, j] == 1 && map[i, j + 1] == 1 
            && (( map[i - 1, j + 1] == 0) 
            || (i < map.GetLength(0) - 1 && j > 0 && map[i + 1, j] == 0 && map[i, j - 1] == 0))))
        {
            return 5;
        }
        // Bottom Left Corner
        if ((i == 0 && j == 0) || (i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1)
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
        if (((i == 0 || i == map.GetLength(0) - 1) && j > 0 && j < map.GetLength(1) - 1)
            || (i > 1 && j > 1 && i < map.GetLength(0) - 1 && j < map.GetLength(1) - 1)
            && map[i, j + 1] == 1 && map[i, j - 1] == 1 && (i == 0 || i == map.GetLength(1) - 1 || map[i - 1, j] == 0 || map[i + 1, j] == 0))
        {
            return 2;
        }

        return 0;
    }

    private void AddPlayerAndCreatures()
    {
        Room mainRoom = rooms[rooms.Count - 1];
        playerLoc = mainRoom.tiles[rand.Next(0, mainRoom.tiles.Count)];
        map[playerLoc.tileX, playerLoc.tileY] = 2;
        manager.movingObjects[playerLoc.tileX, playerLoc.tileY] = true;
        player.transform.position = new Vector3(playerLoc.tileX, playerLoc.tileY, 0f);


        GameObject instance;

        for(int i = 0; i < rooms.Count - 1; i++)
        {
            for(int j = 0; j < (rooms[i].roomSize / rand.Next(15, 25)); j++ )
            {
                Coord tilePlacement = rooms[i].tiles[rand.Next(0, rooms[i].tiles.Count)];
                if (map[tilePlacement.tileX, tilePlacement.tileY] > 1 || playerLoc.Distance(tilePlacement) < 10)
                    continue;
                instance = EnemyFactory.GetRandomObject(new Vector3(tilePlacement.tileX, tilePlacement.tileY, 0f), Quaternion.identity);
                instance.transform.SetParent(levelHolder);
                map[tilePlacement.tileX, tilePlacement.tileY] = 3;
                manager.movingObjects[tilePlacement.tileX, tilePlacement.tileY] = true;
            }

        }
    }

    private void AddExit()
    {
        while(true)
        {
            Room room = rooms[rand.Next(0, rooms.Count - 1)];
            Coord tilePlacement = room.tiles[rand.Next(0, room.tiles.Count)];
            if (map[tilePlacement.tileX, tilePlacement.tileY] > 1 || tilePlacement.Distance(playerLoc) < 25)
                continue;
            GameObject instance = Instantiate(exit, new Vector3(tilePlacement.tileX, tilePlacement.tileY, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(levelHolder);
            break;
        }
    }

    private void AddItems()
    {
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            for (int j = 0; j < (rooms[i].roomSize / rand.Next(30, 35)); j++)
            {
                Coord tilePlacement = rooms[i].tiles[rand.Next(0, rooms[i].tiles.Count)];
                if (map[tilePlacement.tileX, tilePlacement.tileY] > 1)
                    continue;
                GameObject instance = Instantiate(potions[rand.Next(0, potions.Length)], new Vector3(tilePlacement.tileX, tilePlacement.tileY, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(levelHolder);
                map[tilePlacement.tileX, tilePlacement.tileY] = 4;
            }

        }

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            for (int j = 0; j < (rooms[i].roomSize / rand.Next(40, 45)); j++)
            {
                Coord tilePlacement = rooms[i].tiles[rand.Next(0, rooms[i].tiles.Count)];
                if (map[tilePlacement.tileX, tilePlacement.tileY] > 1)
                    continue;
                GameObject instance = Instantiate(powerUps[rand.Next(0, powerUps.Length)], new Vector3(tilePlacement.tileX, tilePlacement.tileY, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(levelHolder);
                map[tilePlacement.tileX, tilePlacement.tileY] = 4;
            }

        }
    }

    public void GameOver()
    {
        foreach (Transform child in levelHolder)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

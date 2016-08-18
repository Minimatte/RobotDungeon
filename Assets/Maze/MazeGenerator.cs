using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MazeGenerator : MonoBehaviour {

    public Room[,] map;

    public Vector2 mapSize;

    internal int mapSizeX { get { return (int)mapSize.x; } }
    internal int mapSizeY { get { return (int)mapSize.y; } }

    public GameObject wall, floor, starter;

    void Start() {
        map = CreateMap(mapSizeX, mapSizeY);
        StartCoroutine(GenerateMaze());


    }

    Room[,] CreateMap(int xSize, int ySize) {
        Room[,] tempMap = new Room[xSize, ySize];
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                tempMap[x, y] = new Room(x, y, true); // position and its a wall
            }
        }
        return tempMap;
    }


    void SpawnMap(int x, int y) {
        if (map[x, y].isWall)
            Instantiate(wall, new Vector3(x, 0, y), Quaternion.identity);
        else
            Instantiate(floor, new Vector3(x, 0, y), Quaternion.identity);
    }

    void SpawnMap(int x, int y, GameObject go) {

        Instantiate(go, new Vector3(x, 0, y), Quaternion.identity);
    }

    IEnumerator GenerateMaze() {

        List<Room> walls = new List<Room>();
        int randomX = Random.Range(0, mapSizeX); // first one
        int randomY = Random.Range(0, mapSizeY);

        map[randomX, randomY].isWall = false;

        foreach (Room r in GetNeighbours(randomX, randomY)) {
            if (r.isWall)
                walls.Add(r);
        }

        SpawnMap(randomX, randomY, starter);
        while (walls.Count > 0) {
            int randomIndex = Random.Range(0, walls.Count);
            Room wall = walls[randomIndex];

            int unvisitedCount = 0;
            foreach (Room r in GetNeighbours(wall.x, wall.y)) {
                if (!r.isWall)
                    unvisitedCount++;
            }
            print(unvisitedCount);
            if (unvisitedCount == 1) {
                walls[randomIndex].isWall = false;

                foreach (Room r in GetNeighbours(walls[randomIndex].x, walls[randomIndex].y)) {
                    if (r.isWall)
                        walls.Add(r);

                }
            }
            SpawnMap(walls[randomIndex].x, walls[randomIndex].y);
            walls.Remove(walls[randomIndex]);
            yield return new WaitForEndOfFrame();
        }

        /*for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                if (x == 0 || y == 0 || x == mapSizeX - 1 || y == mapSizeY - 1) {

                    map[x, y].isWall = true;
                    SpawnMap(x, y);
                }
            }
        }*/

    }

    List<Room> GetNeighbours(int x, int y) {
        List<Room> neighbours = new List<Room>();

        if (x > 0)
            neighbours.Add(map[x - 1, y]);
        if (x < mapSizeX - 1)
            neighbours.Add(map[x + 1, y]);
        if (y > 0)
            neighbours.Add(map[x, y - 1]);
        if (y < mapSizeY - 1)
            neighbours.Add(map[x, y + 1]);

        return neighbours;
    }


}

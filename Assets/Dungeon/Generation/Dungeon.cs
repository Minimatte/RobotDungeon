using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dungeon : MonoBehaviour {
    public GameObject player;

    public DungeonRoom[,] map;

    public int mapSizeX = 10, mapSizeY = 10;

    public int roomSizeX = 5, roomSizeY = 5;

    public GameObject[] dungeonRoomInstance;
    public GameObject startRoomObject;
    public GameObject exitRoomObject;

    public DungeonRoom startRoom;

    List<DungeonRoom> roomProgress = new List<DungeonRoom>();

    void Start() {

        GenerateDungeon();
        SpawnMap();
        GameEvents.RespawnPlayer(startRoom.x, startRoom.y);
    }

    void SpawnMap() {
        foreach (DungeonRoom room in map) {
            GameObject go;
            if (room == roomProgress[roomProgress.Count - 1]) {//if its the last room
                go = (GameObject)Instantiate(exitRoomObject, new Vector3(room.x * roomSizeX, 0, room.y * roomSizeY), Quaternion.identity);
            } else if (room == startRoom) { // if its the first room
                go = (GameObject)Instantiate(startRoomObject, new Vector3(room.x * roomSizeX, 0, room.y * roomSizeY), Quaternion.identity);
            } else {//randomize other rooms
                go = (GameObject)Instantiate(dungeonRoomInstance[Random.Range(0, dungeonRoomInstance.Length)], new Vector3(room.x * roomSizeX, 0, room.y * roomSizeY), Quaternion.identity);
            }
            go.GetComponent<GameDungeonRoom>().room = room;
            room.worldInstance = go;
        }

    }

    void GenerateDungeon() {
        map = new DungeonRoom[mapSizeX, mapSizeY];

        int randomX = Random.Range(0, mapSizeX);
        int randomY = Random.Range(0, mapSizeY);

        DungeonRoom room = new DungeonRoom(null, randomX, randomY, this);
        map[randomX, randomY] = room;
        room.GenerateChildren();
        startRoom = room;
    }

    public DungeonRoom AddRoom(DungeonRoom parent, int x, int y, Dungeon dungeon) {
        DungeonRoom room = new DungeonRoom(parent, x, y, dungeon);
        map[x, y] = room;
        roomProgress.Add(room);
        return room;
    }
}

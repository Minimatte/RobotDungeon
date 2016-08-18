using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;
using System;

public class GameEvents : MonoBehaviour {

    public static int score = 0;

    public GameObject playerGameObject;
    private static GameObject player;
    public Dungeon dungeonObject;
    private static Dungeon dungeon;

    public static GameObject playerInstance;
    private static GameEvents instance;

    void Awake() {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        dungeon = dungeonObject;
        player = playerGameObject;

        if (dungeonObject == null)
            RespawnPlayer(0, 0);
    }

    internal static void AddScore(int scoreToAdd) {
        score += scoreToAdd;
        UIManager.SetScore();
    }

    public static void RespawnPlayer(int x, int y) {
        GameObject pg;
        if (dungeon != null) {
            pg = (GameObject)Instantiate(player, new Vector3(dungeon.startRoom.x * dungeon.roomSizeX, 0.5f, dungeon.startRoom.y * dungeon.roomSizeY), Quaternion.identity);
        } else {
            pg = (GameObject)Instantiate(player, new Vector3(x, 0.5f, y), Quaternion.identity);
        }
        playerInstance = pg;
        Camera.main.GetComponent<CameraMovement>().player = pg;
        Camera.main.transform.position = pg.transform.position;
    }

    public void RespawnPlayerFromButton() {
        GameObject pg;
        if (dungeon != null) {
            pg = (GameObject)Instantiate(player, new Vector3(dungeon.startRoom.x * dungeon.roomSizeX, 0.5f, dungeon.startRoom.y * dungeon.roomSizeY), Quaternion.identity);
        } else {
            pg = (GameObject)Instantiate(player, new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        playerInstance = pg;
        Camera.main.GetComponent<CameraMovement>().player = pg;
        Camera.main.transform.position = pg.transform.position;
    }

    public static void NextLevel() {
        Application.LoadLevel(Application.loadedLevel);

    }
}

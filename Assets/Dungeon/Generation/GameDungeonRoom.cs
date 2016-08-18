using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDungeonRoom : MonoBehaviour {

    public DungeonRoom room;

    public GameObject right, left, up, down;

    internal GameDungeonRoom parent;

    internal bool active = false;
    public List<GameObject> killThese = new List<GameObject>();
    bool roomDone = false;
    void Update() {
        if (killThese.Count == 0 && !roomDone) {
            OpenRoom();
            if (room.parent != null) {
                room.parent.worldInstance.GetComponent<GameDungeonRoom>().OpenRoom();
                if (room.child1 != null)
                    room.child1.worldInstance.GetComponent<GameDungeonRoom>().OpenRoom();
                if (room.child2 != null)
                    room.child2.worldInstance.GetComponent<GameDungeonRoom>().OpenRoom();
            } else {
                if (room.child1 != null)
                    room.child1.worldInstance.GetComponent<GameDungeonRoom>().OpenRoom();
                if (room.child2 != null)
                    room.child2.worldInstance.GetComponent<GameDungeonRoom>().OpenRoom();
            }
            roomDone = true;
        }

    }


    public void OpenRoom() {
        active = true;

        if (room.IsConnectedTo(room.GetRight())) {

            right.SetActive(false);
        }

        if (room.IsConnectedTo(room.GetLeft())) {

            left.SetActive(false);
        }

        if (room.IsConnectedTo(room.GetUp())) {

            up.SetActive(false);
        }

        if (room.IsConnectedTo(room.GetBottom())) {

            down.SetActive(false);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Room {


    public int x, y;
    public bool isWall = true;

    public Room(int x, int y, bool isWall) {
        this.x = x;
        this.y = y;
        this.isWall = isWall;
    }

}

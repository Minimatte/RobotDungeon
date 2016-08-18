using UnityEngine;
using System.Collections;

public class DungeonRoom {

    public int x, y;

    public GameObject worldInstance;

    Dungeon dungeon;

    public DungeonRoom parent;
    public DungeonRoom child1, child2;


    public DungeonRoom(DungeonRoom parent, int x, int y, Dungeon dungeon) {
        this.parent = parent;
        this.x = x;
        this.y = y;
        this.dungeon = dungeon;
    }

    public int GetNumberOfChildren() {
        int count = 0;
        if (child1 != null)
            count++;
        if (child2 != null)
            count++;
        return count;
    }

    public void GenerateChildren() {
        if (GetNumberOfChildren() == 2)
            return;
        if (getNumberOfNearbyRooms() == 4)
            return;

        if (child1 == null) {
            int temp = GetValidDirection(1);
            if (temp != -1) {
                child1 = AddChild(temp);
                child1.GenerateChildren();
            }
        }
        if (child2 == null) {
            int temp = GetValidDirection(1);
            if (temp != -1) {
                child2 = AddChild(temp);
                child2.GenerateChildren();
            }
        }
    }

    public bool IsConnectedTo(DungeonRoom room) {
        if (room == null)
            return false;
        if (room.parent == this)
            return true;
        if (room == this.parent)
            return true;

        return false;
    }


    public DungeonRoom AddChild(int direction) {
        if (direction == 0)
            return dungeon.AddRoom(this, x - 1, y, dungeon); // Left
        if (direction == 1)
            return dungeon.AddRoom(this, x + 1, y, dungeon); // Right
        if (direction == 2)
            return dungeon.AddRoom(this, x, y + 1, dungeon); // Top
        if (direction == 3)
            return dungeon.AddRoom(this, x, y - 1, dungeon); // Bottom
        return null;
    }


    public int getNumberOfNearbyRooms() {
        int count = 0;

        if (GetRight() != null)
            count++;
        if (GetLeft() != null)
            count++;
        if (GetUp() != null)
            count++;
        if (GetBottom() != null)
            count++;

        return count;
    }

    public int GetValidDirection(int num_tries) {
        if (num_tries > 8)
            return -1;

        int direction = Random.Range(0, 4);
        if (direction == 0) // Left
        {
            if (x == 0)
                return GetValidDirection(num_tries + 1);
            if (GetLeft() != null)
                return GetValidDirection(num_tries + 1);
        } else if (direction == 1) // Right
          {
            if (x >= dungeon.mapSizeX - 1)
                return GetValidDirection(num_tries + 1);
            if (GetRight() != null)
                return GetValidDirection(num_tries + 1);
        } else if (direction == 2) // Top
          {
            if (y >= dungeon.mapSizeY - 1)
                return GetValidDirection(num_tries + 1);
            if (GetUp() != null)
                return GetValidDirection(num_tries + 1);
        } else if (direction == 3) // Bottom
          {
            if (y == 0)
                return GetValidDirection(num_tries++);
            if (GetBottom() != null)
                return GetValidDirection(num_tries + 1);
        }
        return direction;
    }




    public DungeonRoom GetRight() {
        int tileX = x + 1;
        if (tileX >= dungeon.mapSizeX)
            return null;

        int tileY = y;
        return dungeon.map[tileX, tileY];
    }
    public DungeonRoom GetLeft() {
        int tileX = x - 1;
        if (tileX < 0)
            return null;

        int tileY = y;
        return dungeon.map[tileX, tileY];
    }
    public DungeonRoom GetUp() {
        int tileY = y + 1;
        if (tileY >= dungeon.mapSizeY)
            return null;

        int tileX = x;
        return dungeon.map[tileX, tileY];
    }

    public DungeonRoom GetBottom() {
        int tileY = y - 1;
        if (tileY < 0)
            return null;

        int tileX = x;
        return dungeon.map[tileX, tileY];
    }


    public override string ToString() {
        return x + ":" + y;
    }
}

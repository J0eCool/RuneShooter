using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {
	public bool doorToEast = false;
	public bool doorToNorth = false;
	public bool isStartRoom = false;

	public List<Room> neighbors = new List<Room>();
	public List<GameObject> associatedWalls = new List<GameObject>();

	public void AddNeighbor(Room room) {
		if (!neighbors.Contains(room)) {
			neighbors.Add(room);
			room.AddNeighbor(this);
		}
	}
}

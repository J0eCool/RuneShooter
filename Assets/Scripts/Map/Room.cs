using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {
	public bool doorToEast = false;
	public bool doorToNorth = false;
	public bool isStartRoom = false;

	public List<GameObject> associatedWalls = new List<GameObject>();
}

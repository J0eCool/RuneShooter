using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Room {
	public bool doorToEast = false;
	public bool doorToNorth = false;
}

public class MapGenerator : JComponent {
	[SerializeField] private GameObject roomPrefab;
	[SerializeField] private GameObject wallPrefab;
	[SerializeField] private float roomWidth = 14.333f;
	[SerializeField] private float roomHeight = 10.0f;

	protected override void OnStart() {
		int width = 1;
		int height = 2;
		Room[,] rooms = new Room[width, height];
		for (int i = 0; i < width; ++i) {
			for (int j = 0; j < height; ++j) {
				rooms[i, j] = new Room();
				GameObject room = Instantiate(roomPrefab);
				room.transform.position = positionForRoom(i, j);
			}
		}
		rooms[0, 0].doorToNorth = true;

		for (int i = 0; i < width; ++i) {
			for (int j = -1; j < height; ++j) {
				Room cur = null;
				Room next = null;
				if (j >= 0) {
					cur = rooms[i, j];
				}
				if (j + 1 < height) {
					next = rooms[i, j + 1];
				}

				bool shouldSpawnWall = (cur != null) || (next != null);
				if (shouldSpawnWall) {
					bool wallHasDoor = (cur != null) && cur.doorToNorth;
					GameObject wall = Instantiate(wallPrefab);
					wall.transform.position = positionForRoom(i, j + 0.5f);
					RandomlySizedDoor door = wall.GetComponent<RandomlySizedDoor>();
					door.Init(totalWallSize: roomWidth, isVertical: false, isDoor: wallHasDoor);
				}
			}
		}

		for (int j = 0; j < height; ++j) {
			for (int i = -1; i < width; ++i) {
				Room cur = null;
				Room next = null;
				if (i >= 0) {
					cur = rooms[i, j];
				}
				if (i + 1 < width) {
					next = rooms[i + 1, j];
				}

				bool shouldSpawnWall = (cur != null) || (next != null);
				if (shouldSpawnWall) {
					bool wallHasDoor = (cur != null) && cur.doorToEast;
					GameObject wall = Instantiate(wallPrefab);
					wall.transform.position = positionForRoom(i + 0.5f, j);
					RandomlySizedDoor door = wall.GetComponent<RandomlySizedDoor>();
					door.Init(totalWallSize: roomHeight, isVertical: true, isDoor: wallHasDoor);
				}
			}
		}
	}

	private Vector3 positionForRoom(float i, float j) {
		return transform.position + new Vector3(i * roomWidth, j * roomHeight);
	}
}

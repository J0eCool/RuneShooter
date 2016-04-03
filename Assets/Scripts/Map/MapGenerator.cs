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

	private int width;
	private int height;
	private Room[,] rooms;
	private Vector3 startOffset;

	protected override void OnStart() {
		generateRooms();
		spawnRooms();
		spawnWalls();
	}

	private void generateRooms() {
		width = Random.Range(6, 8);
		height = Random.Range(4, 6);
		rooms = new Room[width, height];

		int x = Random.Range(0, width);
		int y = Random.Range(0, height);
		rooms[x, y] = new Room();
		startOffset = transform.position - offsetForRoom(x, y);

		int count = Random.Range(1, width) + Random.Range(1, height) + width * height / 4;
		int numRooms = 1;
		while (numRooms < count) {
			// Set up next move
			bool horizontal = RandomUtil.Bool();
			int sign = RandomUtil.Sign();
			int dx = horizontal ? sign : 0;
			int dy = horizontal ? 0 : sign;
			int newX = x + dx;
			int newY = y + dy;
			if (newX < 0 || newX >= width || newY < 0 || newY >= height) {
				continue;
			}

			// Create new room if unvisited
			bool shouldConnect = RandomUtil.Bool();
			if (rooms[newX, newY] == null) {
				rooms[newX, newY] = new Room();
				numRooms++;
				shouldConnect = true;
			}

			// Connect to previous room
			if (shouldConnect) {
				int prevX = Mathf.Min(x, x + dx);
				int prevY = Mathf.Min(y, y + dy);
				Room prev = rooms[prevX, prevY];
				if (horizontal) {
					prev.doorToEast = true;
				} else {
					prev.doorToNorth = true;
				}
			}

			// Perform move
			x = newX;
			y = newY;
		}
	}
	private void spawnRooms() {
		Transform roomFolder = new GameObject("Rooms").transform;
		roomFolder.parent = transform;
		for (int i = 0; i < width; ++i) {
			for (int j = 0; j < height; ++j) {
				if (rooms[i, j] != null) {
					GameObject room = Instantiate(roomPrefab);
					room.name = string.Format("Room ({0}, {1})", i, j);
					room.transform.position = positionForRoom(i, j);
					room.transform.parent = roomFolder;
				}
			}
		}
	}

	private void spawnWalls() {
		Transform wallFolder = new GameObject("Walls").transform;
		wallFolder.parent = transform;
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
					wall.transform.parent = wallFolder;
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
					wall.transform.parent = wallFolder;
					RandomlySizedDoor door = wall.GetComponent<RandomlySizedDoor>();
					door.Init(totalWallSize: roomHeight, isVertical: true, isDoor: wallHasDoor);
				}
			}
		}
	}

	private Vector3 positionForRoom(float i, float j) {
		return startOffset + offsetForRoom(i, j);
	}

	private Vector3 offsetForRoom(float i, float j) {
		return new Vector3(i * roomWidth, j * roomHeight);
	}
}

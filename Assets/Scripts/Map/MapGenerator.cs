using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : JComponent {
	[SerializeField] private string startRoomName = "Start";
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
		// Generate room layout via random walk
		width = Random.Range(6, 8);
		height = Random.Range(4, 6);
		rooms = new Room[width, height];

		int x = Random.Range(0, width);
		int y = Random.Range(0, height);
		Room startRoom = new Room();
		rooms[x, y] = startRoom;
		startRoom.isStartRoom = true;
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
				int curX = Mathf.Max(x, x + dx);
				int curY = Mathf.Max(y, y + dy);
				Room cur = rooms[curX, curY];
				prev.AddNeighbor(cur);
			}

			// Perform move
			x = newX;
			y = newY;
		}
	}
	private void spawnRooms() {
		Transform roomFolder = new GameObject("Rooms").transform;
		roomFolder.parent = transform;
		var roomPrefabs = new List<Object>(Resources.LoadAll("Rooms"));
		GameObject startPrefab = roomPrefabWithName(startRoomName, roomPrefabs);
		roomPrefabs.Remove(startPrefab);

		for (int i = 0; i < width; ++i) {
			for (int j = 0; j < height; ++j) {
				Room room = rooms[i, j];
				if (room != null) {
					GameObject prefab;
					if (room.isStartRoom) {
						prefab = startPrefab;
					} else {
						prefab = RandomUtil.Element(roomPrefabs) as GameObject;
					}

					GameObject roomObject = Instantiate(prefab);
					roomObject.name = string.Format("({1}, {2}) {0}", prefab.name, i, j);
					roomObject.transform.position = positionForRoom(i, j);
					roomObject.transform.parent = roomFolder;

					var roomAware = roomObject.GetComponentsInChildren<RoomAwareComponent>();
					foreach (RoomAwareComponent aware in roomAware) {
						aware.SetRoom(room);
					}
				}
			}
		}
	}

	private GameObject roomPrefabWithName(string name, List<Object> prefabs) {
		foreach (GameObject obj in prefabs) {
			if (obj.name == name) {
				return obj;
			}
		}
		return null;
	}

	private void spawnWalls() {
		Transform wallFolder = new GameObject("Walls").transform;
		wallFolder.parent = transform;

		generateWalls(wallFolder,
			nextRoom: (i, j) => inBounds(i, j + 1) ? rooms[i, j + 1] : null,
			roomHasDoor: room => room.doorToNorth,
			wallPosition: (i, j) => positionForRoom(i, j + 0.5f),
			wallInit: (door, hasDoor) =>
				door.Init(totalWallSize: roomWidth, isVertical: false, isDoor: hasDoor));
		generateWalls(wallFolder,
			nextRoom: (i, j) => inBounds(i + 1, j) ? rooms[i + 1, j] : null,
			roomHasDoor: room => room.doorToEast,
			wallPosition: (i, j) => positionForRoom(i + 0.5f, j),
			wallInit: (door, hasDoor) =>
				door.Init(totalWallSize: roomHeight, isVertical: true, isDoor: hasDoor));
	}

	delegate Room RoomByIndex(int i, int j);
	delegate bool RoomProperty(Room room);
	delegate Vector3 PositionByIndex(int i, int j);
	delegate void WallInitializer(RandomlySizedDoor door, bool hasDoor);

	private void generateWalls(
			Transform wallFolder,
			RoomByIndex nextRoom,
			RoomProperty roomHasDoor,
			PositionByIndex wallPosition,
			WallInitializer wallInit) {
		for (int i = -1; i < width; ++i) {
			for (int j = -1; j < height; ++j) {
				Room cur = inBounds(i, j) ? rooms[i, j] : null;
				Room next = nextRoom(i, j);

				bool shouldSpawnWall = (cur != null) || (next != null);
				if (shouldSpawnWall) {
					bool wallHasDoor = (cur != null) && roomHasDoor(cur);
					GameObject wall = Instantiate(wallPrefab);
					wall.transform.position = wallPosition(i, j);
					wall.transform.parent = wallFolder;

					RandomlySizedDoor door = wall.GetComponent<RandomlySizedDoor>();
					wallInit(door, wallHasDoor);

					if (cur != null) {
						cur.associatedWalls.Add(wall);
					}
					if (next != null) {
						next.associatedWalls.Add(wall);
					}
				}
			}
		}
	}

	private bool inBounds(int i, int j) {
		return i >= 0 && i < width && j >= 0 && j < height;
	}

	private Vector3 positionForRoom(float i, float j) {
		return startOffset + offsetForRoom(i, j);
	}

	private Vector3 offsetForRoom(float i, float j) {
		return new Vector3(i * roomWidth, j * roomHeight);
	}
}

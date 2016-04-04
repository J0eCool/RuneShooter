using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnPoint : RoomAwareComponent, RoomChangeResponder {
	[SerializeField] private GameObject enemyPrefab;

	private bool didSpawn = false;
	private GameObject spawnedEnemy = null;

	protected override void OnStart() {
		RoomManager.Instance.Subscribe(this);
	}

	public void DidSetActiveRoom(Room activeRoom) {
		bool movedToCurrentRoom = activeRoom == room;
		if (movedToCurrentRoom && !didSpawn) {
			spawnEnemy();
		} else if (!movedToCurrentRoom && didSpawn && spawnedEnemy != null) {
			/* If spawned enemy was destroyed by anything else (e.g. it died),
			 * intentionally don't respawn it */
			despawnEnemy();
		}
	}

	private void spawnEnemy() {
		spawnedEnemy = Instantiate(enemyPrefab);
		spawnedEnemy.transform.position = transform.position;
		didSpawn = true;
	}

	private void despawnEnemy() {
		Remove(spawnedEnemy);
		didSpawn = false;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnPoint : JComponent {
	[SerializeField] private GameObject enemyPrefab;

	private GameObject spawnedEnemy = null;

	protected override void OnStart() {
		spawnEnemy();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 0.4f);
	}

	private void spawnEnemy() {
		spawnedEnemy = Instantiate(enemyPrefab);
		spawnedEnemy.transform.parent = transform.parent;
		spawnedEnemy.transform.position = transform.position;
	}
}

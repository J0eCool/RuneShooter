using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomlySizedDoor : JComponent {
	[SerializeField] private GameObject wallPrefab;
	[SerializeField] private float totalWallSize;
	[SerializeField] private bool isVertical = false;
	[SerializeField] private bool isDoor = true;
	[SerializeField] private float minWallSize = 1.0f;
	[SerializeField] private float minDoorSize = 2.0f;
	[SerializeField] private float maxDoorSize = 3.5f;

	protected override void OnStart() {
		float doorWidth = normalizedRandom(minDoorSize, maxDoorSize);
		float halfTotalSize = totalWallSize / 2.0f;
		float halfDoorSize = doorWidth / 2.0f;
		float centerBound = halfTotalSize - minWallSize - halfDoorSize;
		float doorCenter = normalizedRandom(-centerBound, centerBound);

		float doorLeft = doorCenter - halfDoorSize;
		float leftSize = halfTotalSize + doorLeft;
		float leftPos = leftSize / 2.0f - halfTotalSize;
		spawnWall(leftPos, leftSize);

		float doorRight = doorCenter + halfDoorSize;
		float rightSize = halfTotalSize - doorRight;
		float rightPos = halfTotalSize - rightSize / 2.0f;
		spawnWall(rightPos, rightSize);
	}

	private float normalizedRandom(float lo, float hi) {
		float diff = hi - lo;
		float randDelta = Random.Range(0.0f, diff) / 2.0f;
		return Random.Range(lo + randDelta, hi - randDelta);
	}

	private void spawnWall(float offset, float size) {
		GameObject wall = GameObject.Instantiate(wallPrefab);
		wall.transform.parent = transform;

		Vector3 scale = wall.transform.localScale;
		scale.x = size;
		wall.transform.localScale = scale;

		Vector3 pos = transform.position;
		pos.x += offset;
		wall.transform.position = pos;
	}
}

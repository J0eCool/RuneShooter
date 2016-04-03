using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomlySizedDoor : JComponent {
	[SerializeField] private GameObject wallPrefab;
	[SerializeField] private float minWallSize = 1.0f;
	[SerializeField] private float minDoorSize = 2.0f;
	[SerializeField] private float maxDoorSize = 3.5f;

	public void Init(float totalWallSize, bool isVertical, bool isDoor) {
		if (isVertical) {
			// Reduce vertical wall size to make room borders fit together more snugly
			totalWallSize -= 1.0f;
		}
		if (!isDoor) {
			spawnWall(isVertical, 0.0f, totalWallSize);
			return;
		}

		float doorWidth = RandomUtil.Normalized(minDoorSize, maxDoorSize);
		float halfTotalSize = totalWallSize / 2.0f;
		float halfDoorSize = doorWidth / 2.0f;
		float centerBound = halfTotalSize - minWallSize - halfDoorSize;
		float doorCenter = RandomUtil.Normalized(-centerBound, centerBound);

		float doorLeft = doorCenter - halfDoorSize;
		float leftSize = halfTotalSize + doorLeft;
		float leftPos = leftSize / 2.0f - halfTotalSize;
		spawnWall(isVertical, leftPos, leftSize);

		float doorRight = doorCenter + halfDoorSize;
		float rightSize = halfTotalSize - doorRight;
		float rightPos = halfTotalSize - rightSize / 2.0f;
		spawnWall(isVertical, rightPos, rightSize);
	}

	private void spawnWall(bool isVertical, float offset, float size) {
		GameObject wall = Instantiate(wallPrefab);
		wall.transform.parent = transform;

		Vector3 scale = wall.transform.localScale;
		Vector3 pos = transform.position;
		if (isVertical) {
			scale.y = size;
			pos.y += offset;
		} else {
			scale.x = size;
			pos.x += offset;
		}
		wall.transform.localScale = scale;
		wall.transform.position = pos;

	}
}

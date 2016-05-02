using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : JComponent { }

public class EnemyActiveWithinRange : JComponent {
	[SerializeField] private float range = 10.0f;

	[StartComponent] private EnemyMovement movement;
	[StartComponent] new private Rigidbody2D rigidbody;
	private Transform player;

	protected override void OnStart() {
		player = PlayerManager.Instance.Player.transform;
	}

	protected override void OnUpdate(float dT) {
		Vector3 playerPos = player.position;
		Vector3 delta = transform.position - playerPos;
		bool inRange = delta.sqrMagnitude < range * range;
		movement.enabled = inRange;
		if (!inRange) {
			rigidbody.velocity = Vector2.zero;
		}
	}
}

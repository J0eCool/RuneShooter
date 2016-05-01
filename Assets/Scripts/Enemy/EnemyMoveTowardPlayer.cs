using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMoveTowardPlayer : EnemyMovement {
	[SerializeField] private float speed = 1.5f;
	[SerializeField] private float range = 2.0f;

	[StartComponent] new private Rigidbody2D rigidbody;
	private Transform player;

	protected override void OnStart() {
		player = PlayerManager.Instance.Player.transform;
	}

	protected override void OnFixedUpdate() {
		Vector2 delta = player.position - transform.position;
		Vector2 vel = Vector2.zero;
		if (delta.sqrMagnitude > range * range) {
			Vector2 dir = delta.normalized;
			vel = dir * speed;
		}
		rigidbody.velocity = vel;
	}
}

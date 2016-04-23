using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : JComponent {
	[SerializeField] private float moveSpeed = 1.0f;
	[SerializeField] private float accelTime = 0.2f;
	[SerializeField] private float dampingFactor = 2.0f;
	[SerializeField] private float decelerateRange = 0.5f;

	[StartComponent] new private Rigidbody2D rigidbody;
	[StartChildComponent] private PlayerShooter shooter;

	private Vector3 moveTarget;
	private Transform shotTarget;

	protected override void OnStart() {
		moveTarget = transform.position;
	}

	protected override void OnFixedUpdate() {
		float dT = Time.fixedDeltaTime;

		if (Input.GetButton("Click")) {
			Vector3 mousePos = MouseManager.Instance.WorldPos;
			int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
			var hit = Physics2D.Raycast(mousePos, Vector2.zero, 0.01f, enemyLayer);
			if (hit) {
				shotTarget = hit.transform;
				shooter.SetTarget(hit.transform);
			} else {
				moveTarget = mousePos;
			}
		}

		Vector3 delta = moveTarget - transform.position;
		delta.z = 0.0f;
		Vector3 dir = delta.normalized;
		if (delta.sqrMagnitude < decelerateRange * decelerateRange) {
			dir = Vector3.zero;
		}

		rigidbody.velocity = dir * moveSpeed;
		Vector3 lookAtPos = shotTarget != null ? shotTarget.position : (Vector3)MouseManager.Instance.WorldPos;
		MathUtil.LookAt2D(transform, lookAtPos);
	}

	float updateDirVel(float cur, float input, float dT) {
		float accel = moveSpeed / accelTime;
		bool isMoving = Mathf.Abs(cur) > 0.0f;
		bool isHeld = Mathf.Abs(input) > 0.0f;
		float updated = cur;
		if (!isHeld) {
			if (isMoving) {
				updated -= dT * dampingFactor * accel * Mathf.Sign(cur);
				if (updated * cur < 0.0f) {
					updated = 0.0f;
				}
			}
		} else {
			float delta = input;
			if (input * cur < 0.0f) {
				delta = MathUtil.AbsMax(input, -dampingFactor * Mathf.Sign(cur));
			}
			updated += dT * accel * delta;
		}
		return Mathf.Clamp(updated, -moveSpeed, moveSpeed);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : JComponent {
	[SerializeField] private float moveSpeed = 1.0f;
	[SerializeField] private float accelTime = 0.2f;
	[SerializeField] private float dampingFactor = 2.0f;
	[SerializeField] private float decelerateRange = 0.5f;

	new private Rigidbody2D rigidbody;

	private Vector3 moveTarget;

	protected override void OnStart() {
		rigidbody = GetComponent<Rigidbody2D>();
		moveTarget = transform.position;
	}

	protected override void OnFixedUpdate() {
		float dT = Time.fixedDeltaTime;

		if (Input.GetButton("Click")) {
			moveTarget = MouseManager.Instance.WorldPos;
		}

		Vector3 delta = moveTarget - transform.position;
		delta.z = 0.0f;
		Vector3 dir = delta.normalized;
		if (delta.sqrMagnitude < decelerateRange * decelerateRange) {
			dir = Vector3.zero;
		}

		rigidbody.velocity = dir * moveSpeed;
		MathUtil.LookAt2D(transform, MouseManager.Instance.WorldPos);
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : JComponent {
	[SerializeField] private float moveSpeed = 1.0f;
	[SerializeField] private float accelTime = 0.2f;
	[SerializeField] private float dampingFactor = 2.0f;

	private Vector3 velocity;

	protected override void OnFixedUpdate() {
		float dT = Time.fixedDeltaTime;
		float dX = Input.GetAxis("Horizontal");
		float dY = Input.GetAxis("Vertical");
		float vX = updateDirVel(velocity.x, dX, dT);
		float vY = updateDirVel(velocity.y, dY, dT);

		velocity = new Vector3(vX, vY);
		transform.position += dT * velocity;
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

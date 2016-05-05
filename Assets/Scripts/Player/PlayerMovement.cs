using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : JComponent, MouseClickResponder {
	[SerializeField] private float moveSpeed = 1.0f;
	[SerializeField] private float decelerateRange = 0.5f;

	[StartComponent] new private Rigidbody2D rigidbody;
	[StartChildComponent] private PlayerShooter shooter;

	private Vector3 target;
	private bool wasLastClickHandled = false;

	protected override void OnStart() {
		target = transform.position;
		MouseManager.Instance.Subscribe(this);
	}

	protected override void OnFixedUpdate(float dT) {
		Vector3 delta = target - transform.position;
		delta.z = 0.0f;
		Vector3 dir = delta.normalized;
		if (delta.sqrMagnitude < decelerateRange * decelerateRange) {
			dir = Vector3.zero;
		}

		rigidbody.velocity = dir * moveSpeed;
	}

	public void DidMouseClick(MouseClick click, ref bool wasClickConsumed) {
	}

	public void DidMouseHeld(MouseClick click) {
		target = click.pos;
	}

	public float MouseClickPriority() {
		return 0.0f;
	}
}

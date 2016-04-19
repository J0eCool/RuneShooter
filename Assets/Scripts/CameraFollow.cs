using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : JComponent {
	[SerializeField] private Transform target;
	[SerializeField] private float correctionCoefficient = 5.0f;
	[SerializeField] private float mouseLookCoefficient = 0.35f;

	protected override void OnFixedUpdate() {
		Vector2 pos2d = transform.position;
		Vector2 mouseDelta = (MouseManager.Instance.WorldPos - pos2d) * mouseLookCoefficient;
		Vector2 targetPos = new Vector2(target.position.x, target.position.y) + mouseDelta;
		Vector2 delta = targetPos - pos2d;
		Vector2 toMove = delta * Time.fixedDeltaTime * correctionCoefficient;
		if (toMove.sqrMagnitude >= delta.sqrMagnitude) {
			transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
		} else {
			transform.position += (Vector3)toMove;
		}
	}
}

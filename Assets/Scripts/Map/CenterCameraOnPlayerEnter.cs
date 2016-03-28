using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CenterCameraOnPlayerEnter : JComponent {
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject == PlayerManager.Instance.Player) {
			centerCamera();
		}
	}

	private void centerCamera() {
		Transform cameraTransform = Camera.main.transform;
		Vector3 pos = transform.position;
		pos.z = cameraTransform.position.z;
		cameraTransform.position = pos;
	}
}

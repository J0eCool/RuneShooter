using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomPlayerEnterHandler : RoomAwareComponent {
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject == PlayerManager.Instance.Player) {
			enterRoom();
		}
	}

	private void enterRoom() {
		RoomManager.Instance.SetActiveRoom(room);
		centerCamera();
	}

	private void centerCamera() {
		Transform cameraTransform = Camera.main.transform;
		Vector3 pos = transform.position;
		pos.z = cameraTransform.position.z;
		cameraTransform.position = pos;
	}
}

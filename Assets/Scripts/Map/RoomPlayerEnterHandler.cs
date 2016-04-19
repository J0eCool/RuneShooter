using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface RandomPositionGizmoDrawer {
	void DrawRandomGizmos();
}

public class RoomPlayerEnterHandler : RoomAwareComponent {
	[SerializeField] private bool drawChildRandomPositions = false;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject == PlayerManager.Instance.Player) {
			enterRoom();
		}
	}

	private void enterRoom() {
		RoomManager.Instance.SetActiveRoom(room);
	}

	void OnDrawGizmos() {
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
		Vector2 size = VectorUtil.Mult(transform.lossyScale, boxCollider.size);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, size);

		if (drawChildRandomPositions) {
			Gizmos.color = Color.cyan;
			var drawers = transform.parent.GetComponentsInChildren<RandomPositionGizmoDrawer>();
			foreach (var drawer in drawers) {
				drawer.DrawRandomGizmos();
			}
		}
	}
}

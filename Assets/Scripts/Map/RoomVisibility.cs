using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomVisibility : RoomAwareComponent, RoomChangeResponder {
	private bool hasBeenSeen = false;

	protected override void OnStart() {
		RoomManager.Instance.Subscribe(this);
		setVisible(false);
	}

	public void DidSetActiveRoom(Room activeRoom) {
		if (!hasBeenSeen && activeRoom == room) {
			setVisible(true);
		}
	}

	private void setVisible(bool visible) {
		foreach (Renderer renderer in transform.parent.GetComponentsInChildren<Renderer>()) {
			renderer.enabled = visible;
		}
		foreach (GameObject wall in room.associatedWalls) {
			foreach (Renderer renderer in wall.GetComponentsInChildren<Renderer>()) {
				renderer.enabled = visible;
			}
		}
	}
}

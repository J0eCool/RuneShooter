using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface RoomChangeResponder {
	void DidSetActiveRoom(Room activeRoom);
}

public class RoomManager : SingletonComponent<RoomManager> {
	public Room ActiveRoom { get; private set; }

	private List<RoomChangeResponder> registeredResponders = new List<RoomChangeResponder>();

	public void Subscribe(RoomChangeResponder responder) {
		registeredResponders.Add(responder);
	}

	public void Unsubscribe(RoomChangeResponder responder) {
		registeredResponders.Remove(responder);
	}

	public void SetActiveRoom(Room room) {
		ActiveRoom = room;

		foreach (RoomChangeResponder responder in registeredResponders) {
			responder.DidSetActiveRoom(ActiveRoom);
		}
	}
}

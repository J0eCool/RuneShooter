using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface RoomChangeResponder {
	void DidSetActiveRoom(Room activeRoom);
}

public class RoomManager : SingletonComponent<RoomManager> {
	public Room ActiveRoom { get; private set; }

	private List<RoomChangeResponder> changeResponders = new List<RoomChangeResponder>();

	public void Subscribe(RoomChangeResponder responder) {
		changeResponders.Add(responder);
	}

	public void Unsubscribe(RoomChangeResponder responder) {
		changeResponders.Remove(responder);
	}

	public void SetActiveRoom(Room room) {
		if (ActiveRoom != room) {
			ActiveRoom = room;

			foreach (RoomChangeResponder responder in changeResponders) {
				responder.DidSetActiveRoom(ActiveRoom);
			}
		}
	}
}

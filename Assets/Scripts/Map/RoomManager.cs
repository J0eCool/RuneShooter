using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface RoomChangeResponder {
	void DidSetActiveRoom(Room activeRoom);
}

public class RoomManager : Dispatcher<RoomManager, RoomChangeResponder> {
	public Room ActiveRoom { get; private set; }

	public void SetActiveRoom(Room room) {
		if (ActiveRoom != room) {
			ActiveRoom = room;
			Dispatch(responder => responder.DidSetActiveRoom(ActiveRoom));
		}
	}
}

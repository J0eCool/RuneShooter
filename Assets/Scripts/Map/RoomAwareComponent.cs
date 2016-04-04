using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomAwareComponent : JComponent {
	protected Room room;

	public void SetRoom(Room room) {
		this.room = room;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseClick {
	public Vector2 pos;
	public RaycastHit2D[] hits;
}

public interface MouseClickResponder {
	void DidMouseClick(MouseClick click, ref bool wasClickConsumed);
	void DidMouseHeld(MouseClick click);
	float MouseClickPriority();
}

public class MouseManager : Dispatcher<MouseManager, MouseClickResponder> {
	public Vector2 WorldPos { get; private set; }

	private bool wasLastClickHandled = false;

	protected override void OnUpdate(float dT) {
		Vector3 mousePos = Input.mousePosition;
		if (Input.touchCount > 0) {
			mousePos = Input.GetTouch(0).position;
		}
		WorldPos = Camera.main.ScreenToWorldPoint(mousePos);

		bool didClick = Input.GetButtonDown("Click");
		bool clickHeld = Input.GetButton("Click") && Input.touchCount < 2;
		bool clickHandled = false;
		if (!clickHeld) {
			wasLastClickHandled = false;
		}
		if (didClick) {
			MouseClick clickObject = new MouseClick();
			clickObject.pos = WorldPos;
			clickObject.hits = Physics2D.RaycastAll(WorldPos, Vector2.zero, 0.001f);
			Dispatch(responder => {
				if (!clickHandled) {
					responder.DidMouseClick(clickObject, ref clickHandled);
				}
			});
			if (clickHandled) {
				wasLastClickHandled = true;
			}
		}
		if (clickHeld && !wasLastClickHandled) {
			MouseClick clickObject = new MouseClick();
			clickObject.pos = WorldPos;
			Dispatch(responder => responder.DidMouseHeld(clickObject));
		}
	}

	protected override void PostSubscribe(MouseClickResponder responder) {
		responders.Sort((r1, r2) =>
			Comparer<float>.Default.Compare(
				r1.MouseClickPriority(),
				r2.MouseClickPriority()));
	}
}

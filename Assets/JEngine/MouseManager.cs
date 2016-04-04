using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : SingletonComponent<MouseManager> {
	public Vector2 WorldPos { get; private set; }
	protected override void OnUpdate() {
		Vector3 mousePos = Input.mousePosition;
		WorldPos = Camera.main.ScreenToWorldPoint(mousePos);
	}
}

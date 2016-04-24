using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotate : JComponent {
	[SerializeField] private float speed = 30.0f;

	protected override void OnUpdate() {
		transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime);
	}
}

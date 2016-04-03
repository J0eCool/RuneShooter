using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomStartingOffset : JComponent {
	[SerializeField] private float maxOffsetDistance = 1.0f;

	protected override void OnStart() {
		transform.position += (Vector3)(Random.insideUnitCircle * maxOffsetDistance);
	}

	protected override void OnUpdate() {
	}
}

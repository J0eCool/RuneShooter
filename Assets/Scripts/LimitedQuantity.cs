using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LimitedQuantity {
	[SerializeField] private int max = 10;
	[SerializeField] private float regen = 0.0f;

	public int Max { get { return max; } }
	public int Current { get; protected set; }
	public float Fraction { get { return (float)Current / max; } }

	private float partial = 0.0f;

	public void OnStart() {
		Current = max;
	}

	public void OnUpdate(float deltaTime) {
		partial += regen * deltaTime;
		int delta = (int)partial;
		Add(delta);
		partial -= delta;
	}

	public void Add(int delta) {
		Current = Mathf.Clamp(Current + delta, 0, max);
	}
}

public interface HasQuantity {
	LimitedQuantity GetQuantity();
}

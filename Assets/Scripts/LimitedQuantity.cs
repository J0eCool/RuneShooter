using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LimitedQuantity {
	[SerializeField] private int max = 10;

	public int Max { get { return max; } }
	public int Current { get; protected set; }
	public float Fraction { get { return (float)Current / max; } }

	public void OnStart() {
		Current = max;
	}

	public void Add(int delta) {
		Current = Mathf.Clamp(Current + delta, 0, max);
	}
}

public interface HasQuantity {
	LimitedQuantity GetQuantity();
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MathUtil {
	public static float AbsMax(float a, float b) {
		if (Mathf.Abs(a) > Mathf.Abs(b)) {
			return a;
		} else {
			return b;
		}
	}

	public static void LookAt2D(Transform transform, Vector3 pos) {
		Vector3 delta = pos - transform.position;
		float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomUtil {
	public static float Normalized(float lo, float hi) {
		float diff = hi - lo;
		float randDelta = Random.Range(0.0f, diff) / 2.0f;
		return Random.Range(lo + randDelta, hi - randDelta);
	}

	public static bool Bool(float trueProbability = 0.5f) {
		return Random.Range(0.0f, 1.0f) < trueProbability;
	}

	public static int Sign() {
		return Bool() ? 1 : -1;
	}

	public static T Element<T>(T[] list) {
		int idx = Random.Range(0, list.Length);
		return list[idx];
	}
}

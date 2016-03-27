using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[System.Serializable]
public class ShotData {
	public float attackSpeed = 1.4f;
	public float numBullets = 1.0f;
	public float projectileSpeed = 1.0f;
	public float baseSpread = 5.0f;
	public float perBulletSpread = 5.0f;
	public float randomSpread = 0.0f;
	public float cyclicSpread = 0.0f;
	public float cyclicFrequency = 1.0f;

	public ShotData Copy() {
		ShotData ret = new ShotData();
		System.Type t = typeof(ShotData);
		foreach (FieldInfo field in t.GetFields()) {
			field.SetValue(ret, field.GetValue(this));
		}
		return ret;
	}
}

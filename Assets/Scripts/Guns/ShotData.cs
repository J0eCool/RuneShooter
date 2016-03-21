using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		ShotData shot = new ShotData();
		shot.attackSpeed = attackSpeed;
		shot.numBullets = numBullets;
		shot.projectileSpeed = projectileSpeed;
		shot.baseSpread = baseSpread;
		shot.perBulletSpread = perBulletSpread;
		shot.randomSpread = randomSpread;
		shot.cyclicSpread = cyclicSpread;
		shot.cyclicFrequency = cyclicFrequency;
		return shot;
	}
}

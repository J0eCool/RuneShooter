using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunSupportData : ScriptableObject {
	public float manaMultiplier = 1.0f;
	public float increasedAttackSpeed = 0.0f;
	public float increasedProjectileSpeed = 0.0f;
	public float additionalProjectiles = 0.0f;

	public void Modify(ShotData shot) {
		increase(ref shot.attackSpeed, increasedAttackSpeed);
		increase(ref shot.projectileSpeed, increasedProjectileSpeed);

		shot.manaCost *= manaMultiplier;

		shot.numBullets += additionalProjectiles;
	}

	private void increase(ref float value, float percent) {
		value *= 1.0f + percent / 100.0f;
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Assets/Create/Data/GunSupportData")]
	public static void CreateSupportData() {
		ScriptableObjectUtil.CreateAsset<GunSupportData>();
	}
#endif
}

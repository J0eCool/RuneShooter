using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GunData : ScriptableObject {
	public GameObject bulletPrefab = null;
	public float attackSpeed = 1.4f;
	public float numBullets = 1.0f;
	public float baseSpread = 5.0f;
	public float perBulletSpread = 5.0f;
	public float randomSpread = 0.0f;
	public float cyclicSpread = 0.0f;
	public float cyclicFrequency = 1.0f;

	private float shotTimer = 0.0f;
	private float shotNumPartial = 0.0f;

	public void OnUpdate(bool isShootPressed, Vector3 position) {
		if (shotTimer <= 0.0f && isShootPressed) {
			shoot(position);
		}
		updateShotTimer();
	}

	private void updateShotTimer() {
		shotTimer -= Time.deltaTime;
		if (shotTimer < 0.0f) {
			shotTimer = 0.0f;
		}
	}

	private void shoot(Vector3 pos) {
		Vector3 mousePos = MouseManager.Instance.WorldPos;
		Vector3 aimDir = (mousePos - pos).normalized;
		float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

		float toShootFloat = shotNumPartial + numBullets;
		int toShoot = (int)toShootFloat;
		shotNumPartial = toShootFloat - toShoot;

		float totalAngle = baseSpread + perBulletSpread * (toShoot - 1);

		for (int i = 0; i < toShoot; ++i) {
			float randomAngle = Random.Range(-randomSpread, randomSpread) / 2.0f;
			float cyclicAngle = cyclicSpread * Mathf.Sin(Time.time * cyclicFrequency * 2.0f * Mathf.PI);
			float multiBulletAngle = 0.0f;
			if (toShoot > 1) {
				multiBulletAngle = totalAngle * (i - (toShoot - 1) / 2.0f) / (toShoot - 1);
			}
			float angle = aimAngle + cyclicAngle + randomAngle + multiBulletAngle;
			Vector3 dir = VectorUtil.Unit(angle * Mathf.Deg2Rad);

			GameObject bulletObject = Instantiate(bulletPrefab);
			Bullet bullet = bulletObject.GetComponent<Bullet>();
			bullet.Init(pos, dir);
		}

		shotTimer += 1.0f / attackSpeed;
	}

	[MenuItem("Assets/Create/Data/GunData")]
	public static void CreateGunData() {
		ScriptableObjectUtil.CreateAsset<GunData>();
	}
}

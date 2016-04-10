using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Gun {
	public GunData gunData = null;
	public List<GunSupportData> supports = new List<GunSupportData>();

	private float shotTimer = 0.0f;
	private float shotNumPartial = 0.0f;

	public void OnUpdate(bool isShootPressed, Vector3 position, LimitedQuantity mana) {
		if (shotTimer <= 0.0f && isShootPressed) {
			int manaCost = (int)getShotData().manaCost;
			if (mana.Current >= manaCost) {
				mana.Add(-manaCost);
				shoot(position);
			}
		}
		updateShotTimer();
	}

	private void updateShotTimer() {
		shotTimer -= Time.deltaTime;
		if (shotTimer < 0.0f) {
			shotTimer = 0.0f;
		}
	}

	private ShotData getShotData() {
		ShotData shot = gunData.shotData.Copy();
		foreach (GunSupportData support in supports) {
			support.Modify(shot);
		}
		return shot;
	}

	private void shoot(Vector3 pos) {
		ShotData shot = getShotData();

		Vector3 mousePos = MouseManager.Instance.WorldPos;
		Vector3 aimDir = (mousePos - pos).normalized;
		float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

		float toShootFloat = shotNumPartial + shot.numBullets;
		int toShoot = (int)toShootFloat;
		shotNumPartial = toShootFloat - toShoot;

		float totalAngle = shot.baseSpread + shot.perBulletSpread * (toShoot - 1);

		for (int i = 0; i < toShoot; ++i) {
			float randomAngle = Random.Range(-shot.randomSpread, shot.randomSpread) / 2.0f;
			float cyclicAngle = shot.cyclicSpread * Mathf.Sin(Time.time * shot.cyclicFrequency * 2.0f * Mathf.PI);
			float multiBulletAngle = 0.0f;
			if (toShoot > 1) {
				multiBulletAngle = totalAngle * (i - (toShoot - 1) / 2.0f) / (toShoot - 1);
			}
			float angle = aimAngle + cyclicAngle + randomAngle + multiBulletAngle;
			Vector3 dir = VectorUtil.Unit(angle * Mathf.Deg2Rad);

			GameObject bulletObject = GameObject.Instantiate(gunData.bulletPrefab);
			Bullet bullet = bulletObject.GetComponent<Bullet>();
			bullet.Init(shot, pos, dir);
		}

		shotTimer += 1.0f / shot.attackSpeed;
	}
}

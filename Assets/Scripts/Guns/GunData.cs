using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GunData : ScriptableObject {
	public GameObject bulletPrefab = null;
	public float attackSpeed = 1.4f;

	private float shotTimer = 0.0f;

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

	private void shoot(Vector3 position) {
		GameObject bulletObject = Instantiate(bulletPrefab);
		Bullet bullet = bulletObject.GetComponent<Bullet>();
		Vector3 pos = position;
		Vector3 mousePos = MouseManager.Instance.WorldPos;
		Vector3 mouseDelta = mousePos - pos;
		bullet.Init(position, mouseDelta);
		shotTimer += 1.0f / attackSpeed;
	}

	[MenuItem("Assets/Create/Data/GunData")]
	public static void CreateGunData() {
		ScriptableObjectUtil.CreateAsset<GunData>();
	}
}

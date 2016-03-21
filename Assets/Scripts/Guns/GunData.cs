using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GunData : ScriptableObject {
	public GameObject bulletPrefab = null;
	public ShotData shotData;

	[MenuItem("Assets/Create/Data/GunData")]
	public static void CreateGunData() {
		ScriptableObjectUtil.CreateAsset<GunData>();
	}
}

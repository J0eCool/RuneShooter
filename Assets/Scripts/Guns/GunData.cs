using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunData : ScriptableObject {
	public GameObject bulletPrefab = null;
	public Sprite icon;
	public ShotData shotData;

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Assets/Create/Data/GunData")]
	public static void CreateGunData() {
		ScriptableObjectUtil.CreateAsset<GunData>();
	}
#endif
}

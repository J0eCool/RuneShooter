using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteRing : JComponent {
	[SerializeField] private GameObject spritePrefab;
	[SerializeField] private int numSprites = 6;
	[SerializeField] private float radius = 2.0f;
	[SerializeField] private float angleOffset = 0.0f;

	protected override void OnStart() {
		for (int i = 0; i < numSprites; ++i) {
			GameObject sprite = Instantiate(spritePrefab);
			sprite.transform.parent = transform;
			float angle = Mathf.PI * 2.0f / numSprites * i;
			sprite.transform.localPosition = VectorUtil.Unit(angle) * radius;
			sprite.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle * Mathf.Rad2Deg + angleOffset);
		}
	}
}

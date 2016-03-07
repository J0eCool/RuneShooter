using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooter : JComponent {
	[SerializeField] private GunData gun;

	protected override void OnUpdate() {
		gun.OnUpdate(Input.GetButton("Fire1"), transform.position);
	}
}

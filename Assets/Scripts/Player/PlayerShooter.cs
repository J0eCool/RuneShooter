using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooter : JComponent, HasQuantity {
	[SerializeField] private LimitedQuantity mana;
	[SerializeField] private Gun gun;
	[SerializeField] private Gun gun2;

	protected override void OnStart() {
		mana.OnStart();
	}

	protected override void OnUpdate() {
		mana.OnUpdate(Time.deltaTime);

		gun.OnUpdate(Input.GetButton("Fire"), transform.position, mana);
		gun2.OnUpdate(Input.GetButton("Fire2"), transform.position, mana);
	}

	public LimitedQuantity GetQuantity() {
		return mana;
	}
}

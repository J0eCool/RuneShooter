using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooter : JComponent, HasQuantity {
	[SerializeField] private LimitedQuantity mana;
	[SerializeField] private Gun gun;

	private Transform target;

	protected override void OnStart() {
		mana.OnStart();
	}

	protected override void OnUpdate() {
		mana.OnUpdate(Time.deltaTime);

		gun.OnUpdate(target, transform.position, mana);
	}

	public LimitedQuantity GetQuantity() {
		return mana;
	}

	public void SetTarget(Transform target) {
		this.target = target;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooter : JComponent, HasQuantity {
	[SerializeField] private LimitedQuantity mana;
	[SerializeField] private Gun gun;
	[SerializeField] private GameObject reticle;

	private Transform target;

	protected override void OnStart() {
		mana.OnStart();
	}

	protected override void OnUpdate() {
		mana.OnUpdate(Time.deltaTime);
		gun.OnUpdate(target, transform.position, mana);
		updateReticle();
	}

	private void updateReticle() {
		if (reticle) {
			bool hasTarget = target != null;
			reticle.SetActive(hasTarget);
			if (hasTarget) {
				reticle.transform.position = target.position;
			}
		}
	}

	public LimitedQuantity GetQuantity() {
		return mana;
	}

	public void SetTarget(Transform target) {
		this.target = target;
	}
}

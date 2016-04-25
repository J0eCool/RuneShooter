using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooter : JComponent, HasQuantity {
	[SerializeField] private LimitedQuantity mana;
	[SerializeField] private Gun[] guns;
	[SerializeField] private GameObject reticle;

	private int selectedGunIndex = 0;
	private Transform target;

	public Gun[] Guns { get { return guns; } }
	public int SelectedGunIndex { get { return selectedGunIndex; } }

	protected override void OnStart() {
		mana.OnStart();
	}

	protected override void OnUpdate() {
		mana.OnUpdate(Time.deltaTime);
		Gun selectedGun = guns[selectedGunIndex];
		selectedGun.OnUpdate(target, transform.position, mana);
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

	public void SetSelectedGunIndex(int index) {
		if (index >= 0 && index < guns.Length) {
			selectedGunIndex = index;
		}
	}
}

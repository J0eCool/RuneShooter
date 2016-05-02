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

	protected override void OnUpdate(float dT) {
		updateSlotInput();
		mana.OnUpdate(dT);
		Gun selectedGun = guns[selectedGunIndex];
		bool shouldShoot = target != null;
		Vector3 targetPos = target != null ? target.position : Vector3.zero;
		if (Input.GetButton("Fire")) {
			shouldShoot = true;
			targetPos = MouseManager.Instance.WorldPos;
		}
		selectedGun.OnUpdate(dT, shouldShoot, targetPos, transform.position, mana);
		updateReticle();
	}

	static readonly string[] slotInputs = { "Slot1", "Slot2", "Slot3" };
	private void updateSlotInput() {
		for (int i = 0; i < slotInputs.Length; ++i) {
			if (Input.GetButtonDown(slotInputs[i])) {
				SetSelectedGunIndex(i);
			}
		}
	}

	private void updateReticle() {
		if (reticle) {
			bool hasTarget = target != null;
			reticle.SetActive(hasTarget);
			if (hasTarget) {
				VectorUtil.Set2DPos(reticle.transform, target.position);
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

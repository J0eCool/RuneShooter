using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooter : JComponent, HasQuantity, MouseClickResponder {
	[SerializeField] private LimitedQuantity mana;
	[SerializeField] private Gun[] guns;
	[SerializeField] private GameObject reticle;

	private int selectedGunIndex = 0;
	public Transform Target { get; private set; }

	public Gun[] Guns { get { return guns; } }
	public int SelectedGunIndex { get { return selectedGunIndex; } }

	protected override void OnStart() {
		mana.OnStart();
		MouseManager.Instance.Subscribe(this);
	}

	protected override void OnUpdate(float dT) {
		updateSlotInput();
		mana.OnUpdate(dT);
		Gun selectedGun = guns[selectedGunIndex];
		bool shouldShoot = Target != null;
		Vector3 targetPos = Target != null ? Target.position : Vector3.zero;
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
			bool hasTarget = Target != null;
			reticle.SetActive(hasTarget);
			if (hasTarget) {
				VectorUtil.Set2DPos(reticle.transform, Target.position);
			}
		}
	}

	public LimitedQuantity GetQuantity() {
		return mana;
	}

	public void SetSelectedGunIndex(int index) {
		if (index >= 0 && index < guns.Length) {
			selectedGunIndex = index;
		}
	}

	public void DidMouseClick(MouseClick click, ref bool wasClickConsumed) {
		int enemyLayer = LayerMask.NameToLayer("Enemy");
		foreach (var hit in click.hits) {
			if (hit.collider.gameObject.layer == enemyLayer) {
				Target = hit.transform;
				wasClickConsumed = true;
			}
		}
	}

	public void DidMouseHeld(MouseClick click) {
	}

	public float MouseClickPriority() {
		return -10.0f;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooter : JComponent {
	[SerializeField] private GunData gunData;
	[SerializeField] private List<GunSupportData> supports;

	private Gun gun = new Gun();
	private List<bool> enabledSupports = new List<bool>();

	protected override void OnStart() {
		gun.gunData = gunData;
		for (int i = 0; i < supports.Count; ++i) {
			enabledSupports.Add(false);
		}
		recacheActiveSupports();
	}

	protected override void OnUpdate() {
		updateSupportSlots();

		gun.OnUpdate(Input.GetButton("Fire"), transform.position);
	}

	private void updateSupportSlots() {
		int toggled = -1;
		if (Input.GetButtonDown("Slot1")) {
			toggled = 0;
		}
		else if (Input.GetButtonDown("Slot2")) {
			toggled = 1;
		}
		else if (Input.GetButtonDown("Slot3")) {
			toggled = 2;
		}
		if (toggled != -1) {
			enabledSupports[toggled] = !enabledSupports[toggled];
			recacheActiveSupports();
		}
	}

	private void recacheActiveSupports() {
		List<GunSupportData> activeSupports = new List<GunSupportData>();
		for (int i = 0; i < supports.Count; ++i) {
			if (enabledSupports[i]) {
				activeSupports.Add(supports[i]);
			}
		}
		gun.supports = activeSupports;
	}
}

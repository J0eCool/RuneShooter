using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GunSelectButton : JComponent {
	[SerializeField] private int gunIndex = 0;
	[SerializeField] private Image image;

	private PlayerShooter shooter;

	protected override void OnStart() {
		shooter = PlayerManager.Instance.Player.GetComponentInChildren<PlayerShooter>();
	}

	protected override void OnUpdate() {
		if (gunIndex >= 0 && gunIndex < shooter.Guns.Length) {
			Gun gun = shooter.Guns[gunIndex];
			image.sprite = gun.data.icon;
		}
	}

	public void DidClick() {
		shooter.SetSelectedGunIndex(gunIndex);
	}
}

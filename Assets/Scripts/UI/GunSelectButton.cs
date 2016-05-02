using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GunSelectButton : JComponent {
	[SerializeField] private int gunIndex = 0;
	[SerializeField] private Image image;
	[SerializeField] private Color selectedColor;

	[StartComponent] private Image background;
	private PlayerShooter shooter;
	private Color unselectedColor;

	protected override void OnStart() {
		shooter = PlayerManager.Instance.Player.GetComponentInChildren<PlayerShooter>();
		unselectedColor = background.color;
	}

	protected override void OnUpdate() {
		if (gunIndex >= 0 && gunIndex < shooter.Guns.Length) {
			Gun gun = shooter.Guns[gunIndex];
			image.sprite = gun.data.icon;
		}
		bool isSelected = gunIndex == shooter.SelectedGunIndex;
		background.color = isSelected ? selectedColor : unselectedColor;
	}

	public void DidClick() {
		shooter.SetSelectedGunIndex(gunIndex);
	}
}

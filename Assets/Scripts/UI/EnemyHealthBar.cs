using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealthBar : QuantityBar {
	private Renderer[] renderers;
	private bool isVisible = true;

	protected override void OnStart() {
		base.OnStart();

		renderers = GetComponentsInChildren<Renderer>();
		setVisible(false);
	}

	protected override void OnUpdate() {
		base.OnUpdate();

		setVisible(quantity.Current < quantity.Max);
	}

	private void setVisible(bool visible) {
		if (isVisible != visible) {
			isVisible = visible;
			foreach (var r in renderers) {
				r.enabled = visible;
			}
		}
	}
}

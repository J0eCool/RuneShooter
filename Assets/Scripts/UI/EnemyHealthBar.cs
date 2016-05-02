using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealthBar : QuantityBar {
	private Renderer[] renderers;

	protected override void OnStart() {
		base.OnStart();

		renderers = GetComponentsInChildren<Renderer>();
		setVisible(false);
	}

	protected override void OnUpdate(float dT) {
		base.OnUpdate(dT);

		setVisible(quantity.Current < quantity.Max);
	}

	private void setVisible(bool visible) {
		foreach (var r in renderers) {
			r.enabled = visible;
		}
	}
}

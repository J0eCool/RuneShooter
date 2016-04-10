using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuantityBar : JComponent {
	[SerializeField] private GameObject target;
	[SerializeField] private Transform barImage;
	[SerializeField] private Text text = null;
	[SerializeField] private bool forceLeftAlign = true;

	protected LimitedQuantity quantity;
	private float baseWidth;
	private string baseText;

	protected override void OnStart() {
		var targetComponent = target ? target.GetComponent<HasQuantity>() : null;
		quantity = targetComponent != null ? targetComponent.GetQuantity() : null;

		baseWidth = barImage.localScale.x;
		baseText = text ? text.text : null;
	}

	protected override void OnUpdate() {
		if (quantity == null) {
			return;
		}

		float pct = quantity.Fraction;
		pct = Mathf.Clamp01(pct);
		float width = baseWidth * pct;

		var scale = barImage.localScale;
		scale.x = width;
		barImage.localScale = scale;

		if (forceLeftAlign) {
			var pos = barImage.localPosition;
			pos.x = (width - baseWidth) / 2.0f;
			barImage.localPosition = pos;
		}

		if (text) {
			text.text = string.Format(baseText, quantity.Current, quantity.Max);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuantityBar : JComponent {
	[SerializeField] private GameObject target;
	[SerializeField] private Transform barImage;

	protected LimitedQuantity quantity;
	private float baseWidth;

	protected override void OnStart() {
		var targetComponent = target.GetComponent<HasQuantity>();
		quantity = targetComponent.GetQuantity();

		baseWidth = barImage.localScale.x;
	}

	protected override void OnUpdate() {
		float pct = (quantity != null) ? quantity.Fraction : 0.0f;
		pct = Mathf.Clamp01(pct);
		float width = baseWidth * pct;

		var scale = barImage.localScale;
		scale.x = width;
		barImage.localScale = scale;

		var pos = barImage.localPosition;
		pos.x = (width - baseWidth) / 2.0f;
		barImage.localPosition = pos;
	}
}

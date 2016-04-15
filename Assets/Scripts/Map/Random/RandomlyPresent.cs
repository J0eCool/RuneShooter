using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomlyPresent : JComponent {
	[SerializeField] private float percentKeepChance = 50.0f;

	protected override void OnStart() {
		bool shouldKeep = RandomUtil.Bool(percentKeepChance / 100.0f);
		if (!shouldKeep) {
			Remove();
		}
	}
}

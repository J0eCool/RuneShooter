using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFacing : JComponent {
	[StartChildComponent] private PlayerShooter shooter;

	protected override void OnFixedUpdate(float dT) {
		Vector3 lookAtPos = shooter.Target != null ? shooter.Target.position : (Vector3)MouseManager.Instance.WorldPos;
		MathUtil.LookAt2D(transform, lookAtPos);
	}
}

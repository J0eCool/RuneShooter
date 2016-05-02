using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : JComponent, HasQuantity {
	[SerializeField] private LimitedQuantity health;
	[SerializeField] private bool isEnemy = true;
	[SerializeField] private Material damageFlashMaterial;
	[SerializeField] private float damageFlashDuration = 0.25f;
	[SerializeField] private float damageFlashInterval = 0.05f;
	[SerializeField] private float invinciblityDuration = 0.1f;
	[SerializeField] private bool debugInvincible = false;

	public bool IsEnemy { get { return isEnemy; } }

	private float damageTimer = 0.0f;
	private float flashTimer = 0.0f;
	private float invincibleUntil = 0.0f;
	private bool didFlash = false;
	private Material baseMaterial;

	[StartComponent] new private Renderer renderer;

	protected override void OnStart() {
		base.OnStart();

		health.OnStart();
		baseMaterial = renderer.material;
	}

	protected override void OnUpdate(float dT) {
		if (health.Current <= 0) {
			Remove();
			return;
		}

		health.OnUpdate(dT);

		if (damageTimer > 0.0f) {
			damageTimer -= dT;
			flashTimer -= dT;
			if (damageTimer <= 0.0f) {
				stopFlashing();
			} else if (flashTimer <= 0.0f) {
				toggleFlash();
			}
		}
	}

	private void beginFlashing() {
		damageTimer = damageFlashDuration;
		didFlash = false;
		toggleFlash();
	}

	private void stopFlashing() {
		flashTimer = 0.0f;
		renderer.material = baseMaterial;
	}

	private void toggleFlash() {
		flashTimer = damageFlashInterval;
		renderer.material = didFlash ? baseMaterial : damageFlashMaterial;
		didFlash = !didFlash;
	}

	private bool isInvincible() {
		return Time.timeSinceLevelLoad < invincibleUntil || debugInvincible;
	}

	public void TakeDamage(int damage) {
		if (!isInvincible() && damage > 0) {
			health.Add(-damage);
			invincibleUntil = Time.timeSinceLevelLoad + invinciblityDuration;
			beginFlashing();
		}
	}

	public void SetInvincibleFor(float duration) {
		invincibleUntil = Time.timeSinceLevelLoad + duration;
	}

	public LimitedQuantity GetQuantity() {
		return health;
	}
}

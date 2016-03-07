using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LimitedQuantity : JComponent {
	[SerializeField] private int _max = 10;

	public int Max { get { return _max; } }
	public int Current { get; protected set; }
	public float Fraction { get { return (float)Current / _max; } }

	protected override void OnStart() {
		Current = _max;
	}
}

public class Health : LimitedQuantity {
	[SerializeField] private bool _isEnemy = true;
	[SerializeField] private Material _damageFlashMaterial;
	[SerializeField] private float _damageFlashDuration = 0.25f;
	[SerializeField] private float _damageFlashInterval = 0.05f;
	[SerializeField] private float _invinciblityDuration = 0.1f;
	[SerializeField] private bool _debugInvincible = false;

	public bool IsEnemy { get { return _isEnemy; } }

	private float _damageTimer = 0.0f;
	private float _flashTimer = 0.0f;
	private float _invincibleUntil = 0.0f;
	private bool _didFlash = false;
	private Material _baseMaterial;

	[StartComponent] private Renderer _renderer;

	protected override void OnStart() {
		base.OnStart();
		_baseMaterial = _renderer.material;
	}

	protected override void OnUpdate() {
		if (Current <= 0) {
			Remove();
			return;
		}

		if (_damageTimer > 0.0f) {
			_damageTimer -= Time.deltaTime;
			_flashTimer -= Time.deltaTime;
			if (_damageTimer <= 0.0f) {
				stopFlashing();
			} else if (_flashTimer <= 0.0f) {
				toggleFlash();
			}
		}
	}

	private void beginFlashing() {
		_damageTimer = _damageFlashDuration;
		_didFlash = false;
		toggleFlash();
	}

	private void stopFlashing() {
		_flashTimer = 0.0f;
		_renderer.material = _baseMaterial;
	}

	private void toggleFlash() {
		_flashTimer = _damageFlashInterval;
		_renderer.material = _didFlash ? _baseMaterial : _damageFlashMaterial;
		_didFlash = !_didFlash;
	}

	private bool isInvincible() {
		return Time.timeSinceLevelLoad < _invincibleUntil || _debugInvincible;
	}

	public void TakeDamage(int damage) {
		if (!isInvincible() && damage > 0) {
			Current -= damage;
			_invincibleUntil = Time.timeSinceLevelLoad + _invinciblityDuration;
			beginFlashing();
		}
	}

	public void SetInvincibleFor(float duration) {
		_invincibleUntil = Time.timeSinceLevelLoad + duration;
	}
}

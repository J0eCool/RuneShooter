using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : JComponent {
	[SerializeField] private float _speed = 30.0f;
	[SerializeField] private float _range = 150.0f;
	[SerializeField] private int _damage = 2;
	[SerializeField] private bool _isEnemy = false;

	private Vector3 _dir;
	private float _distTraveled = 0.0f;
	private bool _didCollide = false;

	public void Init(Vector3 pos, Vector3 dir) {
		transform.position = pos;
		_dir = dir.normalized;
		MathUtil.LookAt2D(transform, pos + dir);
	}

	protected override void OnFixedUpdate() {
		float dist = _speed * Time.fixedDeltaTime;
		transform.position += _dir * dist;
		_distTraveled += dist;

		if (_distTraveled > _range || _didCollide) {
			Remove();
		}
	}

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponent<Health>();
		if (health && health.IsEnemy != _isEnemy) {
			health.TakeDamage(_damage);
			_didCollide = true;
		} else if (!other.isTrigger) {
			_didCollide = true;
		}
	}
}

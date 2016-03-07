using UnityEngine;
using System;
using System.Reflection;

public class JComponent : MonoBehaviour {
	private PauseManager _pauseManager;

	protected virtual void OnRemove() { }
	protected void Remove() {
		var components = GetComponents<JComponent>();
		foreach (var component in components) {
			component.OnRemove();
		}

		GameObject.Destroy(gameObject);
	}
	public static void Remove(GameObject obj) {
		JComponent component = obj.GetComponent<JComponent>();
		if (component) {
			component.Remove();
		} else {
			GameObject.Destroy(obj);
		}
	}

	protected virtual void OnStart() { }
	void Start() {
		_pauseManager = PauseManager.Instance;
		setupStartComponents();
		OnStart();
	}

	protected virtual bool CanBePaused { get { return true; } }
	protected virtual void OnUpdate() { }
	void Update() {
		if (!(CanBePaused && _pauseManager.IsPaused)) {
			OnUpdate();
		}
	}

	protected virtual void OnFixedUpdate() { }
	void FixedUpdate() {
		// Don't need to check if game is paused; FixedUpdate doesn't get called if Time.timeScale is 0
		OnFixedUpdate();
	}

	private void setupStartComponents() {
		foreach (FieldInfo field in getPrivateFields()) {
			if (isFieldAnnotated<StartComponentAttribute>(field)) {
				field.SetValue(this, GetComponent(field.FieldType));
			} else if (isFieldAnnotated<StartParentComponentAttribute>(field)) {
				field.SetValue(this, transform.parent.GetComponent(field.FieldType));
			} else if (isFieldAnnotated<StartChildComponentAttribute>(field)) {
				field.SetValue(this, GetComponentInChildren(field.FieldType));
			}
		}
	}

	private FieldInfo[] getPrivateFields() {
		return GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
	}

	private static bool isFieldAnnotated<T>(FieldInfo field) where T : Attribute{
		return Attribute.GetCustomAttribute(field, typeof(T)) != null;
	}
}

[AttributeUsage(AttributeTargets.Field)]
public class StartComponentAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Field)]
public class StartParentComponentAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Field)]
public class StartChildComponentAttribute : Attribute { }

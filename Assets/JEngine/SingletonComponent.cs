using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SingletonComponent<T> : JComponent where T : SingletonComponent<T> {
	public static T Instance { get; private set; }

	protected virtual void OnAwake() { }
	void Awake() {
		Instance = this as T;
		OnAwake();
	}
}

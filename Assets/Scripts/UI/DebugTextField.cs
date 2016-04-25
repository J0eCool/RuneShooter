using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugTextField : SingletonComponent<DebugTextField> {
	[StartComponent] private Text text;

	protected override void OnUpdate() {
		text.text = "";
	}

	public void Log(string message, params object[] args) {
		text.text += "\n" + string.Format(message, args);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Log {
	public static void Format(string formatString, params object[] args) {
		Debug.Log(string.Format(formatString, args));
	}
}

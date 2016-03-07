using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public static class ScriptableObjectUtil {
	public static void CreateAsset<T>() where T : ScriptableObject {
		var asset = ScriptableObject.CreateInstance<T>();
		ProjectWindowUtil.CreateAsset(asset, "New " + typeof(T).Name + ".asset");		
	}
}

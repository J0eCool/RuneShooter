using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScriptableObjectUtil {
#if UNITY_EDITOR
	public static void CreateAsset<T>() where T : ScriptableObject {
		var asset = ScriptableObject.CreateInstance<T>();
		UnityEditor.ProjectWindowUtil.CreateAsset(asset, "New " + typeof(T).Name + ".asset");		
	}
#endif
}

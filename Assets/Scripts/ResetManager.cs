using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetManager : SingletonComponent<ResetManager> {
	protected override void OnUpdate() {
		if (Input.GetButtonDown("Reset")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}

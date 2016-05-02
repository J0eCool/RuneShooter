using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetManager : SingletonComponent<ResetManager> {
	protected override void OnUpdate(float dT) {
		if (Input.GetButtonDown("Reset")) {
			Reset();
		}
	}

	public void Reset() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

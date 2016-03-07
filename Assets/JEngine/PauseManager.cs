using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PauseManager : SingletonComponent<PauseManager> {
	[SerializeField] private GameObject _pauseMenuPrefab;
	[SerializeField] private GameObject _canvas;

	private float _savedTimeScale;
	private GameObject _pauseMenu;

	public bool IsPaused { get; private set; }

	protected override void OnStart() {
		_savedTimeScale = Time.timeScale;
	}

	protected override bool CanBePaused { get { return false; } }

	private void togglePause() {
		setPaused(!IsPaused);
	}

	private void setPaused(bool pause) {
		if (pause == IsPaused) {
			return;
		}

		float toSet = pause ? 0.0f : _savedTimeScale;
		if (pause) {
			_savedTimeScale = Time.timeScale;
		}
		Time.timeScale = toSet;
		IsPaused = pause;

		if (pause) {
			_pauseMenu = Instantiate(_pauseMenuPrefab);
			_pauseMenu.transform.SetParent(_canvas.transform, false);
		} else {
			Remove(_pauseMenu);
		}
	}

	public void Pause() {
		// Calling on Instance for use in UI
		Instance.setPaused(true);
	}
	public void Unpause() {
		// Calling on Instance for use in UI
		Instance.setPaused(false);
	}
	public void Restart() {
		Instance.setPaused(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

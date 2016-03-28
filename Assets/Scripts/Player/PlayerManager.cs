using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : SingletonComponent<PlayerManager> {
	private GameObject _player;
	public GameObject Player {
		get {
			if (!_player) {
				_player = GameObject.FindGameObjectWithTag("Player");
			}
			return _player;
		}
	}
}

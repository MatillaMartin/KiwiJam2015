﻿using UnityEngine;
using System.Collections;

public class KiwiLifeManager : MonoBehaviour {
	public GameEngine gameLogic;

	void Start()
	{
		gameLogic = GameObject.FindObjectOfType<GameEngine>();
	}

	void Die()
	{
		gameLogic.ReportDeath();
		Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class KiwiLifeManager : MonoBehaviour {
	public GameEngine gameLogic;
	void Die()
	{
		gameLogic.ReportDeath();
		Destroy(gameObject);
	}
}

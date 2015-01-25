using UnityEngine;
using System.Collections;

public class KiwiLifeManager : MonoBehaviour {
	public GameEngine gameLogic;
	
	public void Start()
	{
		gameLogic = FindObjectOfType<GameEngine>();
	}
	
	public void Die()
	{
		gameLogic.ReportDeath();
		Destroy(gameObject);
	}
}

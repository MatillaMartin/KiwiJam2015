using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	public AudioSource[] firstLevelInsoults;
	public AudioSource[] secondLevelInsoults;
	public AudioSource[] thirdLevelInsoults;
	public AudioSource[] fourLevelInsoults;

	public AudioSource leftSharkWarning, rightSharkWarning;
	public AudioSource leftMeteorWarning, rightMeteorWarning;

	public float minTimeToSpawnShark, maxTimeToSpawnShark;
	public float minTimeToSpawnMeteorite, maxTimeToSpawnMeteorite;

	public float minTimeBetweenWarningAndEvent;
	public float maxTimeBetweenWarningAndEvent;


	private Component SharkSpawner, MeteoriteLeftSpawner, MeteoriteRightSpawner;

	// Use this for initialization
	void Start () {
		SharkSpawner = GetComponent("SharkSpawner") as Component;
		MeteoriteLeftSpawner = GetComponent("Meteorite Spawn Left") as Component;
		MeteoriteRightSpawner = GetComponent("Meteorite Spawn Right") as Component;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

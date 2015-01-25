using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	public AudioClip[] firstLevelInsults;
	public AudioClip[] secondLevelInsults;
	public AudioClip[] thirdLevelInsults;
	public AudioClip[] fourLevelInsults;
	private AudioSource audioSource;
	
	public float meteorVolume;
	public float mamaKiwiVolume;
	
	public AudioClip[] meteorLeftMessage;
	public AudioClip[] meteorRightMessage;
	
	private int kiwiCount = 20;
	
	public MeteorControl meteorControl;
	public Shark sharkControl;
	
	private float meteorSpawnTimer;
	private bool meteorSideLeft = false;
	public Vector2 meteorSpawnTimeRange = new Vector2(10.0f, 20.0f);
	
	private bool meteorSpawning = false;



	//private Component SharkSpawner, MeteoriteLeftSpawner, MeteoriteRightSpawner;

	// Use this for initialization
	void Start () {
		audioSource = Camera.main.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMeteorLogics();
	}
	
	
	void UpdateMeteorLogics()
	{
		if(!meteorSpawning){
			if(meteorSpawnTimer < 0.0f)
			{
				meteorSpawnTimer = Random.Range(meteorSpawnTimeRange.x, meteorSpawnTimeRange.y);
				meteorSpawning = true;
				meteorSideLeft = Random.Range(0.0f,1.0f) < 0.5f  ? true : false;
				startMeteorMessage();
			}
			else
				meteorSpawnTimer -= Time.deltaTime;
		}
		else
		{
			if(audioSource.isPlaying == false)
			{
				if(meteorSideLeft)
					meteorControl.SpawnLeft();
				else
					meteorControl.SpawnRight();
				meteorSpawning = false;
			}
		}
	}
	
	
	void startMeteorMessage()
	{
		if(meteorSideLeft)
			audioSource.PlayOneShot(meteorLeftMessage[Random.Range(0, meteorLeftMessage.Length - 1)]);
		else
			audioSource.PlayOneShot(meteorRightMessage[Random.Range (0, meteorRightMessage.Length - 1)]);
	}
	
	
	public void ReportDeath()
	{
		kiwiCount--;
	}
}

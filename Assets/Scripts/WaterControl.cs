using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterControl : MonoBehaviour {
	public List<WaterLayer> waterLayers;

	public float waterHeightNormalized = 0.25f;
	public float waterSpawnAreaNormalized = 0.33f;
	private Rect waterLayout;

	// Use this for initialization
	void Start () {
		waterLayers.Sort((x, y) => x.depth.CompareTo(y.depth));
		waterLayout = new Rect (0.0f, 1.0f - waterHeightNormalized, 1.0f, 1.0f);
		setTides ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void setTides()
	{
		Rect waterSpawn = new Rect (waterLayout);
		waterSpawn.width = waterSpawn.width * waterSpawnAreaNormalized;
		waterSpawn.x = 0.5f - waterSpawn.width/2.0f;

		Rect waterSpawnScreen = waterSpawn * new Rect(Screen.width, Screen.height, Screen.width, Screen.height);



		foreach (WaterLayer layer in waterLayers) 
		{
			Vector2 spawnPosition = new Vector2(
				waterSpawnScreen.xMin + Random.Range(0.0f, 1.0f) * waterSpawnScreen.width, 
				waterSpawnScreen.yMin + Random.Range(0.0f, 1.0f) * waterSpawnScreen.height);
			layer.initialPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
			layer.revolutionSeconds = Random.Range(1.0f, 2.0f);
			layer.circleRadius = Random.Range(0.01f, 0.2f);
		}
	}
}

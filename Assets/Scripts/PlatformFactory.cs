using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformFactory : MonoBehaviour {


	private bool m_bPlatformStarted = false;
	private Vector2 m_mousePosition;
	private Vector2 m_platformStart, m_platformEnd;
	
	[SerializeField] GameObject m_platformTile;
	[SerializeField] GameObject m_platformTileLeft;
	[SerializeField] GameObject m_platformTileRight;


	private List<Platform> m_platforms;
	
	void Start () {
		//load textures etc..

		//init platform list
		m_platforms = new List<Platform>();
	}

	void Update () {
		Vector3 worldMouse = Input.mousePosition;
		//get point +10 points away from camera (where 2D is)
		worldMouse.z += 10;
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(worldMouse);
		m_mousePosition = new Vector2(worldPoint.x, worldPoint.y); 


		//if mouse button pressed, start plataform
		if(Input.GetButtonDown("Fire1"))
		{
			Debug.Log("mouse pressed");
			//platform start is always at mouse position
			StartPlatform(m_mousePosition);
		}
		//if mouse released, create plataform
		else if(Input.GetButtonUp("Fire1"))
		{
			Debug.Log("mouse released");
			CreatePlatform(m_platformStart,m_mousePosition);
		}
		//might be interesting to cancel platforms, or maybe should be reserved to erase enemy platforms?
		else if(Input.GetButtonDown("Fire2"))
		{
			Debug.Log("right mouse clicked");
			CancelPlatform();
		}
		else if(m_bPlatformStarted)
		{
			//do something while dragging platform
			onDraggingPlatform(m_platformStart, m_mousePosition);
		}
		else
		{
			//do nothing... yet!
		}
	}

	void StartPlatform(Vector2 platformStart)
	{
		Debug.Log("platorm started at "); Debug.Log (platformStart);
		m_platformStart = platformStart;
		m_bPlatformStarted = true;
		//start event to draw line etc (TODO:visuals)
	}

	void onDraggingPlatform(Vector2 platformStart, Vector2 platformTemporalEnd)
	{
		Vector2 platformVector = platformTemporalEnd - platformStart;
		float angle = Vector2.Angle(platformVector, Vector2.right);
		Debug.Log(angle); //angle dodgy... [0 -> 180 -> 0]
	}

	void CancelPlatform()
	{
		Debug.Log("platorm cancelled");
		m_bPlatformStarted = false;
	}
	void CreatePlatform(Vector2 platformStart, Vector2 platformEnd)
	{
		if(m_bPlatformStarted)
		{
			m_bPlatformStarted = false;	
			Debug.Log("creating platform");
			m_platforms.Add(new Platform(m_platformTile, m_platformTileLeft, m_platformTileRight, platformStart, platformEnd));
		}
	}
}

public class Platform
{
	Vector2 start, end;
	GameObject tile, tileLeft, tileRight; //tile as prefab
	float tileWidth;

	GameObject platform;

	List<GameObject> generatedTiles = new List<GameObject> ();

	public Platform(GameObject platformTile, GameObject platformTileLeft, GameObject platformTileRight, Vector2 platformStart, Vector2 platformEnd)
	{
		this.tile = platformTile;
		this.tileLeft = platformTileLeft;
		this.tileRight = platformTileRight;
		this.tileWidth = this.tile.GetComponent<SpriteRenderer>().renderer.bounds.size.x;
		this.platform = new GameObject ();
		this.platform.name = "Platform";

		this.start = platformStart;
		this.end = platformEnd;
		TileTiles(start, end);
		Remove ();
	}

	public void InstanceTiles(int nTiles)
	{
		generatedTiles.Clear ();
		if (nTiles < 2) {
			Debug.LogError ("at least 2 tiles needed");
			return;
		}
		generatedTiles.Add ((GameObject)GameObject.Instantiate (tileLeft));
		for (int i = 0; i < nTiles - 2; i++) {
			generatedTiles.Add ((GameObject)GameObject.Instantiate (tile));
		}
		generatedTiles.Add ((GameObject)GameObject.Instantiate (tileRight));
	}

	public void PositionTiles(Vector2 platformStart, Vector2 platformDirection, Quaternion orientation, float tileWidth)
	{
		Vector2 tilePosition = platformStart;

		for(int i = 0; i < generatedTiles.Count; i++)
		{
			tilePosition = platformStart + platformDirection.normalized*i*tileWidth;
			generatedTiles[i].transform.position = tilePosition;
			generatedTiles[i].transform.rotation = orientation;
			//set as child
			generatedTiles[i].transform.parent = platform.transform;
		}
	}
	public void AddTile (GameObject tileToAdd)
	{
		generatedTiles.Add (tileToAdd);
	}
	public void AddTile (int index, GameObject tileToAdd)
	{
		generatedTiles.Insert (index, tileToAdd);
	}
	public void RemoveTile(int index)
	{
		GameObject.Destroy(generatedTiles[index]);
		generatedTiles.RemoveAt (index);
	}
	public void RemoveTile()
	{
		RemoveTile (generatedTiles.Count - 1);
	}

	public void DynamicConstruction(Vector2 platformStart, Vector2 platformEnd)
	{
		this.start = platformStart;
		//if starting and ending position are too close, then increase width to accomodate left and right tiles
		if ((platformEnd - platformStart).magnitude < tileWidth) {
			platformEnd = platformStart + Vector2.right * tileWidth;
			this.end = platformEnd;
		}

		Vector2 tilingVector = this.end-this.start;
		float distance = tilingVector.magnitude;
		int nTiles = (int)(distance / this.tileWidth);
		Debug.Log ("ntiles" + nTiles.ToString());

		Quaternion orientation = Quaternion.FromToRotation(Vector2.right, tilingVector);
		platform.transform.rotation = orientation;
		platform.transform.position = this.start;
		
		if (nTiles != generatedTiles.Count) 
		{
			InstanceTiles(nTiles);
			PositionTiles(this.start, this.end, orientation, tileWidth);
		}
	}

	public void TileTiles(Vector2 platformStart, Vector2 platformEnd)
	{
		foreach (GameObject go in generatedTiles) {
			GameObject.Destroy (go);
		}

		//generate orientation and distance variables to ease tiling calculus
		platform.transform.position = start;

		//if starting and ending position are too close, then increase width to accomodate left and right tiles
		if ((end - start).magnitude < tileWidth) {
			Debug.Log("Too small");
			Debug.Log(end);
			Debug.Log(start);
			Debug.Log((end - start).magnitude);
			Debug.Log(tileWidth);
			end = start + Vector2.right*tileWidth;
		}
		bool leftToRight = start.x <= end.x;
		Vector2 tilingVector = end-start;
		float distance = tilingVector.magnitude;
		Quaternion orientation = Quaternion.FromToRotation(Vector2.right, tilingVector);
		platform.transform.rotation = orientation;
		
		//nTiles that fit in distance, at least left and right (+1)
		int nTiles = (int)(distance / tileWidth);
		Debug.Log (nTiles);

		Vector2 position = start;
		generatedTiles.Add ((GameObject)GameObject.Instantiate (tileLeft, new Vector3 (position.x, position.y), orientation));

		for(int i = 1; i < nTiles; i++)
		{
			position = start + tilingVector.normalized*i*tileWidth;
			generatedTiles.Add ((GameObject)GameObject.Instantiate(tile, new Vector3(position.x, position.y) , orientation));
		}

		position = start + tilingVector.normalized*nTiles*tileWidth;
		generatedTiles.Add ((GameObject)GameObject.Instantiate (tileRight, new Vector3 (position.x, position.y), orientation));

		foreach (GameObject child in generatedTiles) {
			child.transform.parent = platform.transform;
		}
	}

	public void Remove()
	{
		GameObject.Destroy (platform, 5);
	}

}










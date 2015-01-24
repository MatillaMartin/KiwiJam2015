using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformFactory : MonoBehaviour {


	private bool m_bPlatformStarted = false;
	private Vector2 m_mousePosition;
	private Vector2 m_platformStart, m_platformEnd;
	
	[SerializeField] GameObject m_platformTile;

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
			m_platforms.Add(new Platform(m_platformTile, platformStart, platformEnd));
		}
	}
}

public class Platform
{
	Vector2 start, end;
	GameObject tile; //tile as prefab

	List<GameObject> tiles;
	
	public Platform(GameObject platformTile, Vector2 platformStart, Vector2 platformEnd)
	{
		this.tile = platformTile;
		this.start = platformStart;
		this.end = platformEnd;
		TileTiles();

	}

	void TileTiles()
	{
		Vector2 tilingVector = end-start;
		float distance = tilingVector.magnitude;
		Quaternion orientation = Quaternion.FromToRotation(Vector2.right, tilingVector);
		float width = tile.GetComponent<SpriteRenderer>().renderer.bounds.size.x;
		Debug.Log (distance);
		Debug.Log (width);
		for(int i = 0; i <= distance/width; i++)
		{
			Vector2 position = start + tilingVector.normalized*i*width;
			GameObject.Instantiate(tile, new Vector3(position.x, position.y) , orientation);
		}
	}


}










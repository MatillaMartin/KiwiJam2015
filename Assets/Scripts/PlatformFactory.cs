using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformFactory : MonoBehaviour {


	private bool m_bPlatformStarted = false;
	private Vector2 m_mousePosition;
	private Vector2 m_platformStart, m_platformEnd;

	[SerializeField] int m_platformMaxBlocks;
	int m_buildingBlocks;

	[SerializeField] GameObject m_platformTile;
	[SerializeField] GameObject m_platformTileLeft;
	[SerializeField] GameObject m_platformTileRight;

	private Platform m_buildingPlatform;
	private Platform m_lastPlatform;
	
	private List<Platform> m_platforms;
	private List<GameObject> m_tiles;
	
	void Start () {
		//load textures etc..

		//init platform list
		m_platforms = new List<Platform>();
		m_tiles = new List<GameObject> ();
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
		m_buildingPlatform = new Platform (m_platformTile, m_platformTileLeft, m_platformTileRight, m_platformMaxBlocks);
		m_platformStart = platformStart;
		m_bPlatformStarted = true;
		//start event to draw line etc (TODO:visuals)
	}

	void onDraggingPlatform(Vector2 platformStart, Vector2 platformTemporalEnd)
	{
//		Vector2 platformVector = platformTemporalEnd - platformStart;
//		float angle = Vector2.Angle(platformVector, Vector2.right);
//		Debug.Log(angle); //angle dodgy... [0 -> 180 -> 0]
		int nBlocksBuilding = m_buildingPlatform.DynamicConstruction (platformStart, platformTemporalEnd);
		nBlocksBuilding = Mathf.Min (nBlocksBuilding, m_tiles.Count);
		Debug.Log (m_tiles.Count);
		Debug.Log (nBlocksBuilding);

		int blocksNeeded = nBlocksBuilding + m_tiles.Count - m_platformMaxBlocks;

		m_buildingBlocks = 0;

		if(blocksNeeded > 0)
		{

			for (int i = 0; i < m_tiles.Count; i++) 
			{
				m_tiles[i].GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f);
			}
			for (int i = 0; i < blocksNeeded; i++) 
			{
				m_tiles[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			}
			m_buildingBlocks = blocksNeeded;
		}
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
			m_buildingPlatform.ConfirmPlatform();
			m_platforms.Add(m_buildingPlatform);
			Debug.Log("tiles before :" + m_tiles.Count.ToString());
			for(int i = 0; i < m_buildingBlocks; i++)
			{
				GameObject.Destroy(m_tiles[i]);
			}
			Debug.Log("tiles after :" + m_tiles.Count.ToString());

			m_tiles.RemoveRange(0,m_buildingBlocks);
			m_tiles.AddRange(m_buildingPlatform.getTiles());
			
		}
	}
}


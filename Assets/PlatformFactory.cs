using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformFactory : MonoBehaviour {


	private bool m_bPlatformStarted = false;
	private Vector2 m_mousePosition;
	private Vector2 m_platformStart, m_platformEnd;

	private List<Platform> m_platforms;
	
	void Start () {
		//load textures etc..

		//init platform list
		m_platforms = new List<Platform>();
	}

	void Update () {

		m_mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

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
			m_platforms.Add(new Platform(platformStart, platformEnd));
		}
	}
}

public class Platform
{
	Vector2 platformStart, platformEnd;

	public Platform(Vector2 platformStart, Vector2 platformEnd)
	{
		this.platformStart = platformStart;
		this.platformEnd = platformEnd;
	}
}

using UnityEngine;
using System.Collections;

public class ColliderGizmo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
		Gizmos.DrawCube(collider2D.bounds.center, collider2D.bounds.size);	
	}
}

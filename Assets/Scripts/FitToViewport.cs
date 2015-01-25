using UnityEngine;
using System.Collections;

public class FitToViewport : MonoBehaviour {
	public Vector2 positionOffsetNormalized = new Vector2(0.0f, 0.0f);
	public float offsetNormalized;
	public bool fitWidth = true;
	public bool fitHeight = true;
	public bool toCameraPosY = false;
	public bool toCameraPosX = true;
	
	// Use this for initialization
	void Start () {
		float viewPortWidth = 
			Camera.main.ViewportToWorldPoint(new Vector3( 1.0f, 1.0f, Camera.main.nearClipPlane)).x - 
			Camera.main.ViewportToWorldPoint(new Vector3( 0.0f, 0.0f, Camera.main.nearClipPlane)).x;
		float scaleWidth = viewPortWidth / renderer.bounds.size.x * (1.0f + offsetNormalized);
		
		
		float viewPortHeight = 
			Camera.main.ViewportToWorldPoint(new Vector3( 1.0f, 1.0f, Camera.main.nearClipPlane)).y - 
			Camera.main.ViewportToWorldPoint(new Vector3( 0.0f, 0.0f, Camera.main.nearClipPlane)).y;
		float scaleHeight = viewPortHeight / renderer.bounds.size.y * (1.0f + offsetNormalized);
			
		float aspectRatio = renderer.bounds.size.x / renderer.bounds.size.y;
		
		Vector3 newScale = Vector3.Scale(
			new Vector3(
				(fitWidth ? scaleWidth : scaleHeight * aspectRatio), 
				(fitHeight ? scaleHeight : 1.0f / aspectRatio * scaleWidth), 
				1.0f), 
			transform.localScale);
			
		transform.localScale = newScale;
		transform.position = new Vector3(
			(toCameraPosX ? Camera.main.transform.position.x : transform.position.x) + positionOffsetNormalized.x * viewPortWidth, 
			(toCameraPosY ? Camera.main.transform.position.y : transform.position.y) + positionOffsetNormalized.y * viewPortHeight, 
			transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

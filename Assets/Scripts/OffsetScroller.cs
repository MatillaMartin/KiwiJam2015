using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {
	public bool tileable;
	public float scrollSpeed;
	public FollowCamera paralax;
	
	private Vector2 savedOffset;
	public bool stopAtNextLoop;
	private bool stopped = false;
	
	void Start () {
		// This makes sure we do not kill the original texture asset
		savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex");
		Debug.Log(savedOffset);
		// If we decide it's not tileable we just omit the scrollSpeed
		if (!tileable)
			scrollSpeed = 0.0f;
	}
	
	void Update () {
		// Update texture
		
		if(!stopped)
		{
			float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
			Vector2 offset = new Vector2 (savedOffset.x, y);
			renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
		}
		if (stopAtNextLoop) {
			if(	Mathf.Abs(renderer.sharedMaterial.GetTextureOffset("_MainTex").y - savedOffset.y) < 0.005f )
			{
				paralax.follow = false;
				renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
				stopAtNextLoop = false;
				Camera.main.GetComponent<CameraMovement>().cameraSpeed = scrollSpeed * 10.0f;
				scrollSpeed = 0.0f;
				stopped = true;
			}
		}
	}
	
	void OnDisable () {
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}

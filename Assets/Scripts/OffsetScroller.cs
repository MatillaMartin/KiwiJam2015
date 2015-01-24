using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {
	public bool tileable;
	public float scrollSpeed;

	private Vector2 savedOffset;
	public bool stopAtNextLoop;
	
	void Start () {
		// This makes sure we do not kill the original texture asset
		savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex");

		// If we decide it's not tileable we just omit the scrollSpeed
		if (!tileable)
			scrollSpeed = 0.0f;
	}
	
	void Update () {
		// Update texture
		float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (savedOffset.x, y);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);

		if (stopAtNextLoop) {
			if(	Mathf.Abs(renderer.sharedMaterial.GetTextureOffset("_MainTex").y) < Mathf.Abs(scrollSpeed))
			{
				renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
				stopAtNextLoop = false;
				scrollSpeed = 0.0f;
			}
		}
	}
	
	void OnDisable () {
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}

using UnityEngine;
using System.Collections;

public class BackgroundTint : MonoBehaviour {
	public Color[] colorPalette;
	public float lerpTime = 2.0f;
	private float timer = 2.0f;
	private Color fromColor;
	private Color targetColor;
	
	// Use this for initialization
	void Start () {
		fromColor = Color.white;
		targetColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer < 0.0f)
		{
			timer = lerpTime;
			targetColor = colorPalette[Random.Range(0, colorPalette.Length - 1)];
			fromColor = renderer.material.color;
		}
		else
		{
			timer -= Time.deltaTime;
			renderer.material.color = Color.Lerp(fromColor, targetColor, (lerpTime - timer)/lerpTime);
		}
	}
}

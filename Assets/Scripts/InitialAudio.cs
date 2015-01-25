using UnityEngine;
using System.Collections;

public class InitialAudio : MonoBehaviour {
	public AudioClip clip;
	float timer = 2.0f;
	int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timer < 0.0f)
		{
			
			if(count == 0)
			{
				Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
				count++;
			}
			
			if(count == 1 && !Camera.main.GetComponent<AudioSource>().isPlaying)
				Application.LoadLevel("Kiwi_Genocide");
		}
		else
			timer -= Time.deltaTime;
	}
}

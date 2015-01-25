using UnityEngine;
using System.Collections;

public class KiwiAudioControl : MonoBehaviour {
	public AudioClip[] audioSamples;
	public AudioClip[] kiwiDiedSamples; // u, w, 1, 2, 3
	public AudioSource audioSource;
	public Vector2 audioPlayTimeRange = new Vector2(7.0f, 10.0f);
	private float audioPlayTimer;
	public float audioVolume = 0.5f;
	
	// Use this for initialization
	void Start () {
		audioPlayTimer = Random.Range(audioPlayTimeRange.x, audioPlayTimeRange.y);
		audioSource = Camera.main.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(audioPlayTimer < 0.0f)
		{
			playRandomClip();
			audioPlayTimer = Random.Range(audioPlayTimeRange.x, audioPlayTimeRange.y);
		} 
		else 
		{
			audioPlayTimer -= Time.deltaTime;
		}
	}
	
	void playRandomClip()
	{
		if(audioSamples.Length > 0){
			AudioClip clip = audioSamples[Random.Range(0, audioSamples.Length-1)];
			audioSource.PlayOneShot(clip, audioVolume);
		}
	}

	void OnDestroy()
	{
		if (kiwiDiedSamples.Length > 0.0f)
		{
			AudioClip clip = kiwiDiedSamples[Random.Range(0, kiwiDiedSamples.Length)];
			audioSource.PlayOneShot(clip, audioVolume);
		}
	}
}

using UnityEngine;
using System.Collections;

public class DieAfterSeconds : MonoBehaviour {
	public float seconds = 5.0f;
	public AudioClip fallingAudioClip;
	public AudioClip impactAudioClip;
	public AudioClip onWaterEnterAudioClip;
	public AudioSource audioSource;
	
	// Use this for initialization
	void Start () {
		audioSource = Camera.main.GetComponent<AudioSource>();
	}
	
	void Awake()
	{
		Destroy(gameObject, seconds);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Water" && onWaterEnterAudioClip)
		{
			audioSource.PlayOneShot(onWaterEnterAudioClip);
		}
		else 
		{
			if (coll.gameObject.tag == "Kiwi")
			{
				coll.gameObject.SendMessage("Die");
			}

			if (impactAudioClip)
			{
				audioSource.PlayOneShot(impactAudioClip);
			}

			Destroy(gameObject);
		}
	}
}

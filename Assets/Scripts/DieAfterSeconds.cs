using UnityEngine;
using System.Collections;

public class DieAfterSeconds : MonoBehaviour {
	public float seconds = 5.0f;
	public AudioClip fallingAudioClip;
	public AudioClip impactAudioClip;
	public AudioClip onWaterEnterAudioClip;

	private AudioSource audioSource;
	private float currentTime;
	
	void Awake()
	{
		//audioSource = Camera.main.GetComponent<AudioSource>();
		audioSource = GetComponent<AudioSource>();
		currentTime = 0.0f;

		if (fallingAudioClip)
		{
			audioSource.PlayOneShot(fallingAudioClip);
		}
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		if (currentTime >= seconds)
		{
			audioSource.Stop();
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		audioSource.Stop ();

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

	void OnTriggerEnter2D(Collider2D other) 
	{
		audioSource.Stop();

		if (other.gameObject.name == "WaterCollider" && onWaterEnterAudioClip)
		{
			audioSource.PlayOneShot(onWaterEnterAudioClip);
		}
	}
}

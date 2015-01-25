using UnityEngine;
using System.Collections;

public class KiwiSpawner : MonoBehaviour {

	[SerializeField] GameObject m_kiwi_prefab;
	[SerializeField] float m_minSpeed;
	[SerializeField] float m_maxSpeed;
	[SerializeField] float m_spawnTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnKiwi ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator SpawnKiwi()
	{
		while(true)
		{
			float speed = Random.Range(m_minSpeed, m_maxSpeed);
			SpawnKiwi(speed);
			yield return new WaitForSeconds(m_spawnTime);
		}
	}
	void SpawnKiwi(float speed)
	{
		GameObject kiwi = (GameObject)GameObject.Instantiate (m_kiwi_prefab, this.transform.position, Quaternion.identity	);
		kiwi.GetComponent<KiwiBehaviour> ().velocityX = speed;
	}
}

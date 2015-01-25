using UnityEngine;
using System.Collections;

public class Shark : MonoBehaviour {
	public float jumpForce = 500.0f;
	Animator anim;
	public Vector2 attackTimerRange = new Vector2 (5.0f, 10.0f);
	private float attackTimer = 5.0f;
	public GameObject sharkSpawn;
	private Vector2 spawnRange;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		spawnRange = new Vector2(sharkSpawn.collider2D.bounds.min.x, sharkSpawn.collider2D.bounds.max.x);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (attackTimer <= 0.0f) {
			attackTimer = Random.Range (attackTimerRange.x, attackTimerRange.y);

			
			sharkAttack();
		} else {
			attackTimer -= Time.deltaTime;	
		}
	}

	void sharkAttack(){
		Vector3 newPosition = new Vector3(Random.Range(spawnRange.x, spawnRange.y), sharkSpawn.transform.position.y, transform.position.z);
		transform.position = newPosition;
		rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		rigidbody2D.AddForce(new Vector2(0, jumpForce));
		anim.SetTrigger ("Attack");
	}

	// Update is called once per frame
	void FixedUpdate () {
		//grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		//anim.SetBool ("Ground", grounded);
		//anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		
		anim.SetFloat ("VerticalSpeed", rigidbody2D.velocity.y);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(sharkIsRising())
		{
			Debug.Log("other.name = " + other.name);
			other.SendMessage("Die");
		}
	}
	
	
	bool sharkIsRising()
	{
		return rigidbody2D.velocity.y > 0.0f;
	}
	
}

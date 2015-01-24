using UnityEngine;
using System.Collections;

public class Shark : MonoBehaviour {
	public float jumpForce = 500.0f;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			sharkAttack();
		}
	}

	void sharkAttack(){
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
}

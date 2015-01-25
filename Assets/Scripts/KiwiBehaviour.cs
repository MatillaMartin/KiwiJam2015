using UnityEngine;
using System.Collections;

public class KiwiBehaviour : MonoBehaviour
{
   // public Rigidbody2D KiwiRigidbody;
    public float velocityX = 3.0f;
    private bool facingLeft = true;

	private bool jumping = false;
	private bool mustJump = false;
	private Vector2 jumpDestination; 
	private Animator anim;

	// Gameloop functions

    void Start()
    {
   		anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		if (!mustJump && !jumping)
		{
	        float move = facingLeft ? -1.0f : 1.0f;
			rigidbody2D.velocity = new Vector2(move * velocityX, rigidbody2D.velocity.y);
	    }
		else if (!jumping)
		{
			float deltaX = jumpDestination.x - rigidbody2D.transform.position.x;
			float finalTime = 1.0f;
			float velocityX = deltaX * finalTime;

			float velocityY = ( 2.0f * (jumpDestination.y - rigidbody2D.transform.position.y + collider2D.bounds.size.y * 0.5f) - (Physics.gravity.y * finalTime) ) / 2.0f;

			rigidbody2D.velocity = new Vector2(velocityX, velocityY);

			mustJump = false;
			setJumping(true);
		}
		
		if(rigidbody2D.velocity.x > 0.0f && facingLeft)
			Flip ();
		else if (rigidbody2D.velocity.x < 0.0f && !facingLeft)
			Flip();
			
		anim.SetFloat("VerticalVelocity", rigidbody2D.velocity.y);
	}
	
	
	void Flip()
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}

    void Update()
	{
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (!jumping)
		{
			if (other.tag == "PlatformLimit")
			{
				Flip();
			}
			else if (other.tag == "JumpTrigger")
			{
				jumpDestination = new Vector2(other.transform.parent.position.x, other.transform.parent.position.y);
				mustJump = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.collider.tag == "Platform")
		{
			setJumping(false);
		}
	}
	
	void setJumping(bool isJumping)
	{
		jumping = isJumping;
		anim.SetBool("Jumping", jumping);
	}

	// Utility functions

}

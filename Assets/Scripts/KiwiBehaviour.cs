using UnityEngine;
using System.Collections;

public class KiwiBehaviour : MonoBehaviour
{
    public Rigidbody2D KiwiRigidbody;
    public float velocityX = 3.0f;

	private bool facingRight = true;
	private bool jumping = false;

	// Gameloop functions

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
	{
        float move = facingRight ? 1.0f : -1.0f;
		rigidbody2D.velocity = new Vector2(move * velocityX, rigidbody2D.velocity.y);
    }

    void Update()
    {
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "PlatformLimit" && !jumping)
		{
			Flip();
		}
		else if (other.tag == "JumpTrigger")
		{
			float deltaX = Mathf.Abs(other.transform.parent.transform.position.x - rigidbody2D.transform.position.x);
			float finalTime = deltaX / velocityX;

			float velocityY = ( (-Physics.gravity.y / 2.0f * finalTime * finalTime) - (2.0f * rigidbody2D.transform.position.y) + (2.0f * other.transform.parent.transform.position.y) ) /
								 2.0f * finalTime;

			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, velocityY);

			jumping = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.collider.tag == "Platform")
		{
			jumping = false;
		}
	}

	// Utility functions

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

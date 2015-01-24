using UnityEngine;
using System.Collections;

public class KiwiBehaviour : MonoBehaviour
{
    public Rigidbody2D KiwiRigidbody;
    public float maxSpeed = 10.0f;

	private enum EKiwiState{KS_Walking}
	private EKiwiState CurrentKiwiState;

	private bool facingRight = true;

	// Gameloop functions

    void Start()
    {
		CurrentKiwiState = EKiwiState.KS_Walking;
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		if (CurrentKiwiState == EKiwiState.KS_Walking)
		{
	        float move = facingRight ? 1.0f : -1.0f;

	        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		}
    }

    void Update()
    {

    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "PlatformLimit")
		{
			Flip();
		}
		else if (other.tag == "JumpTrigger")
		{
			KiwiRigidbody.AddForce(new Vector2(0.0f, 500.0f));
		}
	}

	// Utility functions

	private void GoToState(EKiwiState NewKiwiState)
	{
		CurrentKiwiState = NewKiwiState;
	}

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

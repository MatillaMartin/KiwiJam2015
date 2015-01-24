using UnityEngine;
using System.Collections;

public class KiwiBehaviour : MonoBehaviour
{
    public Rigidbody2D KiwiRigidbody;
    public float velocityX = 3.0f;

	private bool facingRight = true;
	private bool jumping = false;
	private bool mustJump = false;
	private Vector2 jumpDestination; 
	[SerializeField] float v; // velocity
	// Gameloop functions

	private GameObject m_current_platform;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		if (!mustJump && !jumping)
		{
	        float move = facingRight ? 1.0f : -1.0f;
			rigidbody2D.velocity = new Vector2(move * velocityX, rigidbody2D.velocity.y);
	    }
		else if (!jumping)
		{
			float deltaX = jumpDestination.x - rigidbody2D.transform.position.x;
			float deltaY = jumpDestination.y - (rigidbody2D.transform.position.y - collider2D.bounds.size.y/2);
			float g = -Physics.gravity.y;
			float x = deltaX; // target x
			float y = deltaY; // target y
			float o = float.NaN;
			float goodO = 0.0f;
			float goodV = 0.0f;
			bool bGotNonNan = false;
			int iterations = 0;
			bool iterate = true;
			if(iterate)
			{
				do
				{
					float s = (v * v * v * v) - g * (g * (x * x) + 2.0f * y * (v * v)); //substitution
					o = Mathf.Atan((((v * v) + Mathf.Sqrt(s)) / (g * x))); // launch angle
					//Debug.Log (v);
					iterations++;
					//Debug.Log (o);
					if(!float.IsNaN(o))
					{
						goodO = o;
						goodV = v;
						bGotNonNan = true;

					}
					v -= 0.5f;
				} while(!float.IsNaN(o) && iterations < 50 && v > 0);
				if(!bGotNonNan)
				{
					Debug.Log ("Cannot jump! need more velocity");
					mustJump = false;
					return;
				}
				else{
					o = goodO;
					v = goodV;
				}
			}
			else
			{
				float s = (v * v * v * v) - g * (g * (x * x) + 2.0f * y * (v * v)); //substitution
				o = Mathf.Atan((((v * v) + Mathf.Sqrt(s)) / (g * x))); // launch angle
			}
				
			float velocityX = (v*Mathf.Cos(o));
			float velocityY = (v*Mathf.Sin(o));

			rigidbody2D.velocity = new Vector2(velocityX, velocityY);

			mustJump = false;
			jumping = true;
		}
	}

    void Update()
	{
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		//Jumping waypoints
		if (other.gameObject.layer == 8) 
		{
			Debug.Log(other.transform.root.gameObject);
			Debug.Log (m_current_platform);
			if(other.transform.root.gameObject.Equals(m_current_platform))
			{
				return;
			}
			else
			{
				if (!jumping)
				{
					jumpDestination = other.transform.parent.position;
					mustJump = true;
				}
			}
		}
		if (!jumping)
		{
			if (other.tag == "PlatformLimit")
			{
				Flip();
			}
			else if (other.tag == "JumpTrigger")
			{
				jumpDestination = other.transform.parent.position;
				mustJump = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		m_current_platform =  collider2D.transform.root.gameObject;
		if (collision.collider.tag == "Platform")
		{
			jumping = false;
			mustJump = false;
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

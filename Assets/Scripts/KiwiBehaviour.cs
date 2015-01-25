using UnityEngine;
using System.Collections;

public class KiwiBehaviour : MonoBehaviour
{
    public Rigidbody2D KiwiRigidbody;
    public float velocityX = 3.0f;
	public float lastFlipTime = 0.0f;
	private bool facingRight = false;
	private bool jumping = false;
	private bool mustJump = false;
	private Vector2 jumpDestination; 
	[SerializeField] float m_maxSpeed; // velocity
	// Gameloop functions

	private GameObject m_current_platform;

	private bool bCheckStayTrigger = false;

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
			//Debug.Log ("Jumping!");
			float deltaX = jumpDestination.x - rigidbody2D.transform.position.x;
			float deltaY = jumpDestination.y - (rigidbody2D.transform.position.y - collider2D.bounds.size.y/2);
			//Debug.Log(collider2D.bounds.size.y/2);
			float g = -Physics.gravity.y;
			float x = deltaX; // target x
			float y = deltaY; // target y
			float v = m_maxSpeed;
			float o = float.NaN;
			float goodO = 0.0f;
			float goodV = 0.0f;
			bool bGotNonNan = false;
			int iterations = 0;
			bool iterate = true;
			if(iterate)
			{
				//Debug.Log ("Iterating to find least velocity");
				do
				{
					float s = (v * v * v * v) - g * (g * (x * x) + 2.0f * y * (v * v)); //substitution
					o = Mathf.Atan((((v * v) + Mathf.Sqrt(s)) / (g * x))); // launch angle
					////Debug.Log (v);
					iterations++;
					////Debug.Log (o);
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
					//Debug.Log ("Cannot jump! need more velocity");
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
				
			//Debug.Log(deltaX + " : " +deltaY);
			float velocityX = (Mathf.Sign(deltaX)*Mathf.Abs(v*Mathf.Cos(o)));
			float velocityY = (Mathf.Sign(deltaY)*Mathf.Abs(v*Mathf.Sin(o)));
			//Debug.Log ("Set velocity yo rigidbody!");
			//Debug.Log ("velocity: " + new Vector2(velocityX, velocityY));
			rigidbody2D.velocity = new Vector2(velocityX, velocityY);

			mustJump = false;
			jumping = true;
		}
	}

    void Update()
	{
    }

	void shouldJump(Collider2D other)
	{
		//Jumping waypoints
		if (other.gameObject.layer == 8 && other.tag == "JumpTrigger") 
		{
			//Debug.Log("same layer and tag");
			//Debug.Log(other.transform.root.gameObject);
			//Debug.Log(m_current_platform);

			if(other.transform.root.gameObject == m_current_platform)
			{
				return;
			}
			else
			{
				if (!jumping)
				{
					if(this.transform.position.y > other.transform.position.y)
					{
						return;
					}
					if((facingRight && this.transform.position.x > other.transform.position.x) ||
					   (!facingRight && this.transform.position.x < other.transform.position.x))
					{
						return;
					}

					Vector3 triggerNormal = other.transform.up;
					//Debug.Log ("Checking normals: " + triggerNormal);
					float dotProduct = Vector2.Dot (this.transform.right, triggerNormal);
					//Debug.Log (dotProduct);
					if((facingRight && dotProduct > 0.70716f) || (!facingRight && dotProduct < -0.70716f))
					{


						jumpDestination = other.transform.parent.position;
						mustJump = true;						
					}
				}
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		shouldJump (other);
		if(other.gameObject.layer != 8)
		{
			if (!jumping)
			{
				if (other.tag == "PlatformLimit")
				{
					Flip();
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		//may slow down app
		if(bCheckStayTrigger)
		{
			//Debug.Log ("Checking stay");
			if(other.transform.root.gameObject.Equals(m_current_platform))
			{
				return;
			}
			else
			{
				bCheckStayTrigger = false;
				shouldJump (other);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		m_current_platform = other.transform.root.gameObject;
		if (other.collider.tag == "Platform")
		{
			jumping = false;
			mustJump = false;
			if(Mathf.Abs(Vector2.Dot (other.transform.up, transform.up)) < 0.2f)
			{
				//Debug.Log ("flipping!!!! : " + Mathf.Abs(Vector2.Dot (other.transform.up, transform.up)));
				float timeFilter = 0.2f;
				if(Time.timeSinceLevelLoad > lastFlipTime + timeFilter)
				{
					lastFlipTime = Time.timeSinceLevelLoad;
					Flip ();
				}
			}
		}
	}

	// Utility functions

    private void Flip()
    {
        facingRight = !facingRight;
		bCheckStayTrigger = true;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

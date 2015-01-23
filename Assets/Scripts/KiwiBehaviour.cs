using UnityEngine;
using System.Collections;

public class KiwiBehaviour : MonoBehaviour
{
    public Rigidbody2D KiwiRigidbody;
    public float maxSpeed = 10.0f;
    public float maxDestinationOffset = 3.0f;
    public float minDestinationOffset = 0.5f;
    bool facingRight = true;

    private float nextDestination;

    // Use this for initialization
    void Start()
    {
        CalculateNextDestinaton();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = facingRight ? 1 : -1;

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
    }

    void Update()
    {
        if (MustTurn())
        {
            CalculateNextDestinaton();
            Flip();
        }
    }

    void CalculateNextDestinaton()
    {
        nextDestination = KiwiRigidbody.transform.position.x + Random.Range(-maxDestinationOffset, maxDestinationOffset);

        if (nextDestination < 0)
        {
            nextDestination = Mathf.Min(minDestinationOffset, nextDestination);
        }
        else
        {
            nextDestination = Mathf.Max(minDestinationOffset, nextDestination);
        }
    }

    bool MustTurn()
    {
        bool HasToTurnLeft, HasToTurnRight;

        HasToTurnLeft = facingRight && KiwiRigidbody.transform.position.x >= nextDestination;
        HasToTurnRight = !facingRight && KiwiRigidbody.transform.position.x <= nextDestination;

        return HasToTurnLeft || HasToTurnRight;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}

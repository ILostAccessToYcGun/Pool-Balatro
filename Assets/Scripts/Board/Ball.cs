using UnityEngine;
using static UnityEngine.Rendering.STP;

public class Ball : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    [Space]
    [Header("Attributes")]
    [Range(0.0f, 5.0f)]
    [SerializeField] float bounciness;
    [Range(0.0f, 5.0f)]
    [SerializeField] float weight;

    public Vector2 testVel;
    private Vector2 previousVel;

    private void Start()
    {
        rb.linearVelocity = testVel;
    }

    private void FixedUpdate()
    {
        Debug.Log(rb.linearVelocity.magnitude);
        rb.linearVelocity *= (1 - (weight * Time.deltaTime));
        previousVel = rb.linearVelocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if this hits a wall, bounce off it with a bounciness var
        //w = v – (2 * (v . n) * n)
        Vector3 normal;
        Vector2 bounceVel;

        //ContactPoint2D[] contactPoint2Ds = collision.contacts;
        //foreach (ContactPoint2D contacts in contactPoint2Ds)
        //{
        //    if (contacts.rigidbody.gameObject.CompareTag("Wall"))
        //    {
        //        normal = contacts.normal;
        //    }
        //}

        normal = collision.GetContact(0).normal;

        float dot = Vector2.Dot(previousVel, normal);

        bounceVel = previousVel - (2.0f * dot * (Vector2)normal);
        bounceVel.Normalize();
        bounceVel = bounceVel * previousVel.magnitude * bounciness;

        //i need to incorporate weight into here

        //also there is a ratio based on the dot product of how much energy is given

        //if we hit a ball, give half of the speed to the ball
        Ball tryGet = collision.gameObject.GetComponent<Ball>();
        if (tryGet != null)
        {
            Rigidbody2D rb2 = collision.gameObject.GetComponent<Rigidbody2D>();
            bounceVel *= 0.25f * bounciness;
            rb2.linearVelocity -= bounceVel;
        }
        rb.linearVelocity += bounceVel;


        Debug.Log(rb.linearVelocity);
        Debug.Log(normal);
        Debug.Log(dot);
        //get the surface normal of the wall
        //Vector3 normal = collision.GetContact(0).normal;
    }
}

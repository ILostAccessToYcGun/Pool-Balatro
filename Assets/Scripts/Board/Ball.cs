using UnityEngine;
using static UnityEngine.Rendering.STP;

public class Ball : MonoBehaviour, IBall
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D col;
    [Space]
    [Header("Attributes")]
    [SerializeField] BallType type;
    [SerializeField] int value;
    [Range(0.0f, 5.0f)]
    [SerializeField] float bounciness;
    [Range(0.0f, 5.0f)]
    [SerializeField] float weight;

    public Vector2 testVel;
    private Vector2 previousVel;

    public BallType Type => type;

    private void Start()
    {
        rb.linearVelocity = testVel;
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.linearVelocity.magnitude);
        rb.linearVelocity *= (1 - (weight * Time.deltaTime));
        previousVel = rb.linearVelocity;
    }

    public int Value()
    {
        return value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 normal;
        Vector2 bounceVel;

        normal = collision.GetContact(0).normal;

        float dot = Vector2.Dot(previousVel, normal);

        bounceVel = previousVel - (2.0f * dot * (Vector2)normal);
        bounceVel = bounceVel.normalized * previousVel.magnitude * bounciness;

        //if we hit a ball, give half of the speed to the ball
        Ball tryGet = collision.gameObject.GetComponent<Ball>();
        if (tryGet != null)
        {
            Rigidbody2D rb2 = collision.gameObject.GetComponent<Rigidbody2D>();
            bounceVel *= 0.4f * bounciness;
            rb2.linearVelocity -= bounceVel;
        }
        rb.linearVelocity += bounceVel;

        ScoreManager.instance.blueScore += value;

    }

    public void OnCollide()
    {
        //hi
    }
}

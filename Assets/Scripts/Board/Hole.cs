using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Hole : MonoBehaviour
{
    [SerializeField] Collider2D col;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //also maybe check if the velocity is not zoom cuz it would skip over the hole
        //but for now nah

        //if the center of a ball is over a hole
        Ball tryGet = collision.gameObject.GetComponent<Ball>();
        if (tryGet != null)
        {
            //velocity checking?
            //Rigidbody2D rb2 = collision.gameObject.GetComponent<Rigidbody2D>();
            if (col.bounds.Contains(tryGet.gameObject.transform.position)) //not sure if this is the right collider
            {
                Destroy(tryGet.gameObject);
                Debug.Log("Ball is sunk");

                //Incorporate the score manager here cuz sinking a ball gives red score
                ScoreManager.instance.redScore += tryGet.Value();
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoHitPlayer : MonoBehaviour
{
    private Enemy_Behaviour enemyParent;
    public GameObject Target;

    public float DmgOfTheEnemey = 0.2f;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !collision.gameObject.GetComponent<ObstacleDefender>().isDead)
        {
           Target = collision.gameObject;

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("the player is triggered");

            Target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
        {

            //enemyParent.SelectTarget();
        }
    }

    public void CheckIfTargetIsNull(GameObject t)
    {
        if(t.GetComponent<MovePlayer>() != null && t != null)
        {
            Debug.Log(t.gameObject.name);
            t.GetComponent<MovePlayer>().TakeDmg(DmgOfTheEnemey);
            Debug.Log("the player is hitted");


        }
        else if (t.GetComponent<ObstacleDefender>() != null && t != null)
        {
            t.GetComponent<ObstacleDefender>().TakeDmg(0.2f);
            Debug.Log("the Obstacle is hitted");

        }
        else
        {
           
            //enemyParent.SelectTarget();

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredArea : MonoBehaviour
{

    public Enemy_Behaviour enemyParent;
    // Start is called before the first frame update
    private void Start()
    {
        enemyParent = GetComponentInParent<Enemy_Behaviour>();
 
            enemyParent.attackMode = false;
            enemyParent.inRange = false;
        enemyParent.SetTargetThatClosedToPlayer();


    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Obstacle"))
       {
          enemyParent.target = collision.transform;
          enemyParent.inRange = true;
          enemyParent.HotZone.SetActive(true);
          gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {

            enemyParent.target = collision.transform;
            enemyParent.inRange = true;
            enemyParent.HotZone.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyParent.SelectTarget();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            enemyParent.SelectTarget();
        }
        }

}

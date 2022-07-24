using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private Enemy_Behaviour enemyParent;
    private bool inRange;
    private Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_Behaviour>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        //if (!inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("AttackAnim"))
        //{
        //    enemyParent.Flip();
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    inRange = true;
        //}
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            enemyParent.inRange = true;
        }

        if (collision.gameObject.CompareTag("Obstacle") && !collision.gameObject.GetComponent<ObstacleDefender>().isDead)
        {
            inRange = true;
            enemyParent.inRange = true;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.GetComponent<ObstacleDefender>() != null)
        //{
        //    if (collision.gameObject.GetComponent<ObstacleDefender>().isDead && collision.gameObject.GetComponent<ObstacleDefender>().isTrigger == false)
        //    {
        //        inRange = false;
        //        gameObject.SetActive(false);
        //        enemyParent.triggerArea.SetActive(true);
        //        enemyParent.inRange = false;
        //        enemyParent.SelectTarget();
        //        collision.gameObject.GetComponent<ObstacleDefender>().isTrigger = true;
        //    }

        //}
        //else
        //{
        //    SetTarget();
        //}

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SetTarget();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            SetTarget();
        }
        else
        {
            
        }

    }

    public void SetTarget()
    {
        inRange = true;
        gameObject.SetActive(false);
        enemyParent.triggerArea.SetActive(true);

        //enemyParent.inRange = false;
        enemyParent.inRange = true;
        enemyParent.SelectTarget();
    }
}

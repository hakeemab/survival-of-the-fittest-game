using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleDefender : MonoBehaviour
{
    public float health;
    public Image imgHealth;
    public bool isTrigger;
    public int currentCostToFillObstacle;

    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isTrigger = false;
        isDead = false;
        currentCostToFillObstacle = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDmg(float dmg)
    {
        if(dmg <= health)
        {
            health -= dmg;
            imgHealth.fillAmount = health;
            if (0 >= health)
            {
                isDead = true;
                this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                Destroy(this.gameObject);


            }

        }
        else
        {
            isDead = true;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(this.gameObject);


        }
    }
    
    public void ReviveObstacle(int cost,GameObject playerObj)
    {
        if(currentCostToFillObstacle <= cost)
        {
            isDead = false;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        }
    }    
}

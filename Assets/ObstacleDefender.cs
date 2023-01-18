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
    public Color getCurMat;
    public Color ReplaceInstanceMat;
    public float MaxHealth;
    public Text uiFixWallText;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isTrigger = false;
        isDead = false;
        health = MaxHealth;
        getCurMat = GetComponent<SpriteRenderer>().color;
        //GetComponent<SpriteRenderer>().material = Instantiate<Material>(getCurMat);
        ReplaceInstanceMat = GetComponent<SpriteRenderer>().color;
        currentCostToFillObstacle = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDmg(float dmg)
    {
        if(0 < health)
        {
            health -= dmg;
            imgHealth.fillAmount = health;
            if (0 >= health)
            {
                isDead = true;
                this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                isTrigger = true;
                 ReplaceInstanceMat.a = 0.1f;
                GetComponent<SpriteRenderer>().color = ReplaceInstanceMat;
                health = 0;
                Debug.Log("Dead");
                //Destroy(this.gameObject);


            }

        }
        else
        {
            isDead = true;
            health = 0;

            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            isTrigger = true;
            ReplaceInstanceMat.a = 0.1f;
            GetComponent<SpriteRenderer>().color = ReplaceInstanceMat;

            Debug.Log("Dead2");

            //Destroy(this.gameObject);


        }
    }
    
    public void ReviveObstacle()
    {
            health = MaxHealth;
            imgHealth.fillAmount = health / MaxHealth;
            isDead = false;
            isTrigger = false;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            ReplaceInstanceMat.a = 1f;
            GetComponent<SpriteRenderer>().color = ReplaceInstanceMat;


        
    }    
}

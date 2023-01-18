using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleReviver : MonoBehaviour
{
    [SerializeField]ObstacleDefender obsDefender;

    //Canvas
    // Start is called before the first frame update
    void Start()
    {
        obsDefender = GetComponentInParent<ObstacleDefender>();
    }

 
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.transform.gameObject.tag == "Player")
        {

            
            if (obsDefender.health <= 0)
            {
                obsDefender.uiFixWallText.transform.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.V))
                {
                    //Revive
                    obsDefender.ReviveObstacle();
                    obsDefender.uiFixWallText.transform.gameObject.SetActive(false);

                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {


            if (obsDefender.health <= 0)
            {
                obsDefender.uiFixWallText.transform.gameObject.SetActive(false);
   
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float BulletDmg;
    public GameObject PsForDestroyBullet;
    public GameObject psForHitEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,6f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if(collision.gameObject.tag == "Ground")
        {
            ContactPoint2D contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
            Vector2 pos = contact.point;
             
            //Script Down Life To The Enemy
            GameObject ps = Instantiate(PsForDestroyBullet, pos, rot);
            Destroy(ps, 2f);
            Destroy(this.gameObject );

        }
        if (collision.gameObject.tag == "Enemy")
        {
            ContactPoint2D contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
            Vector2 pos = contact.point;

            //Script Down Life To The Enemy
            GameObject ps = Instantiate(psForHitEnemy, pos, rot);
            collision.gameObject.GetComponent<Enemy_Behaviour>().takeDmg(BulletDmg);
            Destroy(ps, 2f);
            Destroy(this.gameObject);

        }
        else
        {
           

            ContactPoint2D contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
            Vector2 pos = contact.point;
            GameObject ps = Instantiate(PsForDestroyBullet, pos, rot);
            Destroy(ps, 2f);

            Destroy(this.gameObject );

        }

    }

 
}

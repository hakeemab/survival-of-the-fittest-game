using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScriptBullet : MonoBehaviour
{
    public float Damage;
    Rigidbody2D rb;

    public GameObject PsForDestroyBullet;
    public GameObject psForHitEnemy;

    //Explosion Properties
    Collider2D[] inExplosionRads = null; //2d
    [SerializeField] private float ExplosionForceMulti = 5f;
    [SerializeField] private float ExplosionRadius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        Invoke("Explode", 5f);
    }




    public void Explode()
    {
        ////
        ///
        /*

        if (collision.gameObject.tag == "Enemy")
        {


            //Script Down Life To The Enemy
            GameObject ps = Instantiate(psForHitEnemy, pos, rot);
            collision.gameObject.GetComponent<Enemy_Behaviour>().takeDmg(BulletDmg);
            Destroy(ps, 2f);


        }*/
        ///

        inExplosionRads = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        GameObject ps1 = Instantiate(PsForDestroyBullet, transform.position, Quaternion.identity);
        Destroy(ps1, 2f);

        foreach (Collider2D co in inExplosionRads)
        {
            Rigidbody2D co_rig = co.GetComponentInParent<Rigidbody2D>();
            if(co_rig != null)
            {
                Vector2 distanceVec = co.transform.position - transform.position;
                if(distanceVec.magnitude > 0)
                {
                    //float explosionForce = ExplosionForceMulti / distanceVec.magnitude;
                    float explosionForce = ExplosionForceMulti * co_rig.mass ;

                    co_rig.AddForce(distanceVec.normalized * explosionForce);
                    Debug.Log(co_rig.transform.gameObject.name);
                }
                if (co_rig.gameObject.tag == "Enemy")
                {
                    float massSet = co_rig.mass;
                    //co_rig.mass = 0.1f;
                    //Script Down Life To The Enemy
                    GameObject ps = Instantiate(psForHitEnemy, co_rig.transform.position, Quaternion.identity);
                    co_rig.gameObject.GetComponent<Enemy_Behaviour>().takeDmg(0.2f);
                    Destroy(ps, 2f);
                    co_rig.mass = massSet;


                }
            }
        }
        Destroy(this.gameObject);

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}

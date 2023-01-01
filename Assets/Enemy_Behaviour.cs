using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{

    public float attackDistance;
    public float MoveSpeed;
    public float Timer;

    public Transform leftLimit;
    public Transform rightLimit;
    public Transform target;
    public bool inRange;
    public GameObject HotZone;
    public GameObject triggerArea;

    private Animator anim;
    public float distance;
    public bool attackMode;
    public bool cooling;
    public float intTimer;

    public bool IsFlipped;
    public LayerMask layerMK;
    Rigidbody2D rb;
    public GameObject rayCastPos;

    public GameObject hitBox;

    public float dis1 = 0.5f;
    public float dis2 = 1.0f;

    public float HealthEnemy;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        IsFlipped = false;
        intTimer = Timer;
        anim = GetComponent<Animator>();


    }
    private void Start()
    {
        SetTargetThatClosedToPlayer();
    }
    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            CALLALLMETHODS();
        }
        else
        {

            DetectPlayer();
        }
    }
    public void CALLALLMETHODS()
    {
        if (!attackMode)
        {

            Move();
            Flip();
            MakeAJump();

        }

        if (InideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("AttackAnim"))
        {
            SelectTarget();
        }
        if (inRange)
        {


            SelectTarget();
            EnemyLogic();


        }
    }
    private void FixedUpdate()
    {
          
    }
    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {

        anim.SetBool("canWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("AttackAnim"))
        {
            Vector2 TargetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, MoveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        Timer = intTimer;
        attackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            Timer = intTimer;
        }
    }
    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }


    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InideOfLimits()
    {
        return transform.position.x > leftLimit.transform.position.x && transform.position.x < rightLimit.position.x;
    }
    public void SetTargetThatClosedToPlayer()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
        if(distanceToLeft > distanceToRight)
        {
            target = rightLimit;
        }
        else
        {
            target = leftLimit;

        }
    }
    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
        /*
        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;

        }
        */
        if (distanceToLeft < 5f )
       {
           target = rightLimit;
            inRange = false;
           Flip();

       }
       if (distanceToRight < 5f )
       {
            target = leftLimit;
            inRange = false;

            Flip();
       }
        
       else
        {



        }
        



    }
    public void Flip()
    {
        //Vector3 rotation = transform.eulerAngles;
        //if (transform.position.x > target.position.x)
        //{
        //    rotation.y = 180;
        //}
        //else
        //{
        //    rotation.y = 0;
        //}

        //transform.eulerAngles = rotation;


        //

        Vector3 rotation = transform.eulerAngles;

        Vector3 Dir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        if (angle < 89 & angle > -89)
        {
            rotation.y = 0;

            IsFlipped = false;



        }
        else
        {

            IsFlipped = true;
            rotation.y = 180;

        }
        transform.eulerAngles = rotation;

        ///
    }

    public void CallHitBox()
    {
        if(target != null)
        {
            hitBox.GetComponent<DoHitPlayer>().CheckIfTargetIsNull(target.gameObject);
        }

    }

    public void MakeAJump()
    {
        bool isDetectColliderInFront;
        bool isDetectColliderIn45Degree;

        if (IsFlipped)
        {
            isDetectColliderInFront = Physics2D.Raycast(rayCastPos.transform.position, -Vector2.right, dis1, layerMK);
            Vector2 vec = new Vector2(-1,2);
            isDetectColliderIn45Degree = Physics2D.Raycast(rayCastPos.transform.position, vec, dis2, layerMK);
            if(isDetectColliderInFront == true && isDetectColliderIn45Degree == false)
            {
                rb.AddForce(Vector2.up * 80, ForceMode2D.Impulse);
                Debug.Log("make jump");

            }
            else
            {
  
               

                
            }

        }
        if (!IsFlipped)
        {

            isDetectColliderInFront = Physics2D.Raycast(rayCastPos.transform.position, Vector2.right, dis1, layerMK);
            Vector2 vec = new Vector2(1, 2);
            isDetectColliderIn45Degree = Physics2D.Raycast(rayCastPos.transform.position, vec, dis2, layerMK);

            if (isDetectColliderInFront == true && isDetectColliderIn45Degree == false)
            {
                rb.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
            

            }
            else
            {



            }
        }
    }
    public bool DetectPlayer()
    {
    
 

        if (IsFlipped)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayCastPos.transform.position, -Vector2.right, 5f);
            RaycastHit2D hit2 = Physics2D.Raycast(rayCastPos.transform.position, Vector2.right, 5f);

            if (hit.transform.gameObject.tag == "Player" || hit2.transform.gameObject.tag == "Player")
            {
                if(hit2.transform.gameObject.tag == "Player")
                {
                    target = hit2.transform;

                }
                if (hit.transform.gameObject.tag == "Player")
                {
                    target = hit.transform;

                }
                return true;
            }
             else
            {
                target = leftLimit;
                return false;
            }
   
          
 

        }
        else if (!IsFlipped)
        {
       
            RaycastHit2D hit = Physics2D.Raycast(rayCastPos.transform.position,  Vector2.right, 10f);
            if (hit.transform.gameObject.tag == "Player")
            {
                target = hit.transform;

                return true;
            }
            else
            {
                target = rightLimit;
                return false;
            }
        }
        else
        {
            SetTargetThatClosedToPlayer();
            return false;
        }
    }
    public void takeDmg(float dmg)
    {
        if(HealthEnemy >= dmg && HealthEnemy > 0)
        {

        HealthEnemy -= dmg;
        }
        else
        {
            WaveSpawner.instance.CurrentKillNumPerWave++;
            Destroy(this.gameObject);
        }
    }
}

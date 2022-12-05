using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    public SpriteRenderer SpriteGet;
    Rigidbody2D rb;

    public float groundDistance = 2.16f;
    public LayerMask groundMask;
    public bool m_isGrounded = true;

    float dirX, MoveSpeed = 5f;

    public bool isFliped;

    public int NumJumpAllow = 2;

    public float health = 1f;
    public float maxHealth = 1f;
    public Image imgHealth;


    public float shield = 1f;
    public float maxShield = 1f;
    public Image imgShield;

    public Animator anim;

    //

    public int GainCoins;
    public Text txt_Coins;
    public int TotalLives;
    public Text txt_Lives;
    public bool playerDead;
    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;
        SpriteGet =  GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isFliped = false;
        health = maxHealth;
        imgHealth.fillAmount = maxHealth / health;

        shield = maxShield;

        imgShield.fillAmount = maxShield / shield;

        GainCoins = 0;
        TotalLives = 3 ;
        txt_Lives.text = "Lives : "+ TotalLives;
    }

    // Update is called once per frame
    void  Update()
    {
        //
        CheckGround();
        //
        dirX = Input.GetAxisRaw("Horizontal") * 20f;
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            anim.SetBool("Walk", true);
            SpriteGet.flipX = false;
            isFliped = false;
        }
        else if(Input.GetAxisRaw("Horizontal") == -1)
        {
            anim.SetBool("Walk", true);
            SpriteGet.flipX = true;
            isFliped = true;
        }
        else
        {
            anim.SetBool("Walk", false);
        }
        if (Input.GetKeyDown(KeyCode.W) && NumJumpAllow > 0)
        {
            rb.AddForce(Vector2.up * 8f,ForceMode2D.Impulse);
            NumJumpAllow--;

        }

    }
    private void FixedUpdate()
    {

        rb.velocity = (new Vector2(dirX, rb.velocity.y));

    
    }
    public void AddCoins(int NumGain)
    {
        GainCoins += NumGain;
        txt_Coins.text = "Coins : " + GainCoins;
    }
    void CheckGround()
    {
        Vector2 pos = transform.position;
        Vector2 Vec = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(pos, Vec, groundDistance, groundMask);

        if (hit.collider != null)
        {
            m_isGrounded = true;
            NumJumpAllow = 2;
        }
        else
        {
            m_isGrounded = false;

        }


    }
        private void UpdateSpriteFlip()
    {
        // The conditions according to your needs of course
        //SpriteGet.flipX = Physics2D.gravity.x < 0;
     
    }

    public void TakeDmg(float dmg)
    {


        //
        if(playerDead == false)
        {
            if (shield > 0)
            {
                shield -= dmg;
                if (shield < 0)
                {
                    float setAfterDmg = shield;
                    health -= setAfterDmg;
                    shield = 0;

                    if (health <= 0)
                    {
                        health = 0;
                        Debug.Log("playerDEAD");


                        imgShield.fillAmount = shield / maxShield;
                        imgHealth.fillAmount = health / maxHealth;
                        //ThePlayerIsDead
                        // KillCarDestroy(PlaceWhoDamage);

                    }

                    imgShield.fillAmount = shield / maxShield;
                    imgHealth.fillAmount = health / maxHealth;
                }
                imgShield.fillAmount = shield / maxShield;
                imgHealth.fillAmount = health / maxHealth;
            }
            else
            {
                health -= dmg;
                if (health < 0)
                {
                    health = 0;
                    Debug.Log("playerDEAD");
                    //ThePlayerIsDead
                    //SetTheNameWhoKilled
                    playerDead = true;


                }
                imgHealth.fillAmount = health / maxHealth;
            }
        }
        else
        {

            if (TotalLives > 0 && playerDead == false)
            {
                StartCoroutine("SetPlayerBackToLife");
            }
            else
            {
                Debug.Log("LostGame");
                GameManaging.instance.LostPanel();
            }
        }


        //

    }
    IEnumerator SetPlayerBackToLife()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(2.0f);
        TotalLives -= 1 ;
        txt_Lives.text = "Lives : " + TotalLives;
        health = 1f;
        imgHealth.fillAmount = health;
        transform.position = WaveSpawner.instance.spawnPlayerPos.transform.position;

        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        playerDead = false;
        StopAllCoroutines();


    }


}

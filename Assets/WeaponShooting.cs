using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.U2D;
using UnityEngine.Experimental.Rendering.Universal;
public class WeaponShooting : MonoBehaviour
{

    public int MinAmmo;
    public int MaxAmmo;

    public int Clips;

    public float FireRate;
    public float TimeSet;

    public GameObject Bullet;
    public GameObject FirePlace;

    public float BulletForce;

    public MovePlayer playerGetIFfliped;

    private SpriteRenderer SRend;

    public bool IsReloading;
    public bool isFiring;

    public Transform AimTransform;

    public Vector3 ScreenPos;
    public GameObject MazzleFlash;

    public Sprite setWeaponImage;

    public bool shotAnim;

    public float WeaponDmg = 10f;

    public Light2D lightSet;
    public Light2D LightInstance;

    public string WeaponName;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
        shotAnim = false;
        SRend = GetComponent<SpriteRenderer>();
        AimTransform.gameObject.GetComponent<WeaponSelect>().SetMinAmmo(MinAmmo, MaxAmmo, Clips);
        AimTransform.gameObject.GetComponent<WeaponSelect>().SetWeaponImage(setWeaponImage);

        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShoot();
    }
    public void HandleAiming()
    {
        ScreenPos = Input.mousePosition;
        ScreenPos.z = 0;

        ScreenPos = Camera.main.ScreenToWorldPoint(ScreenPos);

        Vector3 aimDirection = (ScreenPos - AimTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        if (angle < 89 & angle > -89)
        {

            //SRend.flipX = false;
            playerGetIFfliped.SpriteGet.flipX = false;
            playerGetIFfliped.isFliped = false;
            AimTransform.eulerAngles = new Vector3(0, 0, angle);


        }
        else
        {
            float angle1 = Mathf.Atan2( aimDirection.y, -aimDirection.x) * Mathf.Rad2Deg;
            angle = angle1;
            //SRend.flipX = true;
            playerGetIFfliped.SpriteGet.flipX = true;

            playerGetIFfliped.isFliped = true;
            AimTransform.eulerAngles = new Vector3(0, 180, angle);

        }


    }
    public void HandleShoot()
    {
        if (playerGetIFfliped.isFliped == false)
        {
            //SRend.flipX = false;
        }
        else
        {
            //SRend.flipX = true;
        }
        float translation = Input.GetAxis("Fire1");
        if(Input.GetAxis("Fire1") == 0)
        {

        }
        if (Input.GetAxis("Fire1") == 1 && Time.time > TimeSet && IsReloading == false)
        {
            isFiring = true;
            TimeSet = Time.time + FireRate;
            CheckAmmoAndReload();
        }
    }
    public void Shooting()
    {


        ///InstantiateBullet;
        GameObject BLT = Instantiate(Bullet, FirePlace.transform.position, FirePlace.transform.rotation);
        //BLT.GetComponent<Rigidbody2D>().AddForce(BLT.transform.forward * BulletForce , ForceMode2D.Impulse) ;
        BLT.GetComponent<Bullet>().BulletDmg = WeaponDmg;
        BLT.GetComponent<Rigidbody2D>().velocity = AimTransform.transform.right * BulletForce;
        GameObject MazzuleFlash = Instantiate(MazzleFlash, FirePlace.transform.position, Quaternion.identity);
        
        Quaternion e = Quaternion.Euler(0, 0, AimTransform.transform.rotation.y);

        MazzuleFlash.transform.localEulerAngles = new Vector3(0, AimTransform.localEulerAngles.y, AimTransform.localEulerAngles.z);


         Destroy(MazzuleFlash, 0.2f);
        //
        if(LightInstance == null)
        {
            LightInstance = Instantiate(lightSet, FirePlace.transform.position, FirePlace.transform.rotation);
           
        }
        LightInstance.transform.position = FirePlace.transform.position;
        LightInstance.enabled = true;
        Invoke("disabledLight", 0.1f);
        

        //

        //AnimationTriger
        //
        //shotAnim

        float setEnd = AimTransform.transform.localPosition.x - 0.89f;
        /*
        AimTransform.transform.DOLocalMoveX(0, 0.2f).OnComplete(()=>
        {
            AimTransform.transform.localPosition = new Vector2(-0.23f, 0);
        });*/

        //

        StartCoroutine(setShootingAnim());
        //anim.SetTrigger("Shot");


        MinAmmo -= 1;
        AimTransform.gameObject.GetComponent<WeaponSelect>().SetMinAmmo(MinAmmo, MaxAmmo,Clips);


    }

    public void disabledLight()
    {
        LightInstance.enabled = false;
    }
    public void InteractWithAimTransformUI(WeaponSelect Wp)
    {
        Wp.SetMinAmmo(MinAmmo, MaxAmmo, Clips);
    }
    public void CheckAmmoAndReload()
    {
        if (MinAmmo > 0 && IsReloading == false && Clips > -1)
        {
            Shooting();
        }
        else
        {
            StartCoroutine("SetReload");
        }
    }

    public void Reloading()
    {
        if (Clips > 0 && IsReloading == false)
        {
            MinAmmo = MaxAmmo;
            Clips -= 1;

        }
    }

    IEnumerator SetReload()
    {
        IsReloading = true;
        yield return new WaitForSeconds(0.5f);
        if (Clips > 0)
        {
            MinAmmo = MaxAmmo;
            Clips -= 1;
            IsReloading = false;

        }
        else
        {
            IsReloading = false;
            yield return null;
        }
    }


    public void CallCourotineForGetAmmo()
    {
        StartCoroutine("SetReloadFromWeaponPack");
    }

    IEnumerator setShootingAnim()
    {
        if(shotAnim == false)
        {
            shotAnim = true;
            AimTransform.transform.position = AimTransform.transform.position - transform.right;
               yield return new WaitForSeconds(0.2f);
               AimTransform.transform.localPosition = new Vector2(-0.23f, 0);
            shotAnim = false;

            }
    }
    IEnumerator SetReloadFromWeaponPack()
    {
        IsReloading = true;
        yield return new WaitForSeconds(0.5f);
        MinAmmo = MaxAmmo;
        Clips += 1;
        IsReloading = false;
    }
}
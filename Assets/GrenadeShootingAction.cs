using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeShootingAction : MonoBehaviour
{
    public int MinAmmo;
    public int MaxAmmo;

    public int Clips;

    public float FireRate;
    public float TimeSet;

    public GameObject GrenadePref;
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




    public string WeaponName;

    public WeaponShooting wsShoot;
    // Start is called before the first frame update
    void Start()
    {
        wsShoot = GetComponent<WeaponShooting>();
        AimTransform = wsShoot.AimTransform;
        playerGetIFfliped = wsShoot.playerGetIFfliped;
        WeaponDmg = wsShoot.WeaponDmg;
        Clips = wsShoot.Clips;
        MinAmmo = wsShoot.MinAmmo;
        MaxAmmo = wsShoot.MaxAmmo;
        FireRate = wsShoot.FireRate;
        BulletForce = wsShoot.BulletForce;
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
            float angle1 = Mathf.Atan2(aimDirection.y, -aimDirection.x) * Mathf.Rad2Deg;
            angle = angle1;
            //SRend.flipX = true;
            playerGetIFfliped.SpriteGet.flipX = true;

            playerGetIFfliped.isFliped = true;
            AimTransform.eulerAngles = new Vector3(0, 180, angle);

        }


    }
    public void HandleShoot()
    {

        float translation = Input.GetAxis("Fire1");
    
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
        GameObject BLT = Instantiate(GrenadePref, FirePlace.transform.position, FirePlace.transform.rotation);
        //BLT.GetComponent<Rigidbody2D>().AddForce(BLT.transform.forward * BulletForce , ForceMode2D.Impulse) ;
        BLT.GetComponent<GrenadeScriptBullet>().Damage = WeaponDmg;
        BLT.GetComponent<Rigidbody2D>().velocity = AimTransform.transform.right * BulletForce;

        Quaternion e = Quaternion.Euler(0, 0, AimTransform.transform.rotation.y);


        float setEnd = AimTransform.transform.localPosition.x - 0.89f;



        MinAmmo -= 1;
        wsShoot.MinAmmo = MinAmmo;
        AimTransform.gameObject.GetComponent<WeaponSelect>().SetMinAmmo(MinAmmo, MaxAmmo, Clips);


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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro ;

public class RandomBoxUIObject : MonoBehaviour
{
    public GameObject WeaponRandWeaponPref;

    public GameObject WeaponRandWeapon;
    public int[] clipsAdded;
    public int[] clipsCost;

    public int[] armor;
    public int[] armorCost;

    public int[] health;
    public int[] healthCost;

    int setRandomNumberSelected;

    public Image CurrentObjSelection;
    public TextMeshProUGUI txtHeader;
    public TextMeshProUGUI txt;
    public SetBoxState boxState;


    public ShopSystem ShopSys;

    public Sprite healthImg;
    public Sprite armorImg;
    public Sprite ClipsImage;

    public int cost;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentOBJ(int val)
    {
        switch (val)
        {
            case 0:
                boxState = SetBoxState.Weapon;
                ConfigureWeaponAndShow();

                cost = 250;
                break;
            case 1:
                boxState = SetBoxState.Health;
                ConfigureHealthAndShow();
                cost = 100;
                break;
            case 2:
                boxState = SetBoxState.Armor;
                ConfigureArmorAndShow();
                cost = 100;
                break;
            case 3:
                boxState = SetBoxState.Clips;
                ConfigureClipsAndShow();
                cost = 50;
                break;
        }
    }
    //
    public void ConfigureWeaponAndShow()
    {
        GetRandomWeapon();
        if (WeaponRandWeapon != null)
        {
             

        }
        else
        {
            boxState = SetBoxState.Health;
            ConfigureHealthAndShow();
            return;
        }
        if (WeaponRandWeapon.transform.gameObject.name != "")
        {
            WeaponShooting ws = WeaponRandWeapon.GetComponentInChildren<WeaponShooting>(true);

            cost = WeaponRandWeapon.GetComponent<WeaponSettingShop>().CostWeapon;
            CurrentObjSelection.sprite = ws.setWeaponImage;
            txtHeader.text = ws.transform.gameObject.name;
            txt.text = "WeaponDamage +" + ws.WeaponDmg;//addNumHealth
        }

        CurrentObjSelection.GetComponent<Button>().onClick.AddListener(() => ShopSys.BuyRandNum(cost,this.gameObject));

    }
    public void ConfigureHealthAndShow()
    {
        int setRand5 = Random.Range(0, 2);
        setRandomNumberSelected = setRand5;
        cost = healthCost[setRand5];
        CurrentObjSelection.sprite = healthImg;
        txtHeader.text = "Health";
        txt.text = "HEALTH +"+health[setRand5];//addNumHealth
        CurrentObjSelection.GetComponent<Button>().onClick.AddListener(() => ShopSys.BuyRandNum(cost, this.gameObject));

    }
    public void ConfigureArmorAndShow()
    {
        int setRand5 = Random.Range(0, 2);
        setRandomNumberSelected = setRand5;
        cost = armorCost[setRand5];

        CurrentObjSelection.sprite = armorImg;
        txtHeader.text = "Shield";
      txt.text = "SHIELD +"+armor[setRand5];//AddNumArmor
        CurrentObjSelection.GetComponent<Button>().onClick.AddListener(() => ShopSys.BuyRandNum(cost, this.gameObject));

    }
    public void ConfigureClipsAndShow()
    {
        int setRand5 = Random.Range(0, 2);
        setRandomNumberSelected = setRand5;
        cost = clipsCost[setRand5];
        CurrentObjSelection.sprite = ClipsImage;
        txt.text = "Clips " + clipsAdded[setRand5] ;//AddClipsNum
        CurrentObjSelection.GetComponent<Button>().onClick.AddListener(() => ShopSys.BuyRandNum(cost, this.gameObject));

    }

    //
    public void GetRandomWeapon()
    {
        List<GameObject> setList = new List<GameObject>();
        WeaponShooting[] sp = WeaponRandWeaponPref.GetComponentsInChildren<WeaponShooting>(true);
      
        foreach (WeaponShooting tr in sp)
        {
            setList.Add(tr.gameObject);
        }
 
        for(int s = 0;s < setList.Count;s++)
        {
            GameObject objetToReturn = setList[s];

            if (ShopSys.PlayerObjWeaponSelect.WeaponCheckIfValid(objetToReturn) == false)
            {
                WeaponRandWeapon = objetToReturn;
                Debug.Log("Access to find non VALID OBJECT");
        
            }
        }
      
    }
    public void ConfigureToPlayCurrentItem()
    {
        switch (boxState)
        {
            case SetBoxState.Weapon:
                GameObject weaponGetValid = WeaponRandWeapon;
                if (weaponGetValid != null)
                {
                    ShopSys.SetWeaponToPlayer(weaponGetValid);
                }
                break;
            case SetBoxState.Health:
                int setRand1 = setRandomNumberSelected;
                ShopSys.SetPlayerHealth(health[setRand1]);
                break;
            case SetBoxState.Armor:
                int setRand = setRandomNumberSelected;
                ShopSys.SetPlayerArmor(armor[setRand]);
                break;
            case SetBoxState.Clips:
                int setRand2 = setRandomNumberSelected;
      
                ShopSys.SetPlayerClips(clipsAdded[setRand2]);

                break;
        }
    }
}
public enum SetBoxState
{
    Weapon,Health,Armor,Clips
}

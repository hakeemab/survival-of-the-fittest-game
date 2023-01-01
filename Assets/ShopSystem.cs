using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{

    public int getCurrentMoney;
    public GameObject ShopUI;
    public GameObject ShopUIContent;

    public GameObject RandomBoxObj;

    public float CurrentHealth;
    public float CurrentArmor;

    public Image CurrentHealthSlider;
    public Image CurrentArmorSlider;


    public GameObject PlayerObj;
    public WeaponSelect PlayerObjWeaponSelect;


    public Text txtMoney;
    bool isAlreadyInstanted;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetShopAndStoppedGameActivate()
    {
        if(isAlreadyInstanted == false)
        {
            Time.timeScale = 0;
            ShopUI.transform.gameObject.SetActive(true);
            getCurrentMoney = PlayerPrefs.GetInt("MoneyPlayer", 1000);
            txtMoney.text = "" + getCurrentMoney;
            SetCurrentPlayerArmorAndHealth();
            SetShopUI();
            isAlreadyInstanted = true;

            //TEMP
            PlayerPrefs.SetInt("MoneyPlayer", 1000);
        }

    }


    public void SetShopRandomBoxes()
    {
        int randNum = Random.Range(0, 3);
        for(int i = 0; i < 4;i++)
        {
            if(randNum != i)
            {
                GameObject setUI = Instantiate(RandomBoxObj, transform.position, Quaternion.identity, ShopUIContent.transform);
                RandomBoxUIObject boxOBJ = setUI.GetComponent<RandomBoxUIObject>();
                boxOBJ.ShopSys = this;
                boxOBJ.SetCurrentOBJ(i);
            }
    
        }
    }

    public void SetShopUI()
    {
        getCurrentMoney = PlayerPrefs.GetInt("MoneyPlayer", 1000);
        txtMoney.text = "" + getCurrentMoney ;
        SetShopRandomBoxes();
    }

    public void BuyRandNum(int cost,GameObject uiToDestroy)
    {
        getCurrentMoney = PlayerPrefs.GetInt("MoneyPlayer", 1000);

        if (cost < getCurrentMoney)
        {
            getCurrentMoney -= cost;
            PlayerPrefs.SetInt("MoneyPlayer", getCurrentMoney);
            txtMoney.text = "" + getCurrentMoney;
            RandomBoxUIObject setRan = uiToDestroy.GetComponent<RandomBoxUIObject>();
            setRan.ConfigureToPlayCurrentItem();
            Destroy(uiToDestroy);
        }

    }

    public void GoBackToGame()
    {
        Time.timeScale = 2;
        ShopUI.transform.gameObject.SetActive(false);
        isAlreadyInstanted = false;


    }


    ///WeaponConfigureToPlayer
    ///

    public void SetWeaponToPlayer(GameObject weaponAdd)
    {
        PlayerObjWeaponSelect.AddNewWeapon(weaponAdd);

        //ShowTXT WHAT COLLECTED
    }

    //HealthConfigureToPlayer
    public void SetPlayerHealth(float healthadd)
    {
        PlayerObj.GetComponent<MovePlayer>().AddPlayerHealth(healthadd);
        float health = PlayerObj.GetComponent<MovePlayer>().health;
        float MaxHealth = PlayerObj.GetComponent<MovePlayer>().maxHealth;
        CurrentHealthSlider.fillAmount = health / MaxHealth;
    }
    //ArmorConfigureToPlayer
    public void SetPlayerArmor(float Armor)
    {
        PlayerObj.GetComponent<MovePlayer>().AddPlayerShield(Armor);
       float shield = PlayerObj.GetComponent<MovePlayer>().shield;
        float Maxshield = PlayerObj.GetComponent<MovePlayer>().maxShield;
        CurrentArmorSlider.fillAmount = shield / Maxshield;

    }
    public void SetCurrentPlayerArmorAndHealth()
    {
        float shield = PlayerObj.GetComponent<MovePlayer>().shield;
        float Maxshield = PlayerObj.GetComponent<MovePlayer>().maxShield;
        CurrentArmorSlider.fillAmount = shield / Maxshield;
        float health = PlayerObj.GetComponent<MovePlayer>().health;
        float MaxHealth = PlayerObj.GetComponent<MovePlayer>().maxHealth;
        CurrentHealthSlider.fillAmount = health / MaxHealth;
    }
    //ClipsConfigureToPlayer
    public void SetPlayerClips(int clipsAdd)
    {
        PlayerObjWeaponSelect.AddClips(clipsAdd);
    }

}

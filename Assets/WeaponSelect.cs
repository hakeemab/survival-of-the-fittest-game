using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponSelect : MonoBehaviour
{
    public GameObject[] WeaponsStash;

    public MovePlayer PlayerScript;

    public int CurrentWeaponChoose;

    public GameObject CurrentWeaponObj;

    public Text txt_MinAmmo;
    public Text txt_maxAmmo;

    private void Start()
    {
        CurrentWeaponChoose = 0;
        WeaponsStash[CurrentWeaponChoose].SetActive(true);
        CurrentWeaponObj = WeaponsStash[CurrentWeaponChoose].gameObject;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad0))
        {
            WeaponsStash[CurrentWeaponChoose].SetActive(false);
            CurrentWeaponChoose = 0;
            WeaponsStash[CurrentWeaponChoose].SetActive(true);
            CurrentWeaponObj = WeaponsStash[CurrentWeaponChoose].gameObject;


        }
        if (Input.GetKey(KeyCode.Keypad1))
        {
            WeaponsStash[CurrentWeaponChoose].SetActive(false);
            CurrentWeaponChoose = 1;
            WeaponsStash[CurrentWeaponChoose].SetActive(true);
            CurrentWeaponObj = WeaponsStash[CurrentWeaponChoose].gameObject;


        }
    }

    public void SetMinAmmo(int MinAmmo,int MaxAmmmo)
    {
        txt_MinAmmo.text = MinAmmo + "";
        txt_maxAmmo.text = MaxAmmmo + "";
    }
}

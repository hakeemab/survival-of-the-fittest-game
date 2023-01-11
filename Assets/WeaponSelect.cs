using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponSelect : MonoBehaviour
{
    public List<GameObject> WeaponsStash;
    public GameObject WeaponsPrefab;

    public MovePlayer PlayerScript;

    public int CurrentWeaponChoose;

    public GameObject CurrentWeaponObj;

    public Text txt_MinAmmo;
    public Text txt_maxAmmo;

    public Text txt_Clips;

    public Image WeaponImg;
    public Transform setAnimPos;

    private void Awake()
    {
        PlayerScript = GetComponentInParent<MovePlayer>();
    }

    private void Start()
    {
        InstantiateWeapons();

        SetAllWeaponFirstConfigurations();
    }
    public void SetAllWeaponFirstConfigurations()
    {
        CurrentWeaponChoose = 0;
        WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().AimTransform = this.transform;
        WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().playerGetIFfliped = PlayerScript;
        WeaponsStash[CurrentWeaponChoose].SetActive(true);
        Sprite WpImage = WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().setWeaponImage;
        WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().InteractWithAimTransformUI(this);
        SetWeaponImage(WpImage);
        CurrentWeaponObj = WeaponsStash[CurrentWeaponChoose].gameObject;
    }


    public void InstantiateWeapons()
    {

        List<GameObject> setList = new List<GameObject>();
        WeaponShooting[] sp = WeaponsPrefab.GetComponentsInChildren<WeaponShooting>(true);
        Debug.Log("added " + sp.Length);
        foreach (WeaponShooting tr in sp)
        {
            Debug.Log("added " + tr.gameObject.name);
          GameObject set =  Instantiate(tr.gameObject, this.transform.position, Quaternion.identity, this.transform);
            WeaponsStash.Add(set);
        }
   
    }

    public void AddNewWeapon(GameObject AddWeaponToStash)
    {
        WeaponShooting tr = AddWeaponToStash.GetComponentInChildren<WeaponShooting>(true);
        GameObject set = Instantiate(AddWeaponToStash.gameObject, this.transform.position, Quaternion.identity, this.transform);
        set.transform.eulerAngles = new Vector3(0, 0, 0);
        set.transform.localEulerAngles = new Vector3(0, 0, 0);

        WeaponsStash.Add(set);
    }
    public void AddClips(int numClipsTOADD)
    {
        foreach (GameObject tr in WeaponsStash)
        {
            WeaponShooting Ws = tr.GetComponent<WeaponShooting>();
            Ws.Clips += numClipsTOADD;

        }
        SetAllWeaponFirstConfigurations();

    }

    public bool WeaponCheckIfValid(GameObject getWeapon)
    {
        Debug.Log("" + WeaponsStash.Count);
        for (int i = 0;i < WeaponsStash.Count;i++)
        {
            WeaponShooting ws = WeaponsStash[i].GetComponent<WeaponShooting>();
            if (ws.WeaponName == getWeapon.GetComponent<WeaponShooting>().WeaponName)
            {
                Debug.Log("Is already exist OBJECT");

                return true;
            }
      
        }
        return false;
    
            
   
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (WeaponsStash.Count - 1 > CurrentWeaponChoose)
            {
                WeaponsStash[CurrentWeaponChoose].SetActive(false);
                CurrentWeaponChoose += 1;
                WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().AimTransform = this.transform;
                WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().playerGetIFfliped = PlayerScript;
                WeaponsStash[CurrentWeaponChoose].SetActive(true);
                Sprite WpImage = WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().setWeaponImage;
                WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().InteractWithAimTransformUI(this);
                SetWeaponImage(WpImage);
                CurrentWeaponObj = WeaponsStash[CurrentWeaponChoose].gameObject;
            }
            else
            {
                WeaponsStash[CurrentWeaponChoose].SetActive(false);
                CurrentWeaponChoose = 0;
                WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().AimTransform = this.transform;

                WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().playerGetIFfliped = PlayerScript;

                WeaponsStash[CurrentWeaponChoose].SetActive(true);
                Sprite WpImage = WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().setWeaponImage;
                WeaponsStash[CurrentWeaponChoose].GetComponent<WeaponShooting>().InteractWithAimTransformUI(this);

                SetWeaponImage(WpImage);
                CurrentWeaponObj = WeaponsStash[CurrentWeaponChoose].gameObject;
            }



        }
    }

    public void SetMinAmmo(int MinAmmo, int MaxAmmmo, int clips)
    {
        txt_MinAmmo.text = MinAmmo + "";
        txt_maxAmmo.text = MaxAmmmo + "";
        txt_Clips.text = clips + "";
    }

    public void SetWeaponImage(Sprite setImg)
    {
        WeaponImg.sprite = setImg;
    }
}

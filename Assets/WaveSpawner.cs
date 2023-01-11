using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

 

    public int RandomPosSpawn;
    public float TimeSet;

    public bool WaveBegan;
    public static WaveSpawner instance;

    public GameObject[] ZombiesSpawners;

    public GameObject[] PosToSpawnZombies;

    public Transform leftLimit;
    public Transform rightLimit;

    public int Wave;

    public int WaveChanceOfSpawn;

    public float TimeRateToSpawn;

    public float TimeRatePerWaveDouble;

    //Wave Variables
    int RandomSetPos1;
    int RandomSetPos2;

    public Text txtWave;
    //
    int RandomZombie1;
    int RandomZombie2;

    //

    int NumberOfZombiesRandomSpawn1;
    int NumberOfZombiesRandomSpawn2;

    public int NumOfKillPerWave;
    public int SpawnedZombiesSet;
    public int CurrentKillNumPerWave;

    private bool stopSpawnZombies;

   public int numOfSpawnedZombiesTotal;
    bool setActive;

    public Transform spawnPlayerPos;
    
    private void Awake()
    {
         if (instance == null)
         {
             instance = this;
         }
   
    }
    // Start is called before the first frame update
    void Start()
    {

        setActive = false;
        stopSpawnZombies = false;
        TimeRatePerWaveDouble = 7f;
        numOfSpawnedZombiesTotal = 0;
        RandomSetPos1 = 0;
        RandomSetPos2 = 1;

        RandomZombie1 = 0;
        RandomZombie2 = 0;
        SpawnedZombiesSet = 0;
        NumberOfZombiesRandomSpawn1 = 1;
        NumberOfZombiesRandomSpawn2 = 2;

        CurrentKillNumPerWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
     
        if(WaveBegan)
        {
            if(numOfSpawnedZombiesTotal < NumOfKillPerWave)
            {
                SetZombies();
            }
            if(CurrentKillNumPerWave == NumOfKillPerWave)
            {
                WaveBegan = false;
            }
            //check

        }
        /* if( NumOfKillPerWave <= CurrentKillNumPerWave)*/
       else
        {
            if(setActive == false)
            {
                WaveBegan = false;



                //
                StartCoroutine("SetSecondWaveZombies");
                setActive = true;
                //IntergrateShopSystem

         

            }

        }
        
    }


    public void SetZombies()
    {
        TimeRateToSpawn += Time.deltaTime;

        if (TimeRateToSpawn >= TimeRatePerWaveDouble)
        {
            int RandomSetPos = Random.Range(RandomSetPos1, RandomSetPos2);
            int RandomZombie = Random.Range(RandomZombie1, RandomZombie2);

            int NumberZombie = Random.Range(NumberOfZombiesRandomSpawn1, NumberOfZombiesRandomSpawn2);
            SetSpawnPos(ZombiesSpawners[RandomZombie], PosToSpawnZombies[RandomSetPos].transform.position, NumberZombie);
            TimeRateToSpawn = 0;
        }
    }

    public void SetSpawnPos(GameObject MonsterSpawn,Vector3 MonsterSpawnPos,int NumberOfZombiesSpawn)
    {
        Debug.Log(NumberOfZombiesSpawn);

        for (int i = 0; i < NumberOfZombiesSpawn; i++)
        {
           GameObject GO = Instantiate(MonsterSpawn, MonsterSpawnPos, Quaternion.identity);
            GO.GetComponent<Enemy_Behaviour>().leftLimit = leftLimit;
            GO.GetComponent<Enemy_Behaviour>().rightLimit = rightLimit;
            GO.GetComponent<Enemy_Behaviour>().MoveSpeed = Random.Range(2f, 4f);
            GO.GetComponent<Enemy_Behaviour>().enabled = true;
            GO.gameObject.SetActive(true);
            numOfSpawnedZombiesTotal++;
            SpawnedZombiesSet++;

        }
    }

    public void IncrimentToSetMoreHardPerWave()
    {
        if(ZombiesSpawners.Length > RandomZombie2)
        {
            RandomZombie2 += 1;
        }
        if (PosToSpawnZombies.Length > RandomSetPos2)
        {
            RandomSetPos2 += 1;
        }
        if(NumberOfZombiesRandomSpawn2 < 6)
        {
            NumberOfZombiesRandomSpawn2 += 1;
        }
        if(TimeRatePerWaveDouble > 3)
        {
            TimeRatePerWaveDouble -= 0.1f;
        }
        NumOfKillPerWave += 10;
 
    }

    public void PassWave()
    {
        Wave += 1;
        IncrimentToSetMoreHardPerWave();
    }
    public void AnimWaveNum(int iWave)
    {
        txtWave.text = "Wave " + iWave;
    }
     IEnumerator SetSecondWaveZombies()
     {
        if (Wave % 2 == 0)
        {
            Debug.Log("waveStopped");
            GetComponent<ShopSystem>().SetShopAndStoppedGameActivate();
        }
        yield return new WaitForSeconds(5.0f);
        AnimWaveNum(Wave + 1);
        yield return new WaitForSeconds(2.0f);
        PassWave();
        Debug.Log("Call courotine");
        WaveBegan = true;
        setActive = false;
    }
}

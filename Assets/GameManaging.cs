using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManaging : MonoBehaviour
{

    public static GameManaging instance;
    public GameObject cavasPanelLostSetActive;
    public GameObject ChooseLevel;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LostPanel()
    {
   
        cavasPanelLostSetActive.gameObject.SetActive(true);
        Time.timeScale = 0;
    }    
    public void SetBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        cavasPanelLostSetActive.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void CloseChooseLVL()
    {
        ChooseLevel.SetActive(false);
    }
    public void openChooseLVL()
    {
        ChooseLevel.SetActive(true);

    }
    public void CHOOSE(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }
    public void PlayFirstLVL()
    {
        SceneManager.LoadScene(1);
    }
}

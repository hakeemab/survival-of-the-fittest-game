using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STARTGAME : MonoBehaviour
{
     public GameObject InfoPanel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        InfoPanel.SetActive(true);
    }

    public void ClosePanelUI()
    {
        Time.timeScale = 2;
        InfoPanel.SetActive(false);

    }
}

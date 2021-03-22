using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text gemNum;
    public GameObject over;
    void Update()
    {
        if(gemNum.text == "4")
        {
            Time.timeScale = 0;
            over.SetActive(true);
        }
    }
}

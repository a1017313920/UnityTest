using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{ 
    private void Update()
    {
        SetAct();
    }
    void SetAct()
    {
        GameObject.Find("Canvas/111").SetActive(true);
        GameObject.Find("Canvas/222/111").SetActive(true);
        GameObject.Find("Canvas/333/111/111").SetActive(true);
        GameObject.Find("Canvas/333/222").SetActive(true);
        GameObject.Find("Canvas/444").SetActive(true);
        GameObject.Find("Canvas/444/555").SetActive(true);
        GameObject.Find("Canvas/444/555/666").SetActive(true);
    }
}

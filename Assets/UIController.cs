using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject UI;
    public void ActiveUI()
    {
        UI.SetActive(true);
    }
}

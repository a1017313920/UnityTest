using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    private float startPointX,startPointY;
    public bool lockY;

    private void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }
    //�����ӽ��ƶ�����
    private void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX + (cam.position.x - startPointX) * moveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPointX + (cam.position.x - startPointX) * moveRate, startPointY + (cam.position.y - startPointY) * moveRate);
        }
    }
}

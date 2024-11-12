using System;
using UnityEngine;

public class ParallaxBackGround: MonoBehaviour
{
    private GameObject cam;

    //视差滚动系数
    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    private void Update()
    {
        //相机从初始位置到当前位置的位移量
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        //相机盒视差效果的位移量
        float distanceToMove = cam.transform.position.x * parallaxEffect;   
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if (distanceMoved > xPosition + length)
            xPosition += length;
        else if (distanceMoved < xPosition - length)
            xPosition -= length;
    }
}
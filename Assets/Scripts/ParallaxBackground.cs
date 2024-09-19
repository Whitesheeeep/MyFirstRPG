using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        if (cam == null)
        {
            Debug.LogError("Main Camera not found");
        }

        xPosition = transform.position.x;//初始化 xPosition
        
        //bounds 表示渲染器包围盒的体积，size 表示包围盒的尺寸，size.x 表示包围盒的宽度;https://docs.unity3d.com/cn/2020.2/ScriptReference/Renderer-bounds.html
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);//与摄像机相差位置相差的距离

        transform.position = new Vector3(xPosition + distance, transform.position.y);

        if (distanceMoved > xPosition + length) 
        {
            xPosition = xPosition + length;    
        }
        else if(distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }

    }
}

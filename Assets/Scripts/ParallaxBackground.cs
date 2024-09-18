using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        if (cam == null)
        {
            Debug.LogError("Main Camera not found");
        }

        xPosition = transform.position.x;//≥ı ºªØ xPosition
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPosition + distance, transform.position.y);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour
{

    Transform clouds0;
    Transform clouds1;

    // Movement speed in units per second.
    private float speed = 1f;

    // Total distance between the markers.
    private float journeyLength = 10;

    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        Transform background = GameObject.Find("Background").transform;
        clouds0 = background.Find("Clouds_0");
        clouds1 = background.Find("Clouds_1");
        initPosition = clouds1.position;
    }

    // Update is called once per frame
    void Update()
    {
        clouds0.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
        clouds1.position -= new Vector3(speed, 0, 0) * Time.deltaTime;

        // Check is cloud not visible in camera and move to init pos
        if (!Camera.main.IsObjectVisible(clouds0.gameObject))
        {
            clouds0.position = initPosition;
        }

        if (!Camera.main.IsObjectVisible(clouds1.gameObject))
        {
            clouds1.position = initPosition;
        }
    }
}

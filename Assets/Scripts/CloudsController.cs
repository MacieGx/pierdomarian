using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour
{

    Transform clouds0;
    Transform clouds1;

    // Movement speed in units per second.
    private float speed
    {
        get { return Character.getInstance().isPlaying ? 0.5f : 1f; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform background = GameObject.Find("Background").transform;
        clouds0 = background.Find("Clouds_0");
        clouds1 = background.Find("Clouds_1");
    }

    // Update is called once per frame
    void Update()
    {
        clouds0.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        clouds1.position += new Vector3(speed, 0, 0) * Time.deltaTime;

        // Check is cloud not visible in camera and move to init pos
        if (!Camera.main.IsObjectVisible(clouds0.gameObject))
        {
            Debug.Log("xddddddd");
            clouds0.position = new Vector3(Camera.main.OrthographicBounds().min.x - clouds0.GetComponent<SpriteRenderer>().bounds.extents.x, clouds0.position.y, 1);
        }

        if (!Camera.main.IsObjectVisible(clouds1.gameObject))
        {
            clouds1.position = new Vector3(Camera.main.OrthographicBounds().min.x - clouds1.GetComponent<SpriteRenderer>().bounds.extents.x, clouds1.position.y, 1);
        }
    }
}

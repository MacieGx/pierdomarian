using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    Character character;
    GameObject tubesObject;

    private int tubeInitOffset = 3;
    private int tubeOffset = 5;
    private int tubeMinHeight = 8;
    private int tubeMaxHeight = 35;

    private Bounds cameraBounds;

    // Movement speed in units per second.
    private float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        character = Character.getInstance();
        cameraBounds = Camera.main.OrthographicBounds();
        tubesObject = GameObject.Find("Tubes");

        initTubes();
    }

    // Create first tubes before first frame
    void initTubes()
    {
        if (tubesObject.transform.childCount == 0)
        {
            throw new System.ArgumentException("Initial Tube not found, you have to create first tube in editor.");
        }

        Transform firstTube = tubesObject.transform.GetChild(0);
        while(Camera.main.IsObjectVisible(Tube.GetBasicGameObject(firstTube.gameObject)))
        {
            Tube.CreateTubes(firstTube.position.x + tubeOffset, Random.Range(tubeMinHeight, tubeMaxHeight));
            firstTube = tubesObject.transform.GetChild(tubesObject.transform.childCount - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Generate new tubes and delete old
        int tubes = tubesObject.transform.childCount;
        if(tubes == 0)
        {
            Tube.CreateTubes(character.gameObject.transform.position.x + tubeInitOffset, 10);
        } else
        {
            Transform firstTube = tubesObject.transform.GetChild(0);

            if(!Camera.main.IsObjectVisible(Tube.GetBasicGameObject(firstTube.gameObject)))
            {
                Destroy(firstTube.gameObject);
            }

            Transform lastTube = tubesObject.transform.GetChild(tubes - 1);
            if (Camera.main.IsObjectVisible(Tube.GetBasicGameObject(lastTube.gameObject)))
            {
                GameObject newTube = Tube.CreateTubes(tubesObject.transform.GetChild(tubes - 1).transform.position.x + tubeOffset, Random.Range(tubeMinHeight, tubeMaxHeight));
            }
        }

        if (!character.isPlaying) return;
        foreach (Transform tube in tubesObject.transform)
        {
            tube.transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
        }
    }
}

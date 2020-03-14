using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour {
    private static float tubeScale = 0.13f;
    private static float tubeFreeSpace = 3f;

    public static GameObject GetBasicGameObject(GameObject tube)
    {
        return tube.transform.GetChild(0).GetChild(0).gameObject;
    }

    public static GameObject CreateTubes(float positionX, float height)
    {
        GameObject tubesObject = GameObject.Find("Tubes");
        GameObject tubeCollection = new GameObject();

        tubeCollection.transform.parent = tubesObject.transform;

        GameObject tubeDown = CreateTube(height, false);
        GameObject tubeUp = CreateTube(1000, true, tubeDown.transform.GetChild(0));

        tubeDown.transform.parent = tubeCollection.transform;
        tubeUp.transform.parent = tubeCollection.transform;

        tubeCollection.transform.position = new Vector3(positionX, 0, 1);

        return tubeCollection;
    }

    private static GameObject CreateTube(float height, bool rotated, Transform lastElement = null)
    {
        if(rotated && !lastElement)
        {
            throw new System.ArgumentException("lastElement arg is required when rotated.");
        }


        GameObject tubesObject = GameObject.Find("Tubes");
        Bounds cameraBounds = Camera.main.OrthographicBounds();

        GameObject tube = new GameObject();
        tube.transform.parent = tubesObject.transform;

        GameObject tubeDown = new GameObject();
        tubeDown.transform.parent = tube.transform;

        SpriteRenderer spriteRenderer = tubeDown.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        spriteRenderer.transform.localScale = new Vector3(tubeScale, tubeScale, 1);
        spriteRenderer.sprite = Resources.Load("Sprites/Tube/tube-down-0", typeof(Sprite)) as Sprite;
        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        spriteRenderer.size += new Vector2(0f, height);

        BoxCollider2D tubeDownColider = tubeDown.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        tubeDownColider.size = spriteRenderer.size;

        if (rotated)
        {
            tubeDown.transform.position = new Vector3(0, lastElement.GetComponent<SpriteRenderer>().bounds.max.y + tubeFreeSpace + spriteRenderer.bounds.max.y, 1f);


            GameObject coinObject = new GameObject();
            coinObject.transform.parent = tube.transform;
            BoxCollider2D coinCollider = coinObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            coinCollider.name = "CoinCollision";
            coinCollider.isTrigger = true;
            coinCollider.transform.position = new Vector3(0, spriteRenderer.bounds.min.y);
            coinCollider.transform.localScale = new Vector3(0.5f, 10f);

        } else
        {
            tubeDown.transform.position = new Vector3(0, cameraBounds.min.y + spriteRenderer.bounds.max.y, 1f);
        }

        

        GameObject tubeDownEnd = new GameObject();
        tubeDownEnd.transform.parent = tube.transform;

        if(rotated)
        {
            tubeDownEnd.transform.position = new Vector3(0, spriteRenderer.bounds.min.y);
        } else
        {
            tubeDownEnd.transform.position = new Vector3(0, spriteRenderer.bounds.max.y);
        }

        if (rotated) tubeDownEnd.transform.Rotate(new Vector3(180, 0, 0));

        spriteRenderer = tubeDownEnd.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        spriteRenderer.transform.localScale = new Vector3(tubeScale, tubeScale, 1);
        spriteRenderer.sprite = Resources.Load("Sprites/Tube/tube-up", typeof(Sprite)) as Sprite;
        spriteRenderer.sortingOrder = 1;

        BoxCollider2D tubeDownEndColider = tubeDownEnd.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        tubeDownEndColider.size = spriteRenderer.size;

        return tube;
    }

    public static void DeleteAll()
    {
        GameObject tubesObject = GameObject.Find("Tubes");
        foreach (Transform tube in tubesObject.transform)
        {
            Destroy(tube.gameObject);
        }
    }
}

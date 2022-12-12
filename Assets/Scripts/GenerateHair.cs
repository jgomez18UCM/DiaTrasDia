using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHair : MonoBehaviour
{
    public GameObject cara;

    [SerializeField] int numberOfHairs;
    [SerializeField] float minHairLength;
    [SerializeField] float maxHairLength;
    [SerializeField] float startingWidth;
    [SerializeField] float endingWidth;
    [SerializeField] (Vector3, Vector3)[] hairPositions;
    [SerializeField] GameObject[] hairRendererObjects;

    // Start is called before the first frame update
    void Start()
    {
        hairPositions = new (Vector3, Vector3)[numberOfHairs];
        hairRendererObjects = new GameObject[numberOfHairs];

        int i = 0;
        while(i < numberOfHairs) 
        {
            Vector3 startingPosition = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-4.0f, -1.5f), -1);
            Vector3 endPosition = new Vector3(startingPosition.x, startingPosition.y - Random.Range(minHairLength, maxHairLength), -1);

            int layerMask = 1 << 11;
            RaycastHit2D[] hit;
            if (Physics2D.Raycast(startingPosition, new Vector3(0, 0, 1), 1000f, layerMask))
            {
                hairPositions[i] = (startingPosition, endPosition);
                hairRendererObjects[i] = new GameObject("Hair " + i);
                hairRendererObjects[i].transform.parent = transform;
                hairRendererObjects[i].layer = 9;
                LineRenderer l = hairRendererObjects[i].AddComponent<LineRenderer>();
                BoxCollider2D c = hairRendererObjects[i].AddComponent<BoxCollider2D>();
                c.size = new Vector2(0.1f, 0.1f);
                c.offset = startingPosition;
                l.material = Resources.Load("Blurry", typeof(Material)) as Material;
                l.material.color = Color.black;
                l.positionCount = 10;

                Vector3 v = startingPosition - endPosition;
                float distance = v.magnitude / 10;

                for (int j = 0; j < 10; j++)
                {
                    Vector3 p = startingPosition + new Vector3(Random.Range(-0.02f, 0.02f), -distance * j, 0);

                    l.SetPosition(j, p);
                }

                l.startColor = Color.black;
                l.endColor = Color.black;
                l.startWidth = startingWidth;
                l.endWidth = endingWidth;
                i++;
            }
        }

        cara.GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (GameObject h in hairRendererObjects)
        {
            LineRenderer l = h.GetComponent<LineRenderer>();
            l.startColor = Color.black;
            l.endColor = Color.black;
            l.startWidth = startingWidth;
            l.endWidth = endingWidth;

            i++;
        }
    }
}

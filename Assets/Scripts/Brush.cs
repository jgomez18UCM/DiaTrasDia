using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]

public class Brush : MonoBehaviour
{
    public PaintMakeup makeup;
    [SerializeField] Color color;
    [Range(0.1f, 0.6f)] public float radius;
    [Range(0.1f, 1f)] public float harshness;
    [SerializeField] int numberOfPoints;

    [SerializeField] float z;

    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.material = Resources.Load("Blurry", typeof(Material)) as Material;
        line.material.color = Color.black;
        line.positionCount = numberOfPoints;
        line.loop = false;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        line.positionCount = numberOfPoints;
        for (int j = 0; j < numberOfPoints; j++) 
        {
            line.SetPosition(j, transform.position + new Vector3(radius * Mathf.Cos(j * (360 / numberOfPoints)), radius * Mathf.Sin(j * (360 / numberOfPoints)), z));
        }

        if (Input.GetMouseButton(0))
        {
            makeup.setPainting(true);
            int layerMask = 1 << 11;

            if (Physics2D.Raycast(transform.position, new Vector3(0, 0, 1), 1000f, layerMask))
            {
                makeup.paintPixel(transform.position, color, radius, harshness, false);
            }

            int steps = 3;

            for (int j = 1; j <= steps; j++)
            {
                float currentRadius = (j / (float)steps) * radius;

                for (int i = 0; i < 36; i++)
                {
                    Vector3 pos = transform.position + new Vector3(currentRadius * Mathf.Cos(i * 10), currentRadius * Mathf.Sin(i * 10));

                    if (Physics2D.Raycast(pos, new Vector3(0, 0, 1), 1000f, layerMask))
                    {
                        makeup.paintPixel(pos, color, radius, harshness, steps == j);
                    }
                }
            }
        }

        else makeup.setPainting(false);
    }

    public void setColor(Color c) { color = c; }

    public void setHarshness(float h) { harshness = h; }

    public void setRadius(float r) { radius = r; }

    public float getRadius() { return radius; }
}

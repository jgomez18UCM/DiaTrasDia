using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintMakeup : MonoBehaviour
{
    [SerializeField] Texture2D makeupTexture;
    public bool painting = false;
    Renderer renderer;
    [SerializeField] public float[,] lerpValues;
    [SerializeField] Color fundationColor;
    [SerializeField] float paintedPercentage;
    public PolygonCollider2D paintingArea;

    int paintedPixels = 0;
    Vector2 pixelSize;

    // Start is called before the first frame update
    void Start()
    {
        lerpValues = new float[256, 256];
        
        for(int i = 0; i < 256; i++) 
        {
            for (int j = 0; j < 256; j++) lerpValues[i, j] = 0.0f;
        }

        makeupTexture = new Texture2D(256, 256);
        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = makeupTexture;

        for (int y = 0; y < makeupTexture.height; y++)
        {
            for (int x = 0; x < makeupTexture.width; x++)
            {
                Color color = new Color(0, 0, 0, 0);
                makeupTexture.SetPixel(x, y, color);
            }
        }

        makeupTexture.Apply();

        Bounds bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;
        pixelSize = new Vector2(bounds.size.x / (float)makeupTexture.width, bounds.size.z / (float)makeupTexture.height);
    }

    // Update is called once per frame
    void Update()
    {
        checkPaintedPixels();
    }

    public void paintPixel(Vector2 pos, Color c, float radius, float harshness, bool edge) 
    {
        int x = getPixelCoordinates(pos).Item1;
        int y = getPixelCoordinates(pos).Item2;

        Color color;

        int area = (int)((radius / 4) / pixelSize.x);

        if (!edge)
        {
            for (int i = -area; i <= area; i++)
            {
                for (int j = -area; j <= area; j++)
                {
                    Vector2 p = new Vector2(pos.x + pixelSize.x * i, pos.y + pixelSize.y * j);
                    (int, int) pixel = getPixelCoordinates(p);

                    if (paintingArea.OverlapPoint(p)) 
                    {
                        Color pixelColor = makeupTexture.GetPixel(pixel.Item1, pixel.Item2);

                        if (pixelColor.a != 0 && (c.r != pixelColor.r || c.g != pixelColor.g || c.b != pixelColor.b))
                        {
                            lerpValues[pixel.Item1, pixel.Item2] += harshness / 1000f;
                            color = Color.Lerp(pixelColor, c, lerpValues[pixel.Item1, pixel.Item2]);
                        }
                        else color = c;
                        makeupTexture.SetPixel(pixel.Item1, pixel.Item2, color);

                    }

                }
            }
        }

        else if(paintingArea.OverlapPoint(pos))
        {
            //Color pixelColor = makeupTexture.GetPixel(x, y);

            //if (pixelColor.a != 0 && (c.r != pixelColor.r || c.g != pixelColor.g || c.b != pixelColor.b))
            //{
            //    lerpValues[x, y] += harshness / 1000f;
            //    color = Color.Lerp(pixelColor, c, lerpValues[x, y]);
            //}

            //else color = c;

            //makeupTexture.SetPixel(x, y, color);
        }


        makeupTexture.Apply();
    }

    (int, int) getPixelCoordinates(Vector2 c) 
    {
        Vector2 deltaPos = new Vector2(transform.position.x - c.x, transform.position.y - c.y);

        int x = (int)(deltaPos.x / pixelSize.x);
        int y = (int)(deltaPos.y / pixelSize.y);
        x = (makeupTexture.width / 2) + x % makeupTexture.width;
        y = (makeupTexture.height / 2) + y % makeupTexture.height;

        return (x, y);
    }

    public void setPainting(bool b) 
    {
        painting = b;

        if(!painting) 
        {
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++) lerpValues[i, j] = 0.0f;
            }
        }
    }

    void checkPaintedPixels() 
    {
        paintedPixels = 0;

        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                Color c = makeupTexture.GetPixel(i, j);
                Color distance = c - fundationColor;

                if (c.a != 0 && Mathf.Abs(distance.r) > 0.04f && Mathf.Abs(distance.b) > 0.04f && Mathf.Abs(distance.g) > 0.04f) paintedPixels++;
            }
        }

        paintedPercentage = ((float)paintedPixels / 23000) * 100;
    }

    public void setPresentation()
    {
        checkPaintedPixels();
        int p = -(int)Mathf.Min(paintedPercentage / 3, 3f);
        GetComponent<ChangeVariable>().SetValue(p);
        GetComponent<ChangeVariable>().Add("Presentacion");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintMakeup : MonoBehaviour
{
    [SerializeField] Texture2D makeupTexture;
    public bool painting = false;
    Renderer renderer;
    [SerializeField] public float[,] lerpValues;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void paintPixel(Vector2 pos, Color c, float radius, float harshness, bool edge) 
    {
        Bounds bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;
        Vector2 pixelSize = new Vector2(bounds.size.x / (float)makeupTexture.width, bounds.size.z / (float)makeupTexture.height);
        Vector2 deltaPos = new Vector2(transform.position.x - pos.x, transform.position.y - pos.y);

        int x = (int)(deltaPos.x / pixelSize.x);
        int y = (int)(deltaPos.y / pixelSize.y);
        x = (makeupTexture.width / 2) + x % makeupTexture.width;
        y = (makeupTexture.height / 2) + y % makeupTexture.height;

        Color color;

        int area = (int)((radius / 4) / pixelSize.x);

        if (!edge)
        {
            for (int i = -area; i <= area; i++)
            {
                for (int j = -area; j <= area; j++)
                {
                    Color pixelColor = makeupTexture.GetPixel(x + i, y + j);

                    if (pixelColor.a != 0 && (c.r != pixelColor.r || c.g != pixelColor.g || c.b != pixelColor.b))
                    {
                        lerpValues[x + i, y + j] += harshness / 1000f;
                        color = Color.Lerp(pixelColor, c, lerpValues[x + i, y + j]);
                    }
                    else color = c;
                    makeupTexture.SetPixel(x + i, y + j, color);
                }
            }
        }

        else
        {
            Color pixelColor = makeupTexture.GetPixel(x, y);

            if (pixelColor.a != 0 && (c.r != pixelColor.r || c.g != pixelColor.g || c.b != pixelColor.b))
            {
                lerpValues[x, y] += harshness / 1000f;
                color = Color.Lerp(pixelColor, c, lerpValues[x, y]);
            }
            else color = c;
            makeupTexture.SetPixel(x, y, color);
        }

        makeupTexture.Apply();
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
}

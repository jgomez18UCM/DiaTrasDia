using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintMakeup : MonoBehaviour
{
    [SerializeField] Texture2D makeupTexture;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        makeupTexture = new Texture2D(128, 128);
        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = makeupTexture;

        for (int y = 0; y < makeupTexture.height; y++)
        {
            for (int x = 0; x < makeupTexture.width; x++)
            {
                Color color = ((x & y) != 0 ? Color.white : Color.gray);
                makeupTexture.SetPixel(x, y, color);
            }
        }

        makeupTexture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

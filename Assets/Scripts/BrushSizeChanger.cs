using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushSizeChanger : MonoBehaviour
{
    public Brush brush;
    [Range(-1, 1)] public int sign;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSize()
    {
        float s = brush.getRadius();
        s =  Mathf.Clamp(s + 0.05f * sign, 0.1f, 1f);
        brush.setRadius(s);
    }
}

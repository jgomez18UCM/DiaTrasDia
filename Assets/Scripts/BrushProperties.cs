using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushProperties : MonoBehaviour
{
    public Brush brush;

    [SerializeField] Color brushColor;
    [SerializeField] float brushRadius;
    [SerializeField] float brushHarshness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBrushProperties()
    {
        brush.setColor(brushColor);
        brush.setHarshness(brushHarshness);
        brush.setRadius(brushRadius);
    }
}

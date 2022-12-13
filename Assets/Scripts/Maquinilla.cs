using System;
using System.Collections;
using System.Collections.Generic;
using uAdventure.Core;
using uAdventure.Runner;
using UnityEngine;

public class Maquinilla : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<LineRenderer>().enabled = false;
    }
}

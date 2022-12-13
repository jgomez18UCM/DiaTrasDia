using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Renderer mRenderer;
    private bool cogido;
    private Vector3 posicionCogido;
    // Start is called before the first frame update
    void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
        cogido = false;
        posicionCogido = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (mousePos.y >= transform.position.y - mRenderer.bounds.size.y / 2.0
                && mousePos.y <= transform.position.y + mRenderer.bounds.size.y / 2.0
                && mousePos.x >= transform.position.x - mRenderer.bounds.size.x / 2.0
                && mousePos.x <= transform.position.x + mRenderer.bounds.size.x / 2.0)
            {
                cogido = true;
                posicionCogido = mousePos - transform.position;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (cogido)
            {
                transform.position = mousePos - posicionCogido;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (cogido) cogido = false;
        }

    }
}

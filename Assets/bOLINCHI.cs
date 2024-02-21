using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bOLINCHI : MonoBehaviour
{
    private Vector3 posicionInicial;

    public Vector3 PosicionInicial { get => posicionInicial; }

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * 30 * Time.deltaTime);
    }
   
}

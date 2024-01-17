using System;
using UnityEngine;

public class ArmasMostrador : MonoBehaviour
{
    private BoxCollider[] armasMostrador;

    public BoxCollider[] GetArmasMostrador()
    {
        return armasMostrador;
    }

    private void SetArmasMostrador(BoxCollider[] value)
    {
        armasMostrador = value;
    }

    private void Start()
    {
        armasMostrador = GetComponentsInChildren<BoxCollider>();
    }
}

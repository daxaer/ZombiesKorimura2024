using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamaraMouseMenu : MonoBehaviour
{
    Vector2 _startpos;

    [SerializeField] private float efectoModificador;
    private Camera _cam;
    private void Start()
    {
        _startpos = transform.position;
        _cam = Camera.main;
    }

    private void Update()
    {
        var mouse = _cam.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, _startpos.x + (-mouse.x * efectoModificador), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, _startpos.y + (-mouse.y * efectoModificador), 2f * Time.deltaTime);

        transform.position = new Vector3(
            posX,
            posY,
            0
            );
    }
}

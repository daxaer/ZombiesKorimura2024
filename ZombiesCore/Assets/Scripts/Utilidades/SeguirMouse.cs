using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeguirMouse : MonoBehaviour
{
    public Transform player;
    public float maxRadius;
    public float maxDistance;
    private void FixedUpdate()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        //LimiteCuadrado(ray);

        //POR SI SE QUIERE HACER CON UN CIRCULO
        LimitesEnRadio(ray);

    }

    private RaycastHit LimitesEnRadio(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, CursorManager.Instance.LayerSuelo))
        {
            Vector3 mousePos = raycastHit.point;

            Vector3 difference = mousePos - player.position;
            float magnitude = difference.magnitude;
            if (magnitude > maxRadius)
            {
                difference = difference * (maxRadius / magnitude);
            }
            //transform.position = player.position + difference;
            transform.position = new Vector3(player.position.x, 0f, player.position.z) + new Vector3(difference.x, 0f, difference.z);
        }

        return raycastHit;
    }

    private void LimiteCuadrado(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, CursorManager.Instance.LayerSuelo))
        {
            Vector3 mousePos = raycastHit.point;

            mousePos.x = Mathf.Clamp(mousePos.x, player.position.x - maxDistance, player.position.x + maxDistance);
            mousePos.z = Mathf.Clamp(mousePos.z, player.position.z - maxDistance, player.position.z + maxDistance);
            transform.position = mousePos;
            

        }
    }

   
}

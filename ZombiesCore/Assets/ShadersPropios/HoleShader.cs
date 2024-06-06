using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleShader : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody player;

    public float holeSize = 0.1f;
    private void Awake()
    {
        mainCamera = Camera.main;
        player = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, 5f);
        foreach (var collider in hitColliders)
        {
            float SetTransparencia = 0f;

            if(Vector3.Distance(collider.transform.position, mainCamera.transform.position) < Vector3.Distance(player.transform.position, mainCamera.transform.position))
            {
                SetTransparencia = holeSize;
            }

            try
            {
                Material[] materials = collider.transform.GetComponent<Renderer>().materials;

                for(int i = 0; i < materials.Length; i++)
                {
                    materials[i].SetFloat("_hole", SetTransparencia);
                }
            }
            catch { }
        }
    }
}

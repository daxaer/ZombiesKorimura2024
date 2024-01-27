
using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CambiadorCamaras
{
    private static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera CamaraActiva = null;

    public static CinemachineVirtualCamera TerceraPersonaCamara; 
    public static CinemachineVirtualCamera PrimeraPersonaCamara; 


    public static bool EstaCamaraActiva(CinemachineVirtualCamera camara)
    {
        
        return camara == CamaraActiva;
    }
    public static void CambiarCamara(CinemachineVirtualCamera camara)
    {
        camara.Priority = 10;
        CamaraActiva = camara;

        foreach (var cam in cameras)
        {
            if(cam!= camara && cam.Priority !=0)
            {
                cam.Priority = 0;
            }
        }
    }
    public static void CambiarCamaraPrimeraPersona()
    {
        PrimeraPersonaCamara.Priority = 10;
        CamaraActiva = PrimeraPersonaCamara;
        foreach (var cam in cameras)
        {
            if (cam != PrimeraPersonaCamara && cam.Priority != 0)
            {
                cam.Priority = 0;
            }
        }
    }
    public static void CambiarCamaraTerceraPersona()
    {
        TerceraPersonaCamara.Priority = 10;
        CamaraActiva = TerceraPersonaCamara;
        foreach (var cam in cameras)
        {
            if (cam != TerceraPersonaCamara && cam.Priority != 0)
            {
                cam.Priority = 0;
            }
        }
    }
    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }
    public static void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }
}

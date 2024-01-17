using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaNocheManager : Singleton<DiaNocheManager>
{
    [SerializeField] private Light sol;
    [Range(0, 24)] public float horaDelDia;


    [SerializeField] private float sunRotationSpeedDeNocheADia;

    [Header("LightingPreset")]
    [SerializeField] private Gradient colorDelCielo;
    [SerializeField] private Gradient colorDelEcuador;
    [SerializeField] private Gradient colorDelSol;

    [SerializeField] private float rotacionDiaSol;
    private float _rotacionSolVelocidad;
    public bool enOleada = false;
    private void Start()
    {
        WaveSpawnerManager.Instance.IniciarOleada();
        _rotacionSolVelocidad = rotacionDiaSol;
    }
    private void Update()
    {
        if (enOleada)
            return;
        horaDelDia += Time.deltaTime * _rotacionSolVelocidad;
        if (horaDelDia > 24)
            horaDelDia = 0;
        if (EsDeDia())
        {
            _rotacionSolVelocidad = rotacionDiaSol;
            WaveSpawnerManager.Instance.ActivarSpawnSystem();
        }
        else
        {
            _rotacionSolVelocidad = sunRotationSpeedDeNocheADia;
        }


        ActualizarRotacionSol();
        ActualizarIluminacion();
    }

    [ContextMenu("AvanzarDiaParaZombies")]
    public void AvanzarDia()
    {
        horaDelDia = 20;
    }
    private bool EsDeDia()
    {
        return horaDelDia > 7 && horaDelDia < 20;
    }

    public bool HoraIndicadaParaZombies()
    {
        
        return horaDelDia > 20 || horaDelDia < 7;
    }

    private void OnValidate()
    {
        ActualizarRotacionSol();
        ActualizarIluminacion();
    }

    private void ActualizarRotacionSol()
    {
        float rotacionSol = Mathf.Lerp(-90, 270, horaDelDia / 24);
        //float porcentajeEnZombies = ((float)WaveSpawnerManager.Instance.EnemigosVivos / WaveSpawnerManager.Instance.ZombiesMaxPorRonda) * 100;
        //horaDelDia = (porcentajeEnZombies / 100) * 24;
        //horaDelDia = Mathf.Clamp(horaDelDia, 0, 24);
        sol.transform.rotation = Quaternion.Euler(rotacionSol, sol.transform.rotation.y, sol.transform.rotation.z);
    }

    private void ActualizarIluminacion()
    {
        float timeFraction = horaDelDia / 24;
        RenderSettings.ambientEquatorColor = colorDelEcuador.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = colorDelCielo.Evaluate(timeFraction);
        sol.color = colorDelSol.Evaluate(timeFraction);

    }
}

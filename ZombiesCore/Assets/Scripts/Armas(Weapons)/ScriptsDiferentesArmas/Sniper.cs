using System.Collections;
using TMPro;
using UnityEngine;

public class Sniper : DetallesArma
{
    public override void Atacar()
    {
        if (CurrentCharger <= 0) return;
        if (Recargando) return;
        CancelInvoke(nameof(RecargaAutomatica));
        if (!(Time.time > SiquienteDisparo)) return;
        SiquienteDisparo = Time.time + VelocidadDisparo;
        CursorManager.Instance.ActivarMira();
        _factoriaArmas.CrearBala(IdArma, _balaSpawnReference);
        CurrentCharger--;
        AudioManager.Instance.PlayAudio3D(audioDisparo, transform);
        UIManager.Instance.UpdateChargerAmmo(MaxCharger, CurrentCharger);
        Invoke(nameof(RecargaAutomatica), TiempoRecargaAutomatica);
        if (CurrentCharger <= 0)
        {
            StartCoroutine(nameof(Recargar));
        }
    }
    public override IEnumerator Recargar()
    {
        Debug.Log("recargando");
        Recargando = true;
        CursorManager.Instance.Reloading(TiempoRecarga);
        AudioManager.Instance.PlayAudio3D(audioRecargar, transform);
        yield return new WaitForSeconds(TiempoRecarga);
        if (MaxCharger >= CurrentAmmo)
        {
            CurrentCharger = CurrentAmmo;
            CurrentAmmo = 0;
        }
        else
        {
            var balasEnCargador = CurrentCharger;
            CurrentCharger = MaxCharger;
            CurrentAmmo -= CurrentCharger;
            CurrentAmmo += balasEnCargador;
        }
        UIManager.Instance.UpdateTotalAmmo(MaxAmmo, CurrentAmmo);
        UIManager.Instance.UpdateChargerAmmo(MaxCharger, CurrentCharger);
        SiquienteDisparo = Time.time + VelocidadDisparo;
        Recargando = false;
        Debug.Log("recargaCompletada");
        AudioManager.Instance.PlayAudio3D(audioRecargarCompleta, transform);
        CursorManager.Instance.DefaultCursor();
        CancelInvoke(nameof(RecargaAutomatica));
    }

    public override void RecargaAutomatica()
    {
        StartCoroutine(nameof(Recargar));
    }
}

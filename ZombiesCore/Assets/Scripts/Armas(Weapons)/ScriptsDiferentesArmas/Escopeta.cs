using System.Collections;
using TMPro;
using UnityEngine;

public class Escopeta : DetallesArma
{
    public override IEnumerator Recargar()
    {
        Debug.Log("recargando");
        Recargando = true;
        CursorManager.Instance.Reloading(TiempoRecarga);
        yield return new WaitForSeconds(TiempoRecarga);
        if (MaxBalasCargador >= ActualBalas)
        {
            ActualBalasCargador = ActualBalas;
            ActualBalas = 0;
        }
        else
        {
            var balasEnCargador = ActualBalasCargador;
            ActualBalasCargador = MaxBalasCargador;
            ActualBalas -= ActualBalasCargador;
            ActualBalas += balasEnCargador;
        }
        Municion.GetComponent<TextMeshProUGUI>().text = ActualBalas + "/" + MaxBalas;
        Cargador.GetComponent<TextMeshProUGUI>().text = ActualBalasCargador + "/" + MaxBalasCargador;
        SiquienteDisparo = Time.time + VelocidadDisparo;
        Recargando = false;
        Debug.Log("recargaCompletada");
        CursorManager.Instance.DefaultCursor();
        CancelInvoke(nameof(RecargaAutomatica));
    }

    public override void Atacar()
    {
        if (ActualBalasCargador <= 0) return;
        if (Recargando) return;
        CancelInvoke(nameof(RecargaAutomatica));
        if (!(Time.time > SiquienteDisparo)) return;
        SiquienteDisparo = Time.time + VelocidadDisparo;
        CursorManager.Instance.ActivarMira();
        Debug.Log("hola");
        for (int i = 0; i < Perdigones; i++)
        {
            Debug.Log("i");
            _factoriaArmas.CrearBala(IdArma, _balaSpawnReference);
        }
        ActualBalasCargador--;
        Cargador.GetComponent<TextMeshProUGUI>().text = ActualBalasCargador + "/" + MaxBalasCargador;
        Invoke(nameof(RecargaAutomatica), TiempoRecargaAutomatica);
        if (ActualBalasCargador <= 0)
        {
            StartCoroutine(nameof(Recargar));
        }
    }

    public override void RecargaAutomatica()
    {
        StartCoroutine(nameof(Recargar));
    }
}

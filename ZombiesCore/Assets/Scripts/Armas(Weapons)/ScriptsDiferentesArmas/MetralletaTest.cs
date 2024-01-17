using System.Collections;
using TMPro;
using UnityEngine;

public class MetralletaTest : DetallesArma
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

    public override void RecargaAutomatica()
    {
        StartCoroutine(nameof(Recargar));
    }
}
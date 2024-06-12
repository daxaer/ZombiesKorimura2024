using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class DatosTotalesManager : MonoBehaviour
{
    [SerializeField] private string urlActualizarDatosTotales = "http://localhost/zombie/actualizar_datos_totales.php";

    // Método para enviar los datos totales al servidor
    public void EnviarDatosTotales(int usuarioID, int partidasJugadas, int rondasJugadas, int rondaMaximaAlcanzada, int rondaMediaAlcanzada, TimeSpan tiempoTotalJugado, TimeSpan tiempoMedioJugado)
    {
        // Crear el formulario para enviar los datos
        WWWForm form = new WWWForm();
        form.AddField("IDUsuario", usuarioID);
        form.AddField("PartidasJugadas", partidasJugadas);
        form.AddField("RondasJugadas", rondasJugadas);
        form.AddField("RondaMaximaAlcanzada", rondaMaximaAlcanzada);
        form.AddField("RondaMediaAlcanzada", rondaMediaAlcanzada);
        form.AddField("TiempoTotalJugado", (int)tiempoTotalJugado.TotalSeconds);
        form.AddField("TiempoMedioJugado", (int)tiempoMedioJugado.TotalSeconds);

        // Enviar la solicitud al servidor para actualizar los datos totales del jugador
        StartCoroutine(EnviarSolicitudDatosTotales(urlActualizarDatosTotales, form));
    }

    // Corrutina para enviar la solicitud al servidor
    private IEnumerator EnviarSolicitudDatosTotales(string url, WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al enviar datos totales al servidor: " + www.error);
            }
            else
            {
                Debug.Log("Datos totales actualizados correctamente en el servidor.");
            }
        }
    }
}

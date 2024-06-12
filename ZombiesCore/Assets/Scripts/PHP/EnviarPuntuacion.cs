using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnviarPuntuacion : MonoBehaviour
{
    [SerializeField] private string urlGuardarPuntuacion = "http://localhost/zombie/guardar_puntuacion.php";

    // Método para enviar los datos de la partida al servidor
    public void GuardarPuntuacion(int idUsuario, int enemigosDerrotados, string duracionPartida, int rondaAlcanzada)
    {
        StartCoroutine(EnviarSolicitudGuardarPuntuacion(idUsuario, enemigosDerrotados, duracionPartida, rondaAlcanzada));
    }

    IEnumerator EnviarSolicitudGuardarPuntuacion(int idUsuario, int enemigosDerrotados, string duracionPartida, int rondaAlcanzada)
    {
        // Crear el formulario con los datos de la partida
        WWWForm form = new WWWForm();
        form.AddField("id_usuario", idUsuario);
        form.AddField("enemigos_derrotados", enemigosDerrotados);
        form.AddField("duracion_partida", duracionPartida);
        form.AddField("ronda_alcanzada", rondaAlcanzada);

        // Enviar la solicitud al servidor
        using (UnityWebRequest www = UnityWebRequest.Post(urlGuardarPuntuacion, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al guardar la puntuación: " + www.error);
            }
            else
            {
                Debug.Log("Puntuación guardada exitosamente");
                PuntuacionManager.Instance.SetTop3();
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GuardarPartida : MonoBehaviour
{
    [SerializeField] private string urlGuardarPartida = "http://localhost/zombie/guardar_partida.php";

    public void FinalizarPartida(int usuarioID, int rondas, string inicioPartida, string finalPartida, string tiempoJugado)
    {
        StartCoroutine(EnviarSolicitudGuardarPartida(usuarioID, rondas, inicioPartida, finalPartida, tiempoJugado));
    }

    IEnumerator EnviarSolicitudGuardarPartida(int usuarioID, int rondas, string inicioPartida, string finalPartida, string tiempoJugado)
    {
        WWWForm form = new WWWForm();
        form.AddField("UsuarioID", usuarioID);
        form.AddField("Rondas", rondas);
        form.AddField("InicioPartida", inicioPartida);
        form.AddField("FinalPartida", finalPartida);
        form.AddField("TiempoJugado", tiempoJugado);

        using (UnityWebRequest www = UnityWebRequest.Post(urlGuardarPartida, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al guardar la partida: " + www.error);
            }
            else
            {
                string respuesta = www.downloadHandler.text;
                Debug.Log("Respuesta del servidor al guardar la partida: " + respuesta);

                if (respuesta.StartsWith("Partida guardada"))
                {
                    Debug.Log("Partida guardada correctamente");
                }
                else
                {
                    Debug.LogError("Error al guardar la partida: " + respuesta);
                }
            }
        }
    }
}

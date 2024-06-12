using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class ObtenerPuntuacionesMaximas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] NombreUsuariostext;
    [SerializeField] private TextMeshProUGUI[] RondaMaximatext;
    [SerializeField] private TextMeshProUGUI[] EnemigosDerrotadostext;
    [SerializeField] private TextMeshProUGUI[] Tiempotext;

    [SerializeField] private string urlObtenerPuntuacionesMaximas = "http://localhost/zombie/obtener_puntuaciones_maximas.php";

    // Método para obtener las puntuaciones máximas del servidor
    public void ObtenerPuntuaciones()
    {
        StartCoroutine(EnviarSolicitudObtenerPuntuaciones());
    }

    IEnumerator EnviarSolicitudObtenerPuntuaciones()
    {
        // Enviar solicitud al servidor
        using (UnityWebRequest www = UnityWebRequest.Get(urlObtenerPuntuacionesMaximas))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener las puntuaciones máximas: " + www.error);
            }
            else
            {
                // Procesar la respuesta del servidor
                string respuesta = www.downloadHandler.text;
                Debug.Log("Respuesta del servidor: " + respuesta);

                // Dividir la respuesta en líneas
                string[] lineas = respuesta.Split('\n');

                // Iterar sobre las líneas y llenar los campos correspondientes
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    string[] datos = lineas[i].Split(';');
                    NombreUsuariostext[i].text = datos[0]; // Asignar el nombre de usuario
                    EnemigosDerrotadostext[i].text = datos[1]; // Asignar los enemigos derrotados
                    RondaMaximatext[i].text = datos[2]; // Asignar la ronda máxima
                    Tiempotext[i].text = datos[3]; // Asignar el tiempo
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ActualizarUltimaSesion : MonoBehaviour
{
    [SerializeField] private string urlActualizarSesion = "http://localhost/zombie/actualizar_ultima_sesion.php";

    public void ActualizarSesion(string nombreUsuario)
    {
        StartCoroutine(EnviarSolicitudActualizarSesion(nombreUsuario));
    }

    IEnumerator EnviarSolicitudActualizarSesion(string nombreUsuario)
    {
        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", nombreUsuario);

        using (UnityWebRequest www = UnityWebRequest.Post(urlActualizarSesion, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error actualizando última sesión: " + www.error);
            }
            else
            {
                string respuesta = www.downloadHandler.text;
                Debug.Log("Respuesta del servidor para actualización de última sesión: " + respuesta);

                if (respuesta.StartsWith("Última sesión actualizada"))
                {
                    int idUsuario = ObtenerIDUsuarioDeRespuesta(respuesta);
                    Debug.Log("Última sesión actualizada correctamente para el usuario: " + nombreUsuario + idUsuario);
                    // Obtener el IDUsuario de la respuesta
                    // Guardar el IDUsuario en PlayerPrefs
                    PlayerPrefs.SetInt("IDUsuario", idUsuario);
                }
                else
                {
                    Debug.LogError("Error en la respuesta del servidor: " + respuesta);
                }
            }
        }
    }

    // Método para obtener el IDUsuario de la respuesta del servidor
    private int ObtenerIDUsuarioDeRespuesta(string respuesta)
    {
        // Aquí debes implementar la lógica para extraer el IDUsuario de la respuesta
        // Por ejemplo, si la respuesta tiene el formato "Última sesión actualizada: [fecha], IDUsuario: [id]", 
        // puedes dividir la cadena y obtener el IDUsuario.
        // Aquí se asume que el IDUsuario está al final de la cadena después de "IDUsuario: "
        string[] partes = respuesta.Split(':');
        if (partes.Length >= 2)
        {
            int idUsuario;
            if (int.TryParse(partes[2], out idUsuario))
            {
                return idUsuario;
            }
        }
        return -1; // Retornar -1 si no se puede obtener el IDUsuario
    }
}

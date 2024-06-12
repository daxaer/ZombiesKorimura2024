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
                Debug.LogError("Error actualizando �ltima sesi�n: " + www.error);
            }
            else
            {
                string respuesta = www.downloadHandler.text;
                Debug.Log("Respuesta del servidor para actualizaci�n de �ltima sesi�n: " + respuesta);

                if (respuesta.StartsWith("�ltima sesi�n actualizada"))
                {
                    int idUsuario = ObtenerIDUsuarioDeRespuesta(respuesta);
                    Debug.Log("�ltima sesi�n actualizada correctamente para el usuario: " + nombreUsuario + idUsuario);
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

    // M�todo para obtener el IDUsuario de la respuesta del servidor
    private int ObtenerIDUsuarioDeRespuesta(string respuesta)
    {
        // Aqu� debes implementar la l�gica para extraer el IDUsuario de la respuesta
        // Por ejemplo, si la respuesta tiene el formato "�ltima sesi�n actualizada: [fecha], IDUsuario: [id]", 
        // puedes dividir la cadena y obtener el IDUsuario.
        // Aqu� se asume que el IDUsuario est� al final de la cadena despu�s de "IDUsuario: "
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

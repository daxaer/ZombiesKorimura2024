using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ObtenerIDUsuario : MonoBehaviour
{
    [SerializeField] private string urlObtenerIDUsuario = "http://localhost/zombie/obtener_id_usuario.php";
    public void Awake()
    {
        ObtenerID(PlayerPrefs.GetString("NombreUsuario")); 
    }
    public void ObtenerID(string nombreUsuario)
    {
        StartCoroutine(EnviarSolicitudObtenerID(nombreUsuario));
    }

    IEnumerator EnviarSolicitudObtenerID(string nombreUsuario)
    {
        // Crear formulario para enviar el nombre de usuario al servidor
        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", nombreUsuario);

        // Enviar la solicitud al servidor
        using (UnityWebRequest www = UnityWebRequest.Post(urlObtenerIDUsuario, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Error al enviar la solicitud
                Debug.LogError("Error al obtener ID del usuario: " + www.error);
            }
            else
            {
                // ID del usuario obtenido correctamente
                string idUsuarioTexto = www.downloadHandler.text;
                
                Debug.Log("ID del usuario: " + idUsuarioTexto);

                if (int.TryParse(idUsuarioTexto, out int idUsuario))
                {
                    // Almacenar el ID del usuario en PlayerPrefs
                    PuntuacionManager.Instance.SetUsuarioID(idUsuario);
                    PlayerPrefs.SetInt("IDUsuario", idUsuario);
                    Debug.Log(PlayerPrefs.GetInt("IDUsuario"));
                }
                else
                {
                    Debug.LogError("No se pudo convertir el ID del usuario a entero.");
                }
            }
        }
    }
}

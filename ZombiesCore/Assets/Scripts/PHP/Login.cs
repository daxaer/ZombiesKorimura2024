using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField nombreUsuarioInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private GameObject panelMensajes;
    [SerializeField] private TextMeshProUGUI mensajeText;
    [SerializeField] private TextMeshProUGUI nombreUsuarioUI;
    [SerializeField] private string urlLogin = "http://localhost/zombie/login.php";
    [SerializeField] private GameObject inicioSesion;
    [SerializeField] private GameObject CerrarSesion;
    [SerializeField] ActualizarUltimaSesion actualizar;

    private string nombreUsuario; // Guardará el nombre del usuario

    public void IniciarSesion()
    {
        string nombreUsuario = nombreUsuarioInput.text;
        string password = passwordInput.text;

        StartCoroutine(EnviarSolicitudLogin(nombreUsuario, password));
    }

    IEnumerator EnviarSolicitudLogin(string nombreUsuario, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", nombreUsuario);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(urlLogin, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                mensajeText.text = "Error de conexión: " + www.error;
            }
            else
            {
                string respuesta = www.downloadHandler.text;
                Debug.Log("Respuesta del servidor: " + respuesta);

                if (respuesta.StartsWith("Login exitoso"))
                {
                    // Obtener el nombre de usuario desde la respuesta
                    string[] partes = respuesta.Split(':');
                    if (partes.Length >= 2)
                    {
                        nombreUsuario = partes[1].Trim();

                        // Guardar el valor de inicio de sesión en PlayerPrefs
                        PlayerPrefs.SetString("NombreUsuario", nombreUsuario);
                        PlayerPrefs.SetInt("LoggedIn", 1); // 1 para indicar que está logueado
                        PlayerPrefs.Save();

                        mensajeText.text = "Inicio de sesión exitoso. Usuario: " + nombreUsuario;
                        nombreUsuarioUI.text = "Usuario: " + nombreUsuario;
                        actualizar.ActualizarSesion(PlayerPrefs.GetString("NombreUsuario"));
                        inicioSesion.SetActive(false);
                        CerrarSesion.SetActive(true);
                    }
                    else
                    {
                        mensajeText.text = "Error al procesar la respuesta del servidor.";
                    }
                }
                else
                {
                    mensajeText.text = respuesta;
                }
            }
        }
        StartCoroutine("AbrirMenu");
    }
    IEnumerator AbrirMenu()
    {
        panelMensajes.SetActive(true);
        yield return new WaitForSeconds(3f);
        panelMensajes.SetActive(false);
    }
}

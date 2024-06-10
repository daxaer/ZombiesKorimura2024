using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Networking;

public class RegistroUsuario : MonoBehaviour
{
    [SerializeField] private TMP_InputField nombreUsuarioInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField edadInput;
    [SerializeField] private GameObject panelError;
    [SerializeField] private TextMeshProUGUI mensajeText;
    [SerializeField] private string urlRegistro = "http://zombies.atwebpages.com/zombies/registro.php";

    public void RegistrarUsuario()
    {
        string nombreUsuario = nombreUsuarioInput.text;
        string password = passwordInput.text;
        string edad = edadInput.text;
        StartCoroutine(EnviarRegistro(nombreUsuario, password, edad));
    }

    IEnumerator EnviarRegistro(string nombreUsuario, string password, string edad)
    {
        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", nombreUsuario);
        form.AddField("password", password);
        form.AddField("edad", edad);

        using (UnityWebRequest www = UnityWebRequest.Post(urlRegistro, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                panelError.GetComponent<Image>().color = Color.red;
                Debug.LogError("Error de conexión: " + www.error);
            }
            else
            {
                panelError.GetComponent<Image>().color = Color.green;
                mensajeText.text = www.downloadHandler.text;
            }
        }
    }
}

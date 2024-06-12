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
    [SerializeField] private string urlRegistro = "http://localhost/zombie/registrar_usuario.php";
    private bool usuarioregistrado;

    public void RegistrarUsuario()
    {
        string nombreUsuario = nombreUsuarioInput.text;
        string password = passwordInput.text;
        string edad = edadInput.text;

        if (!int.TryParse(edad, out int age))
        {
            mensajeText.text = "Error: La edad debe ser un numero.";
            return;
        }

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
                mensajeText.text = "Error de conexi�n: " + www.error;
                StartCoroutine("Mensaje");
            }
            else
            {
                mensajeText.text = www.downloadHandler.text;
                usuarioregistrado = true;
                StartCoroutine("Mensaje");
            }
        }
    }
    IEnumerator Mensaje()
    {
       
        panelError.SetActive(true);
        yield return new WaitForSeconds(5f);
        panelError.SetActive(false);
        if (usuarioregistrado)
        {
            this.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private InputField nombreUsuarioInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private TextMeshPro respuesta;

    public void EnviarFormulario()
    {
        string nombreUsuario = nombreUsuarioInput.text;
        string password = passwordInput.text;

        StartCoroutine(EnviarDatosAlServidor(nombreUsuario, password)); 
    }

    IEnumerator EnviarDatosAlServidor(string nombreUsuario, string password) 
    {
        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", nombreUsuario);
        form.AddField("password", password);

        WWW www = new WWW("http://zombies.atwebpages.com/zombies/Login.php", form);

        yield return www;

        if (www.error != null)
        {
            respuesta.text = "Error de conexión: " + www.error;
        }
        else
        {
            respuesta.text = www.text;
        }
    }
}

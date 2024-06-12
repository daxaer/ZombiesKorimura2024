using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogOut : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _usuario;
    [SerializeField] GameObject _login;
    [SerializeField] GameObject _logOut;
    [SerializeField] ActualizarUltimaSesion actualizar;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("LoggedIn") == 1)
        {
            _usuario.text = "Usuario: " + PlayerPrefs.GetString("NombreUsuario");
            actualizar.ActualizarSesion(PlayerPrefs.GetString("NombreUsuario"));
            _login.SetActive(false);
            _logOut.SetActive(true);
        }
        else
        {
            _login.SetActive(true);
            _logOut.SetActive(false);
        }
    }
    public void CerrarSesion()
    {
        PlayerPrefs.SetInt("UserID", 0);
        PlayerPrefs.SetString("NombreUsuario", " ");
        PlayerPrefs.SetInt("LoggedIn", 0); // 1 para indicar que está logueado
        _usuario.text = "";
        PlayerPrefs.Save();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] private Image image;
    [SerializeField] private Color color;
    [SerializeField] private Texture2D[] _cursorTexture;
    [SerializeField] private Texture2D[] _recharge;
    [SerializeField] private float _timeAnimation;
    private bool _recargando;
    public bool Recargando { get { return _recargando; } set { _recargando = value; } }
    public LayerMask LayerSuelo;

    private void Start()
    {
        Cursor.SetCursor(_cursorTexture[0], new Vector2(16, 16), CursorMode.Auto);
    }

    public void ActivarMira()
    {
        if (!_recargando)
        {
            Cursor.SetCursor(_cursorTexture[1], new Vector2(16, 16), CursorMode.Auto);
        }
        Invoke("DefaultCursor", 0.1f);
    }
    public void Reloading(float recarga)
    {
        StartCoroutine(AnimacionRecarga(recarga));
    }
    public void DefaultCursor()
    {
        if(!_recargando)
        {
            Cursor.SetCursor(_cursorTexture[0], new Vector2(16, 16), CursorMode.Auto);
        }
    }

    private IEnumerator AnimacionRecarga(float recarga)
    {
        _recargando = true;
        for (int i = 0; i < _recharge.Length; i++)
        {
            Cursor.SetCursor(_recharge[i], new Vector2(16, 16), CursorMode.Auto);
            yield return new WaitForSeconds(recarga/_recharge.Length);
            if(i == _recharge.Length - 1)
            {
                _recargando = false;
                DefaultCursor();
                break;
            }
        }
    }

    public void Damage(int vida)
    {
        switch(vida)
        {
            case 2:
                color.a = 0.3f;
                break;
            case 1:
                color.a = 0.6f;
                break;
            case 0:
                color.a = 1f;
                break;
            default:
                color.a = 0f;
                break;
        }

        image.color = color;
    }
}


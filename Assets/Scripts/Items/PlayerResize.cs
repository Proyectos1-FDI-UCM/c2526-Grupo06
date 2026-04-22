//---------------------------------------------------------
// Componente del player que gestiona el powerup de la reducción de tamaño
// Alejandro de Haro
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerResize : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float _smallTime = 5f;  //tiempo máximo 
    [SerializeField]
    private float _smallSize = 0.75f;  //cuanto de pequeño se hace

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _timerActive = 0f;  //timer que mide cuanto lleva activo
    private bool _isSmall = false;  //estado en el que se encuentra el player

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Update()
    {
        // El temporizador avanza si el jugador es pequeño
        if (_isSmall)
        {
            _timerActive += Time.deltaTime;
            if (_timerActive > _smallTime)
            {
                DisableResize();
            }
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// reinicia el timer para ser stackeable y empequeñece al jugador
    /// </summary>
    public void EnableResize()
    {
        _isSmall = true;
        _timerActive = 0f;
        Vector3 _scaleChange = new Vector3(_smallSize, _smallSize, _smallSize);
        transform.localScale = _scaleChange;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    /// <summary>
    /// Lo desactiva
    /// </summary>
    private void DisableResize()
    {
        _isSmall = false;
        Vector3 _scaleReset = new Vector3(1, 1, 1);
        transform.localScale = _scaleReset;
    }
    #endregion   

} // class PlayerResize 
// namespace

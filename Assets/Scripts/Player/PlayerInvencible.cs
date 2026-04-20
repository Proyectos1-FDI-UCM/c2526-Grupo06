//---------------------------------------------------------
// Hacemos al player "invencible" durante un tiempo determinado después de chocar con un enemigo
// (Add-on Pablo Redondo Vaillo) tambien hace que brille durante la duracion, adicion estetica
// Javier de Sala Rodríguez
// Dream o' SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerInvencible : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float TiempoInvencible = 3f;
    /// <summary>
    /// Sprite renderer del jugador para hacer cambios en este cuando es invencible
    /// </summary>
    [SerializeField]
    private SpriteRenderer PlayerSprite;
    /// <summary>
    /// Tiempo entre brillo y no brillo cuando el componente está activado
    /// </summary>
    [SerializeField]
    private float FlickerTime = 0.3f;
    /// <summary>
    /// Color del brillo
    /// </summary>
    [SerializeField]
    private Color FlickerColor = new Color(0, 0, 0, 150);

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _tiempo;
    /// <summary>
    /// Tiempo del ultimo parpadeo
    /// </summary>
    private float _lastFlicker;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        if (PlayerSprite == null) { Debug.LogWarning("No se ha asignado sprite renderer al playerinvencible"); }
    }
    private void OnEnable()
    {

        // Resetea el contador del parpadeo 
        if (PlayerSprite != null) { _lastFlicker = 0f; }
        // Resetea el contador de invencibilidad
        _tiempo = 0f;
    }
    private void OnDisable()
    {
        // Devuelve el color original al player
        if (PlayerSprite != null) { PlayerSprite.color = Color.white; }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Añade tiempo a la invincibilidad
        _tiempo += Time.deltaTime;
        // Condición de terminado
        if (_tiempo >= TiempoInvencible)
        {
            // Desactiva el componente
            this.enabled = false;
        }
        // Añade tiempo al flicker si hay sprite adjuntado
        else if (PlayerSprite != null)
        {
            _lastFlicker += Time.deltaTime;
            if (_lastFlicker > FlickerTime)
            {
                // Intercambia entre el color deseado y el original
                if (PlayerSprite.color == Color.white) { PlayerSprite.color = FlickerColor; }
                else { PlayerSprite.color = Color.white; }
                // Quita el tiempo para el siguiente cambio
                _lastFlicker -= FlickerTime;
            }
        }

    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion

} // class PlayerInvencible 
// namespace

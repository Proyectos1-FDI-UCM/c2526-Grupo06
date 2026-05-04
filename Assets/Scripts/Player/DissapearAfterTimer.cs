//---------------------------------------------------------
// Componente que hace que un objeto desaparezca x segundos tras ser instanciado
// Pablo Redondo Vaillo
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Hace que un objeto desaparezca x segundos despues de ser activado
/// </summary>
public class DissapearAfterTimer : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    /// <summary>
    /// Tiempo que va a estar presente el objeto antes de desaparecer
    /// </summary>
    [SerializeField]
    private float DissapearTime = 3f;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    /// <summary>
    /// Tiempo que lleva activo el objeto
    /// </summary>
    private float _currentTime = 0f;
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        _currentTime = 0f;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Añade tiempo al contador
        _currentTime += Time.deltaTime;
        // Si se sobrepasa, desactiva el objeto
        if ( _currentTime >= DissapearTime )
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion   

} // class DissapearAfterTimer 
// namespace

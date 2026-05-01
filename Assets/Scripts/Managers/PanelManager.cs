//---------------------------------------------------------
// Script para poder seguir usando el mando aunque se cambie de panel
// Sergio Navarro Herreros
// Dream O' Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PanelManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] 
    private GameObject opcionesFirstSelected;
    [SerializeField]
    private GameObject principalFirstSelected;
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private GameObject gameOverFirstSelected;
    [SerializeField]
    private GameObject panelVictoria;
    [SerializeField]
    private GameObject victoriaFirstSelected;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void AbrirPanelOpciones()
    {
        // Limpiar la selección actual
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opcionesFirstSelected);
        // Activar el panel
        gameObject.SetActive(true);
    }
    public void CerrarPanelOpciones()
    {
        // Limpiar la selección actual
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(principalFirstSelected);

        // Desactivar el panel
        gameObject.SetActive(true);
    }
    public void PanelGameOver()
    {
        if (panelGameOver != null)
        {
            // Limpiar la selección actual
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("Estoy en el boton que me toca");
            EventSystem.current.SetSelectedGameObject(gameOverFirstSelected);
        }
    }
    public void PanelVictoria()
    {
        if (panelVictoria != null)
        {
            // Limpiar la selección actual
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(victoriaFirstSelected);
        }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class PanelManager 
// namespace

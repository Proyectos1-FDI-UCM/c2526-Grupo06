//---------------------------------------------------------
// Sistema que controla los elementos del fondo.
// Adán Calvo Durán
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class AnimatedBackgroundSystem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] Transform BackgroundPage1;
    [SerializeField] Transform BackgroundPage2;
    [SerializeField] SpriteRenderer BackgroundSprite1;
    [SerializeField] SpriteRenderer BackgroundSprite2;
    [SerializeField] Transform EndBackground;
    [SerializeField] Sprite[] BackgroundVariations;
    [SerializeField] float BackgroundSpeed = 3;
    [SerializeField] float PositionZ = 5;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private float _spawnHeight = -5;
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
    void Start()
    {
        BackgroundPage1.position = new Vector3(0, 0, PositionZ);
        BackgroundPage2.position = new Vector3(BackgroundPage1.position.x + 20, _spawnHeight, PositionZ);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (BackgroundPage1.position.x < EndBackground.position.x)
        {
            BackgroundPage1.position = new Vector3(BackgroundPage2.position.x + 20, _spawnHeight, PositionZ);
            BackgroundSprite1.sprite = BackgroundVariations[Random.Range(0, BackgroundVariations.Length)];
        }
        if (BackgroundPage2.position.x < EndBackground.position.x)
        {
            BackgroundPage2.position = new Vector3(BackgroundPage1.position.x + 20, _spawnHeight, PositionZ);
            BackgroundSprite2.sprite = BackgroundVariations[Random.Range(0, BackgroundVariations.Length)];
        }
        BackgroundPage1.position += new Vector3(-BackgroundSpeed * Time.deltaTime, BackgroundSpeed/(4) * Time.deltaTime, 0);
        BackgroundPage2.position += new Vector3(-BackgroundSpeed * Time.deltaTime, BackgroundSpeed/(4) * Time.deltaTime, 0);
        Debug.Log(1/2 * BackgroundSpeed);
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class AnimatedBackgroundSystem 
// namespace

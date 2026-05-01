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
/// Sistema que controla los elementos del fondo.
/// </summary>
public class AnimatedBackgroundSystem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
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
    private float _spawnHeight = -5;
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
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
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion   

} // class AnimatedBackgroundSystem 
// namespace

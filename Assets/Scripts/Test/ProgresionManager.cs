//---------------------------------------------------------
// Archivo que contendra la estructura del nivel
// Adán Calvo Durán
// Dream O’ SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class ProgresionManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    GameObject LitleBoy;
    [SerializeField]
    GameObject HugeCore;

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
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void SpawnLitleBoy()
    {
        Debug.Log("Trying to spawn boy");
        if (LitleBoy != null) if (GameManager.Instance != null) GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(0, 0), 0);
        else Debug.Log("LitleBoy Prefab Mising");  
    }

    public void SpawnHugeCore()
    {
        Debug.Log("Trying to spawn core");
        if (HugeCore != null) if (GameManager.Instance != null) GameManager.Instance.EnemigoSpawn(HugeCore, 1, new Vector2(0, 0), 0);
            else Debug.Log("HugeCore Prefab Mising");
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class ProgresionManager 
// namespace

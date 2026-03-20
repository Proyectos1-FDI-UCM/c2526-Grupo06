//---------------------------------------------------------
// Archivo que contendra la estructura del nivel
// Adán Calvo Durán
// Dream O’ SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using
using System.Collections;

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
    [SerializeField]
    int XPositionEnemySpawn;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private int _enemyAmount = 0;
    private float _timer = 0f;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start, it's called once at the start
    /// </summary>
    private void Start()
    {
        if (GameManager.Instance != null) StartCoroutine(LevelSequence());
        else Debug.Log("Missing GameManager to start level");

    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController


    public void ReduceEnemyCount(int amount)
    {
        _enemyAmount -= amount;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private IEnumerator LevelSequence()
    {
        //Horda 1
        while (_timer < 3)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, -1), 0);
        _enemyAmount++;
        _timer = 0f;

        //Horda 2
        while (_timer < 7 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 3), 0);
        _enemyAmount++;
        _timer = 0f;

        //Horda 3
        while (_timer < 7 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 2, new Vector2(XPositionEnemySpawn, 0), 3f);
        _enemyAmount += 2;
        _timer = 0f;

        //Horda 4
        while (_timer < 15 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(HugeCore, 1, new Vector2(XPositionEnemySpawn, 0),0);
        _enemyAmount++;
        _timer = 0f;

        //Horda 5
        while (_timer < 20 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 3), 0);
        _enemyAmount++;
        _timer = 0f;

        //Horda 6
        while (_timer < 3 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 1), 0);
        _enemyAmount++;
        _timer = 0f;

        //Horda 7
        while (_timer < 3 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, -4), 0);
        _enemyAmount++;
        _timer = 0f;

        //Horda 8
        while (_timer < 12 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 4, new Vector2(XPositionEnemySpawn, 0), 3.5f);
        _enemyAmount += 4;
        _timer = 0f;

        //Horda 8
        while (_timer < 7 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 1), 2f);
        _enemyAmount++;
        _timer = 0f;

        //Horda 9
        while (_timer < 7 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 3, new Vector2(XPositionEnemySpawn, -1), 2.5f);
        _enemyAmount += 3;
        _timer = 0f;

        //Horda 10
        while (_timer < 15 && _enemyAmount > 0)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, -3), 0);
        GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 3), 0);
        GameManager.Instance.EnemigoSpawn(HugeCore, 1, new Vector2(XPositionEnemySpawn, -1), 0);
        GameManager.Instance.EnemigoSpawn(HugeCore, 1, new Vector2(XPositionEnemySpawn, 1), 0);
        _enemyAmount += 4;
        _timer = 0f;
    }
    #endregion

} // class ProgresionManager 
// namespace

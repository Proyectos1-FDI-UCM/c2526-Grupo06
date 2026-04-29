//---------------------------------------------------------
// Archivo que contendra la estructura del nivel
// Adán Calvo Durán
// Dream O’ SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using
using System.Collections;
using System.Runtime.CompilerServices;
using System.Diagnostics;

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
    [SerializeField]
    GameObject Boss;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private int _enemyAmount = 0; // Variable que controla la cantidad de enemigos en pantalla para adelantar las hordas en caso de que no queden enemigos
    private float _timer = 0f; // Variable que controla el tiempo para que las oleadas puedan generarse segun el tiempo
    private bool _stoped = false; // Variable con la funcion de ponerse en true cuando la generacion de oleadas deba detenerse
    private bool _start = false; // Variable que en caso de que haya una instancia de game manager permite el funcionamiento del script
    private int _numHorde = 0; //Variable que cuenta cuantas hordas han pasado
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start, it's called once at the start
    /// </summary>
    private void Start()
    {
        if (GameManager.Instance != null) _start = true;
    }
    private void Update()
    {
        if (_start && !_stoped)
        {
            _timer += Time.deltaTime;
            CheckSpawn();
        }
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
        //UnityEngine.Debug.Log(_enemyAmount.ToString());
    }

    public void StopHordeSpawning(bool stop)
    {
        _stoped = stop;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private void CheckSpawn()
    {
        switch (_numHorde)
        {
            case 0:
                //Horda 1
                if (_timer > 1.5f)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, -1), 0);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 1:
                //Horda 2
                if (_timer > 7 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 3), 0);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 2:
                //Horda 3
                if (_timer > 7 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 2, new Vector2(XPositionEnemySpawn, 0), 3f);
                    _enemyAmount += 2;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 3:
                //Horda 4
                if (_timer > 20 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(HugeCore, 1, new Vector2(XPositionEnemySpawn, 0), 0);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 4:
                //Horda 5
                if (_timer > 20 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 2), 1f);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 5:
                //Horda 6
                if (_timer > 3 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 1), 0);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 6:
                //Horda 7
                if (_timer > 3 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, -4), 0);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 7:
                //Horda 8
                if (_timer > 12 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 4, new Vector2(XPositionEnemySpawn, 0), 3.5f);
                    _enemyAmount += 4;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 8:
                //Horda 9
                if (_timer > 7 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 1), 2f);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 9:
                //Horda 10
                if (_timer > 7 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 3, new Vector2(XPositionEnemySpawn, -1), 2.5f);
                    _enemyAmount += 3;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 10:
                //Horda 11
                if (_timer > 20 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, -3), 0);
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 3), 0);
                    GameManager.Instance.EnemigoSpawn(HugeCore, 1, new Vector2(XPositionEnemySpawn, -1), 0);
                    _enemyAmount += 3;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 11:
                //Horda 12
                if (_timer > 12 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, 0), 1f);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 12:
                //Horda 13
                if (_timer > 4 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 1, new Vector2(XPositionEnemySpawn, -2), 1f);
                    _enemyAmount++;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 13:
                //Horda 14
                if (_timer > 5 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 3, new Vector2(XPositionEnemySpawn, 0), 3.5f);
                    _enemyAmount += 3;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 14:
                //Horda 15
                if (_timer > 10 || _enemyAmount <= 0)
                {
                    GameManager.Instance.EnemigoSpawn(HugeCore, 1, new Vector2(XPositionEnemySpawn, 0), 2f);
                    GameManager.Instance.EnemigoSpawn(LitleBoy, 2, new Vector2(XPositionEnemySpawn, 0), 3f);
                    _enemyAmount += 3;
                    _timer = 0f;
                    _numHorde++;
                }
                break;
            case 15:
                //Horda 16
                if (_enemyAmount <= 0)
                {
                    //Detiene la música del nivel y activa las tres del boss, pero solo con el volumen de la primera
                    SoundEffectsManager.instance.StopMusic(0);
                    SoundEffectsManager.instance.PlayMusic(1,1f,1);
                    SoundEffectsManager.instance.PlayMusic(2, 0f, 2);
                    SoundEffectsManager.instance.PlayMusic(3, 0f, 3);

                    //Metodo para empezar la boss fight
                    Boss.SetActive(true);
                    //Para que la condición de victoria no toma en cuenta un boss
                    //GameManager.Instance.MostrarVictory();
                    _stoped = true;
                }
                break;
            default:
            break;
        }
    }
    #endregion


} // class ProgresionManager 
// namespace

//---------------------------------------------------------
// Componente del player que gestiona el powerup de escudo
// Pablo Redondo Vaillo
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using UnityEngine;
// Añadir aquí el resto de directivas using


public class PlayerShield : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float ShieldMaxTime = 5f; // Tiempo máximo que el jugador puede permanecer con el escudo
    [SerializeField]
    private GameObject BulletDestroyer; // Limpiador de pantalla por si es dañado con el escudo
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _shieldTime = 0; // Temporizador del escudo
    private bool _shieldEnabled = false; // Estado del escudo 

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        if (BulletDestroyer == null) { Debug.LogWarning("No se ha asignado un screen cleaner para el escudo"); }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // El temporizador avanza si el escudo está activo
        if (_shieldEnabled)
        {
            _shieldTime += Time.deltaTime; 
            if (_shieldTime > ShieldMaxTime)
            {
                DisableShield(); // Desactiva el escudo si sobrepasa el tiempo
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Reinicia el temporizador del escudo del jugador y lo habilita
    /// </summary>
    public void EnableShield()
    {
        _shieldTime = 0;
        _shieldEnabled = true;   
    }
    /// <summary>
    /// Deshabilita el escudo del jugador sin hacer el ataque que limpia las balas enemigas
    /// </summary>
    public void DisableShield()
    {
        _shieldEnabled = false;
    }
    /// <summary>
    /// Activa el limpiador de balas enemigas cuando el jugador es atacado con el escudo
    /// Luego desactiva el escudo
    /// </summary>
    public void ShieldAttack()
    {
        _shieldEnabled = false; // Desactiva el escudo
        if (BulletDestroyer != null) // Programación defensiva
        {
            // Establece la posición del screen cleaner en la posición actual del jugador
            BulletDestroyer.transform.position = transform.position;
            // Activa el screen cleaner
            BulletDestroyer.SetActive(true);
        }
    }
    /// <summary>
    /// Devuelve el estado del escudo del jugador (activo/no activo)
    /// </summary>
    /// <returns></returns>
    public bool GetShieldState()
    {
        return _shieldEnabled; 
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class PlayerShield 
// namespace

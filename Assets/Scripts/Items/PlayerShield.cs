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
    /// <summary>
    /// Tiempo máximo que el jugador puede permanecer con el escudo
    /// </summary>
    [SerializeField]
    private float ShieldMaxTime = 5f;
    /// <summary>
    /// Tiempo restante en el que el escudo empieza a parpadear
    /// </summary>
    [SerializeField]
    private float ShieldFlickerTime = 1f;
    /// <summary>
    /// Limpiador de pantalla por si es dañado con el escudo
    /// </summary>
    [SerializeField]
    private GameObject BulletDestroyer;
    /// <summary>
    /// Sprite del escudo
    /// </summary>
    [SerializeField]
    private SpriteRenderer ShieldSprite;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _shieldTime = 0; // Temporizador del escudo
    private bool _shieldEnabled = false; // Estado del escudo 
    private const float _flickerInterval = 0.1f; // Intervalo entre activa-desactiva cuando el escudo parpadea
    private float _lastFlicker = 0f;

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
        if (ShieldSprite == null) { Debug.LogWarning("No se ha asignado un sprite para el escudo"); }
        if (ShieldFlickerTime>ShieldMaxTime) { ShieldFlickerTime=ShieldMaxTime; }
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
            // Parpadeo del escudo
            if (ShieldMaxTime - _shieldTime < ShieldFlickerTime)
            {
                // Añade tiempo al timer
                _lastFlicker += Time.deltaTime;
                if (_lastFlicker > _flickerInterval)
                {
                    if (ShieldSprite != null) { ShieldSprite.enabled = !ShieldSprite.enabled; }
                    _lastFlicker -= _flickerInterval;
                }
            }
            // Cuando el tiempo se acaba
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
        if (ShieldSprite != null) { ShieldSprite.enabled = true; }
        _shieldTime = 0;
        _shieldEnabled = true;   
    }
    /// <summary>
    /// Deshabilita el escudo del jugador sin hacer el ataque que limpia las balas enemigas
    /// </summary>
    public void DisableShield()
    {
        if (ShieldSprite != null) { ShieldSprite.enabled = false; } 
        _shieldEnabled = false;
    }
    /// <summary>
    /// Activa el limpiador de balas enemigas cuando el jugador es atacado con el escudo
    /// Luego desactiva el escudo
    /// </summary>
    public void ShieldAttack()
    {
        if (ShieldSprite != null) { ShieldSprite.enabled = false; }
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
    #endregion

} // class PlayerShield 
// namespace

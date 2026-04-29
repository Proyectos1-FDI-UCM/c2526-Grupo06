//---------------------------------------------------------
// Gestionar el sistema de vida del player
// Javier de Sala Rodríguez
// Dream O' SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Clase vida, maneja los puntos de vida del jugador y los casos en los que pueden cambiar
/// </summary>
public class Vida : MonoBehaviour
{



    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)


    [SerializeField]
    private int Vidas = 4; //declaramos cuantas vidas tendremos al inicio de la partida

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private const int _maximoVidas = 6; //el máximo de vidas que puede tener el jugador
    /// <summary>
    /// Componente animator del player para la animación de hacerse daño
    /// </summary>
    private Animator _animator;
    private PlayerInvencible _scriptInvencible;
    private PlayerShield _scriptShield;
    private bool _debugInmortalidad = false;
    /// <summary>
    /// Booleano que controla las animaciones secretas del player
    /// </summary>
    private bool _secretAnimations = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        _scriptInvencible = GetComponent<PlayerInvencible>();
        _scriptShield = GetComponent<PlayerShield>();
        _animator = GetComponent<Animator>();
        if (GameManager.HasInstance())
        {
            GameManager.Instance.MuestraVida(Vidas);
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public bool ActualizarVidas(int delta)
    {
        // Mecánica escudo, solo se activa si se hace daño al jugador
        if (_scriptShield != null && delta < 0) // +Programación defensiva
        {
            // Pregunta si tiene el escudo activo 
            if (_scriptShield.GetShieldState()) 
            {
                // Hace el screen clean del escudo
                _scriptShield.ShieldAttack(); 
                return false;
            }
        }
        if (delta < 0 && _scriptInvencible != null && _scriptInvencible.enabled)
        {
            return false; // ignoramos daño
        }
        if (!_debugInmortalidad) Vidas += delta; // aplicar vidaa
        if (Vidas > _maximoVidas)
        {
            Vidas = _maximoVidas;
        }

        if (GameManager.HasInstance())
        {
            GameManager.Instance.MuestraVida(Vidas);
        }

        if (Vidas <= 0)
        {
            if (GameManager.HasInstance()) GameManager.Instance.MostrarGameOver();
            gameObject.SetActive(false);
        }

        else if (delta < 0)
        {
            if (_scriptInvencible != null)
            {
                _scriptInvencible.enabled = true;
            }
            // Comienza animación de daño
            if (_animator != null)
            {
                if(_secretAnimations) { _animator.SetTrigger("SecretHurtTrigger"); }
                else _animator.SetTrigger("HurtTrigger");
            }
        }
        return true;
    }
    public void SwapInmortalDebug()
    {
        _debugInmortalidad = !_debugInmortalidad;
    }
    /// <summary>
    /// Cambia el booleano de animaciones secretas on/off
    /// </summary>
    public void ChangeSecret()
    {
        _secretAnimations = !_secretAnimations;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion   

} // class Vida 
// namespace

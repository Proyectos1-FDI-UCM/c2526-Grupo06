//---------------------------------------------------------
// Gestionar el sistema de vida del player
// Javier de Sala Rodríguez
// Dream o' SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
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

    private PlayerInvencible _scriptInvencible;
    private PlayerShield _scriptShield;

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

        Vidas += delta; // aplicar vidaa

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
            Time.timeScale = 0;
            gameObject.SetActive(false);
        }

        else if (delta < 0)
        {
            if (_scriptInvencible != null)
            {
                _scriptInvencible.enabled = true;
            }
        }
        return true;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion   

} // class Vida 
// namespace

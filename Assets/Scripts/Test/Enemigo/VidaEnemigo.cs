//---------------------------------------------------------
// Script con la vida de los enemigos
// Sergio Navarro Herreros
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class VidaEnemigo : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private int PuntosVidaMaximos = 1; // Puntos de vida máximos del enemigo

    [SerializeField]
    private GameObject[] PowerUps; // lista de power ups posibles

    [SerializeField, Range(0, 100)]
    private float ProbabilidadDrop = 50f; // Probabilidad en % de que el enemigo suelte un power up al morir

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private int _puntosVidaRestantes; // Puntos de vida restantes del enemigo

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
        _puntosVidaRestantes = PuntosVidaMaximos;
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController
    public void RecibeDaño(int daño) // Método que se llama para hacer que el enemigo reciba daño
    {
        _puntosVidaRestantes -= daño;

        if (_puntosVidaRestantes <= 0)
        {
            Muere();
        }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private void Muere() // Método que se llama cuando el enemigo muere
    {
        SoltarPowerUp();
        Destroy(gameObject);
    }
    private void SoltarPowerUp() // Método que se llama para soltar un power up al morir el enemigo
    {
        if (PowerUps != null && PowerUps.Length > 0)
        {
            float random = Random.Range(0f, 100f);

            if (random <= ProbabilidadDrop)
            {
                int indice = Random.Range(0, PowerUps.Length);
                Instantiate(PowerUps[indice], transform.position, Quaternion.identity);
            }
        }
    }

    #endregion

} // class VidaEnemigo 
// namespace

//---------------------------------------------------------
// Gestionar el sistema de vida del player
// Javier de Sala Rodríguez
// Dream o' SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using TMPro;
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
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints


    [SerializeField]
    private int Vidas = 4;

    [SerializeField]
    private TextMeshProUGUI TextoVida;

    [SerializeField]
    private GameObject PanelGameover;

    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private static int _maximoVidas = 6;

    private static string _puntoVida = "▓ ";

    private static int _layerEnemigo = 11;

    private static int _layerItem = 12;

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
        ActualizarVidas(0);
        Debug.Log(TextoVida);
        Debug.Log(PanelGameover);
        Debug.Log(SpriteRenderer);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        ActualizarVidas(0);

    }

    /// <summary>
    /// Detecta cuando hay colisión.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == _layerEnemigo)
        {
            ActualizarVidas(-1);
        }
        else if (other.gameObject.layer == _layerItem)
        {
            ActualizarVidas(1);
            Destroy(other.gameObject);
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

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void ActualizarVidas(int delta)
    {
        string puntos = "";
        Vidas += delta;
        if (Vidas > _maximoVidas)
        {
            Vidas = _maximoVidas;
        }

        for (int i = 0; i < Vidas; i++)
        {
            puntos += _puntoVida;
        }
        TextoVida.text = puntos;

        if(Vidas <= 0)
        {
            PanelGameover.SetActive(true);
            SpriteRenderer.enabled = false;
        }
        else
        {
            PanelGameover.SetActive(false);
            SpriteRenderer.enabled = true;

            if(delta < 0)
            {
                PlayerInvencible pi = GetComponent<PlayerInvencible>();
                if( pi != null)
                {
                    pi.enabled = true;
                }
            }
        }


    }
    #endregion   

} // class Vida 
// namespace

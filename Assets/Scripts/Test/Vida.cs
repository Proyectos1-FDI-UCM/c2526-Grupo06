//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
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
    [SerializeField]
    public int vidas = 4;

    [SerializeField]
    public TextMeshProUGUI textoVida;

    [SerializeField]
    public GameObject panelGameover;

    [SerializeField]
    public SpriteRenderer spriteRenderer;



    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private static int MAXIMO_VIDAS = 6;

    private static string PUNTO_VIDA = "▓ ";

    private static int LAYER_ENEMIGO = 11;

    private static int LAYER_ITEM = 12;

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
        if (other.gameObject.layer == LAYER_ENEMIGO)
        {
            ActualizarVidas(-1);
        }
        else if (other.gameObject.layer == LAYER_ITEM)
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
        vidas += delta;
        if (vidas > MAXIMO_VIDAS)
        {
            vidas = MAXIMO_VIDAS;
        }

        for (int i = 0; i < vidas; i++)
        {
            puntos += PUNTO_VIDA;
        }
        textoVida.text = puntos;

        if(vidas <= 0)
        {
            panelGameover.SetActive(true);
            spriteRenderer.enabled = false;
        }
        else
        {
            panelGameover.SetActive(false);
            spriteRenderer.enabled = true;

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

//---------------------------------------------------------
// Breve descripción del contenido del archivo
//Script que controla el movimiento leyendo inputs y moviendo al objeto, además 
// Responsable de la creación de este archivo
//Adán 
// Nombre del juego
//Dream O'SpaceShip
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using
using UnityEngine.InputSystem;


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerControler : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    #endregion
    [SerializeField]
    private float fly_speed = 1;


    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    #endregion
    private InputAction movement_action;
    private Vector2 direction;
    private float x_limit = 8.33f;
    private float y_limit = 4.4f;

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
        movement_action = InputSystem.actions.FindAction("Move");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        direction = movement_action.ReadValue<Vector2>() ;
        if (direction != Vector2.zero)
        {
            if (direction.x > 0) transform.Translate(fly_speed * Time.deltaTime, 0, 0);
            else if (direction.x < 0) transform.Translate(-fly_speed * Time.deltaTime, 0, 0);
            if (direction.y > 0) transform.Translate(0,fly_speed * Time.deltaTime, 0);
            else if (direction.y < 0) transform.Translate(0,-fly_speed * Time.deltaTime, 0);

            //Esta parte mantiene al jugador en pantalla, solo ocurre si se intenta mover, por que por ahora no hay nada en el juego que mueva al player de otra forma
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -x_limit, x_limit), Mathf.Clamp(transform.position.y, -y_limit, y_limit), 0);

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

    #endregion

} // class PlayerControler 
// namespace

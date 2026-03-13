//---------------------------------------------------------
//Script que controla el movimiento leyendo inputs y moviendo al objeto, además 
//Adán 
//Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------


using UnityEngine;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using



/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerControler : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    [SerializeField]
    private float FlySpeed = 1;

    #endregion


    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private InputAction _movementAction; //El action donde se guarda la action de move del InputSystem
    private Vector2 _direction; //El vector que almacenara la dirección devuelta por el wasd o el joystick
    private float _xLimit = 8.33f; // Punto límite que el jugador podra alcanzar en el eje x
    private float _yLimit = 4.4f; // Punto límite que el jugador alcanzara en el eje y
    private float _freezeTimer = 0f;//esta variable se utilizara para contar cuanto tiempo le queda congelada

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
        _movementAction = InputSystem.actions.FindAction("Move");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_freezeTimer > 0) //si esta congelada no hace nada más que reducir su timer de congelación.
        {
            _freezeTimer -= Time.deltaTime;
            if (_freezeTimer < 0) _freezeTimer = 0;
        }
        else 
        {
            _direction = _movementAction.ReadValue<Vector2>();
            if (_direction != Vector2.zero)
            {
                if (_direction.x > 0) transform.Translate(FlySpeed * Time.deltaTime, 0, 0);
                else if (_direction.x < 0) transform.Translate(-FlySpeed * Time.deltaTime, 0, 0);
                if (_direction.y > 0) transform.Translate(0, FlySpeed * Time.deltaTime, 0);
                else if (_direction.y < 0) transform.Translate(0, -FlySpeed * Time.deltaTime, 0);

                //Esta parte mantiene al jugador en pantalla, solo ocurre si se intenta mover, por que por ahora no hay nada en el juego que mueva al player de otra forma
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_xLimit, _xLimit), Mathf.Clamp(transform.position.y, -_yLimit, _yLimit), 0);
            }
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
    public void AddFreezeTime(float freeze)//Añade tiempo de congelación
    {
        _freezeTimer += freeze;
    }
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

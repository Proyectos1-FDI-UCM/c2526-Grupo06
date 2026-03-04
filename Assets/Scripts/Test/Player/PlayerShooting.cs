//---------------------------------------------------------
// Componente que toma una acción de disparo y al ser pulsada instancia una cantidad de objetos
// Pablo Redondo Vaillo
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using


/// <summary>
/// Manejo de la acción de disparo e instanciamiento de un GameObject
/// </summary>
public class PlayerShooting : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private float Cadence = 0.2f;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private InputAction _fireAction;
    private bool _isShooting;
    private float _lastShot;
    private int _bulletCount;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        _fireAction = InputSystem.actions.FindAction("Fire");
        // Programación defensiva, el componente no funcionará si no hay gameobject bala o acción disparo
        if (_fireAction == null)
        {
            Debug.LogWarning("No fire action found");
            Destroy(this);
        }
        else if (Bullet == null)
        {
            Debug.LogWarning("No Prefab for Bullet selected in editor");
            Destroy(this);
        }
        // Todo correcto, inicializar valores
        else
        {
            _isShooting = false;
            _lastShot = 0f;
        }
    }
    /// <summary>
    /// Comprueba cada frame si se ha presionado la acción de disparo
    /// Si se ha hecho, ejecuta la función correspondiente n veces (cantidad de balas disponible)
    /// </summary>
    private void Update()
    {
        // Comprobación de input
        if (_fireAction.WasPressedThisFrame() && !_isShooting)
        {
            _bulletCount = 5; // Valor provisional hasta tener gamemanager
            if (_bulletCount != 0)
            {
                _isShooting = true;
            }
        }
        // Acción de disparo
        if (_isShooting && _bulletCount != 0)
        {
            // Comprueba si entre el último disparo y ahora ha pasado el tiempo de cadencia
            if (Time.time - _lastShot > Cadence) 
            {
                Shoot(); // Función de disparo
                _bulletCount--; // función provisional hasta tener gamemanager
                if (_bulletCount == 0) // Si el número de balas acaba deja la acción de disparar
                {
                    _isShooting = false;
                }
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

    //Permite al GameManager el estado del disparo
    public bool IsShooting() 
    {
        return _isShooting;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    /// <summary>
    /// Método disparar, instancia un GameObject en el jugador y guarda el tiempo de disparo
    /// </summary>
    private void Shoot()
    {
        _lastShot = Time.time; // Guarda el momento del disparo como último disparo
        GameObject bulletinstance = Instantiate(Bullet); // Instancia una copia de la bala
        bulletinstance.transform.position = transform.position; // La posición debe ser la del player
    }
    #endregion

} // class PlayerShooting 
// namespace

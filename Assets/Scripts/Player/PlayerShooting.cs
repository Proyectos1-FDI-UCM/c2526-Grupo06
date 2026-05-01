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
    /// <summary>
    /// Prefab de bala que disparara el player
    /// </summary>
    [SerializeField]
    private GameObject Bullet;
    /// <summary>
    /// Cadencia entre bala y bala cuando el jugador dispara mas de una
    /// </summary>
    [SerializeField]
    private float Cadence = 0.2f;

    [SerializeField]
    private AudioClip SonidoDisparoJugador;
    [SerializeField]
    private AudioClip SondioNoAmmo;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private InputAction _fireAction;
    // Animator para la animación de disparar
    private Animator _animator;
    private bool _isShooting;
    private float _lastShot;
    public int _bulletCount;
    private float _freezeTimer = 0f;//(Añadido de Adán) esta variable se utilizara para contar cuanto tiempo le queda congelada
    private RecogeMunicion _rMunicion;//(Añadido de Adán) esta variable es para cachear el componente recogemunición
    private bool _infiniteAmmoDebug = false; //(Añadido de Adán) variable para el modo debug 
    private bool _soundColdown = false; //(Añadido de Adán) Variable que determina si debe sonar el sondio que indica no munición
    /// <summary>
    /// booleano que controla las animaciones secretas del jugador
    /// </summary>
    private bool _secretAnimation = false;
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
            _animator = GetComponent<Animator>();
            if (_animator == null) { Debug.LogWarning("No hay animator en el player"); }
            _rMunicion = this.gameObject.GetComponent<RecogeMunicion>();
        }
        
    }
    /// <summary>
    /// Comprueba cada frame si se ha presionado la acción de disparo
    /// Si se ha hecho, ejecuta la función correspondiente n veces (cantidad de balas disponible)
    /// </summary>
    private void Update()
    {
        if (_freezeTimer > 0) //(Añadido de Adán) si esta congelada no hace nada más que reducir su timer de congelación.
        {
            _freezeTimer -= Time.deltaTime;
            if (_freezeTimer < 0) _freezeTimer = 0;
        }
        else
        {
            // Comprobación de input
            if (_fireAction.WasPressedThisFrame() && !_isShooting)
            {
                if (!_infiniteAmmoDebug) _bulletCount = _rMunicion.AmmoCount();
                if (_bulletCount != 0)
                {
                    _isShooting = true;
                }
                else if (!_soundColdown) SoundEffectsManager.instance.PlaySoundFXClip(SondioNoAmmo, transform, 2f);
                if (!_soundColdown) _soundColdown = true;
            }
            else if (_soundColdown) _soundColdown = false;
            // Acción de disparo
            if (_isShooting && _bulletCount != 0)
            {
                // Comprueba si entre el último disparo y ahora ha pasado el tiempo de cadencia
                if (Time.time - _lastShot > Cadence)
                {
                    Shoot(); // Función de disparo
                    _rMunicion.ReduceAmo(1);
                    _bulletCount--; // función provisional hasta tener gamemanager
                    if (_bulletCount == 0) // Si el número de balas acaba deja la acción de disparar
                    {
                        _isShooting = false;
                    }
                }
            }
            if (_infiniteAmmoDebug && !_isShooting)
            {
                _bulletCount = 5;
            }
        }
        
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public bool IsShooting() //(Añadido de Adán) Permite al RecogeMunición conocer el estado del disparo
    {
        return _isShooting;
    }
    public void AddFreezeTime(float freeze)//(Añadido de Adán) Añade tiempo de congelación
    {
        _freezeTimer += freeze;
    }
    public void DebugAmmoInfinite()
    {
       _infiniteAmmoDebug = !_infiniteAmmoDebug;
    }
    /// <summary>
    /// Cambia el booleano de animaciones secretas on/off
    /// </summary>
    public void ChangeSecret()
    {
        _secretAnimation = !_secretAnimation;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    /// <summary>
    /// Método disparar, instancia un GameObject en el jugador y guarda el tiempo de disparo
    /// </summary>
    private void Shoot()
    {
        if (SoundEffectsManager.instance != null) SoundEffectsManager.instance.PlaySoundFXClip(SonidoDisparoJugador, transform, 0.6f);
        // Animación de disparo
        if (_animator != null) 
        { 
            if (_secretAnimation) { _animator.SetTrigger("SecretShootTrigger"); }
            else _animator.SetTrigger("ShootTrigger"); 
        }
        _lastShot = Time.time; // Guarda el momento del disparo como último disparo
        GameObject bulletinstance = Instantiate(Bullet); // Instancia una copia de la bala
        bulletinstance.transform.position = transform.position; // La posición debe ser la del player
    }
    #endregion

} // class PlayerShooting 
// namespace

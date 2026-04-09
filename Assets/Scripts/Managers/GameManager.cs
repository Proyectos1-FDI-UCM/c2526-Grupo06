//---------------------------------------------------------
// Contiene el componente GameManager
// Guillermo Jiménez Díaz, Pedro P. Gómez Martín
// Marco A. Gómez Martín
// Template-P1
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


/// <summary>
/// Componente responsable de la gestión global del juego. Es un singleton
/// que orquesta el funcionamiento general de la aplicación,
/// sirviendo de comunicación entre las escenas.
///
/// El GameManager ha de sobrevivir entre escenas por lo que hace uso del
/// DontDestroyOnLoad. En caso de usarlo, cada escena debería tener su propio
/// GameManager para evitar problemas al usarlo. Además, se debería producir
/// un intercambio de información entre los GameManager de distintas escenas.
/// Generalmente, esta información debería estar en un LevelManager o similar.
/// </summary>
public class GameManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Boss;

    [SerializeField] private TextMeshProUGUI TextoVida;
    [SerializeField] private GameObject PanelGameover;
    [SerializeField] private GameObject PanelVictory;
    [SerializeField] private GameObject[] SpritesVidas = new GameObject[5];
    [SerializeField] private GameObject ProgresionManager;
    [SerializeField] private GameObject PanelPausa;
    [SerializeField] private GameObject PanelAjustes;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----

    #region Atributos Privados (private fields)

    /// <summary>
    /// Instancia única de la clase (singleton).
    /// </summary>
    private static GameManager _instance;
    private string _puntoVida = "▓ ";
    private GameObject _player; // Jugador
    private GameObject _boss; //jefe
    private bool _juegoPausado = false; // Estado actual del juego (pausado o no)
    private InputActionAsset _inputActions; // Asset de acciones de entrada para gestionar el control del juego
    private bool _gameOver = false; // Variable para controlar si el juego ha terminado
    // Sin _gameOver, el juego se podría pausar y reanudar después de mostrar el panel de game over, lo que no es deseable.
    private bool _panelAjustesAbierto = false; // Variable para controlar si el panel de ajustes está abierto o no

    //Variable que evita relaizar una comprovación múltiples veces.
    private bool _pMAsigned = false;
    //Donde guardaremos el MonoBehavour del ProgresionManager con el fin de llamarlo múltiples veces.
    private ProgresionManager _pM;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----

    #region Métodos de MonoBehaviour

    /// <summary>
    /// Método llamado en un momento temprano de la inicialización.
    /// En el momento de la carga, si ya hay otra instancia creada,
    /// nos destruimos (al GameObject completo)
    /// </summary>
    protected void Awake()
    {
        if (_instance != null)
        {
            // No somos la primera instancia. Se supone que somos un
            // GameManager de una escena que acaba de cargarse, pero
            // ya había otro en DontDestroyOnLoad que se ha registrado
            // como la única instancia.
            // Si es necesario, transferimos la configuración que es
            // dependiente de este manager al que ya existe.
            // Esto permitirá al GameManager real mantener su estado interno
            // pero acceder a los elementos de la nueva escena
            // o bien olvidar los de la escena previa de la que venimos
            TransferManagerSetup();

            // Y ahora nos destruímos del todo. DestroyImmediate y no Destroy para evitar
            // que se inicialicen el resto de componentes del GameObject para luego ser
            // destruídos. Esto es importante dependiendo de si hay o no más managers
            // en el GameObject.
            DestroyImmediate(this.gameObject);
        }
        else
        {
            // Somos el primer GameManager.
            // Queremos sobrevivir a cambios de escena.
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
            _juegoPausado = true;
            Init();
        } // if-else somos instancia nueva o no.
    }

    /// <summary>
    /// lo que hacemos en este método desactivar el panel de derrota cuando empezamos la partida
    /// </summary>

    private void Start()
    {
        if (PanelGameover != null)
        {
            PanelGameover.SetActive(false); // para desactivar el panel al inicio
        }
        if (ProgresionManager != null)
        {
            _pMAsigned = true;
            _pM = ProgresionManager.gameObject.GetComponent<ProgresionManager>();
        }
        _inputActions = InputSystem.actions; // para gestionar el control del juego (pausa, etc.)
        if (_instance != null) CambiarEstadoPausa();
    }

    /// <summary>
    /// Método llamado cuando se destruye el componente.
    /// </summary>
    protected void OnDestroy()
    {
        if (this == _instance)
        {
            // Éramos la instancia de verdad, no un clon.
            _instance = null;
        } // if somos la instancia principal
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----

    #region Métodos públicos

    /// <summary>
    /// Propiedad para acceder a la única instancia de la clase.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    }

    /// <summary>
    /// Devuelve cierto si la instancia del singleton está creada y
    /// falso en otro caso.
    /// Lo normal es que esté creada, pero puede ser útil durante el
    /// cierre para evitar usar el GameManager que podría haber sido
    /// destruído antes de tiempo.
    /// </summary>
    /// <returns>Cierto si hay instancia creada.</returns>
    public static bool HasInstance()
    {
        return _instance != null;
    }

    /// <summary>
    /// Método que cambia la escena actual por la indicada en el parámetro.
    /// </summary>
    /// <param name="index">Índice de la escena (en el build settings)
    /// que se cargará.</param>
    public void ChangeScene(int index)
    {
        // Antes y después de la carga fuerza la recolección de basura, por eficiencia,
        // dado que se espera que la carga tarde un tiempo, y dado que tenemos al
        // usuario esperando podemos aprovechar para hacer limpieza y ahorrarnos algún
        // tirón en otro momento.
        // De Unity Configuration Tips: Memory, Audio, and Textures
        // https://software.intel.com/en-us/blogs/2015/02/05/fix-memory-audio-texture-issues-in-unity
        //
        // "Since Unity's Auto Garbage Collection is usually only called when the heap is full
        // or there is not a large enough freeblock, consider calling (System.GC..Collect) before
        // and after loading a level (or put it on a timer) or otherwise cleanup at transition times."
        //
        // En realidad... todo esto es algo antiguo por lo que lo mismo ya está resuelto)
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        System.GC.Collect();
    } // ChangeScene


    ///<summary>
    ///Método para mostrar las vidas en pantalla
    ///</summary>
    public void MuestraVida(int vidasActuales)
    {
        if (TextoVida == null)
        {
            return;
        }

        string puntos = "";

        for (int i = 0; i < vidasActuales; i++)
        {
            puntos += _puntoVida;
        }

        TextoVida.text = puntos;
    }

    public void MuestraAmmo(int _municion, int _maxAmmo)
    {
        for (int i = 0; i < _maxAmmo; i++)
        {
            if (SpritesVidas[i] != null)
            {
                    SpritesVidas[i].transform.GetChild(0).gameObject.SetActive(i+1 <= _municion);
                    SpritesVidas[i].transform.GetChild(1).gameObject.SetActive(i+1 > _municion);
            }
        }
    }

    ///<summary>
    ///Metodo que muestra el panel de game over
    ///</summary>

    public void MostrarGameOver()
    {
        _gameOver = true;

        if (ProgresionManager != null)
            ProgresionManager.GetComponent<ProgresionManager>().StopHordeSpawning(true);

        if (PanelGameover != null)
            PanelGameover.SetActive(true);

        Time.timeScale = 0f;
    }
    /// <summary>
    /// Método que muestra el panel de victoria
    /// </summary>
    public void MostrarVictory()
    {
        _gameOver = true;

        if (PanelVictory != null)
            PanelVictory.SetActive(true);
    }

    /// <summary>
    /// Metodo que permite spawnear un enemigo asignandole una cantidad, una posicion y un valor
    /// que indica un rango de spawn al rededor de la posición otorgada. Esto solo se usara si
    /// la cantidad de enemigos es mayor a 1
    /// </summary>
    public void EnemigoSpawn(GameObject enemy, int amount, Vector2 xy, float Spread)
    {
        if (enemy != null)
        {
            if (!(Spread > 0f))
            {
                GameObject enemyinstance = Instantiate(enemy);
                enemyinstance.transform.position = xy;
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    GameObject enemyinstance = Instantiate(enemy);
                    Vector2 SpawnPosition = xy + (new Vector2(Random.Range(-Spread, Spread+1), Random.Range(-Spread, Spread + 1)));
                    enemyinstance.transform.position = SpawnPosition;
                }
            }
        }
    }
    public void EnemyKilled()
    {
        if (_pMAsigned) _pM.ReduceEnemyCount(1);
        else Debug.Log("There's no ProgresionManager so this kill wont register");
    }
    // -- Métodos para get y set player --
    /// <summary>
    /// Devuelve el GameObject que equivale al jugador
    /// </summary>
    /// <returns>GameObject jugador</returns>
    public GameObject GetPlayer()
    {
        // Avisa si se intenta conseguir un player y es nulo (no se ha asignado un gameObject jugador)
        if (_player == null) { Debug.LogWarning("No hay player que devolver"); } 
        return _player;
    }
    /// <summary>
    /// Le indica al GameManager que el objeto dado es el jugador, útil para enemigos que siguen su posición.
    /// </summary>GameObject que equivale al jugador</param>
    public void SetPlayer(GameObject Player)
    {
        _player = Player;
    }

    public GameObject GetBoss()
    {
        // Avisa si se intenta conseguir un boss  y es nulo (no se ha asignado un gameObject boss)
        if (_boss == null) { Debug.LogWarning("No hay player que devolver"); }
        return _boss;
    }
    /// <summary>
    /// Le indica al GameManager que el objeto dado es el boss, útil para proyectiles que se instancien en su posición.
    /// </summary>GameObject que equivale al boss</param>
    public void SetBoss(GameObject Boss)
    {
        _boss = Boss;
    }

    /// <summary>
    /// Alterna el estado de pausa del juego. 
    /// Si el juego está en ejecución lo pausa, y si está pausado lo reanuda.
    /// Además, muestra u oculta el panel de pausa y ajusta el Time.timeScale.
    /// </summary>
    public void CambiarEstadoPausa()
    {
        if (!_gameOver)
        {
            if (_panelAjustesAbierto)
            {
                CerrarPanelAjustes();
            }
            else
            {
                _juegoPausado = !_juegoPausado;

                if (_juegoPausado)
                {
                    Time.timeScale = 0f;
                    if (PanelPausa != null) PanelPausa.SetActive(true);

                    _inputActions.FindActionMap("Player").Disable();
                    _inputActions.FindActionMap("UI").Enable();
                }
                else
                {
                    Time.timeScale = 1f;
                    if (PanelPausa != null) PanelPausa.SetActive(false);

                    if (_inputActions.FindActionMap("Player") != null) _inputActions.FindActionMap("Player").Enable();
                }
            }
        }
    }

    public void AbrirPanelAjustes()
    { 
        if (PanelAjustes != null) PanelAjustes.gameObject.SetActive(true);
        if (PanelPausa != null) PanelPausa.SetActive(false);
        _panelAjustesAbierto = true;
    }
    public void CerrarPanelAjustes()
    {
        if (PanelAjustes != null) PanelAjustes.gameObject.SetActive(false);
        if (PanelPausa != null) PanelPausa.SetActive(true);
        _panelAjustesAbierto = false;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----

    #region Métodos Privados

    /// <summary>
    /// Dispara la inicialización.
    /// </summary>
    private void Init()
    {
        // De momento no hay nada que inicializar
    }

    private void TransferManagerSetup()
    {
        // De momento no hay que transferir ningún setup
        // a otro manager
    }

    #endregion
} // class GameManager 
// namespace
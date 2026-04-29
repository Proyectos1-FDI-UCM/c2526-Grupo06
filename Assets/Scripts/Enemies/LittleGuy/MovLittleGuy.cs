//---------------------------------------------------------
// Script que define el comportamiento del enemigo little guy
// Alejandro de Haro
// Dream O' Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UIElements;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class MovLittleGuy : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private GameObject _littleBullet; //Prefab de la bala normal

    [SerializeField] private Animator _animator; //Animador del propio enemigo

    [SerializeField]
    private AudioClip[] SonidoDisparoLittleGuy;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private Vector3 _posIni;       //para poner la posición inicial al instanciarlo
    private Vector3 _endPos;       //para guardar la posición que debe de tomar
    private int _vel = 4;          //velocidad a la cual se mueve
    private Vector3 _maxOffset = new Vector3 (0.2f, 0.2f, 0.2f); //guarda un offset para comprobar posiciones
    private float _cadencia = 1f;   //cada cuanto dispara
    private float _timerCad = 0f;   //temporizador de disparo
    private float _freezeTimer = 0f;//(Añadido de Adán) esta variable se utilizara para contar cuanto tiempo le queda congelada
    private Transform _player;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour


    void Start()
    {
        _posIni = transform.position;
        _endPos = new Vector3 (3.5f, _posIni.y, _posIni.z);
        // Busca al player mediante el GameManager
        if (GameManager.Instance != null)
        {
            GameObject player = GameManager.Instance.GetPlayer();
            if (player != null)
            {
                _player = player.transform;
            }
        }
    }

    void Update()
    {
        if (_freezeTimer > 0) //(Añadido de Adán) si esta congelada no hace nada más que reducir su timer de congelación.
        {
            _freezeTimer -= Time.deltaTime;
            if (_freezeTimer < 0) _freezeTimer = 0;
        }
        else if (!Equal(_posIni, _endPos, _maxOffset))
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPos, _vel * Time.deltaTime);
            _posIni = transform.position;
        }
        else
        {
            Vector2 direccion = _player.position - transform.position;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angulo + 180f);

            _timerCad += Time.deltaTime;

            if (_timerCad >= _cadencia)
            {
                Disparar();
                _timerCad = 0f;
            }

            //Extra para controlar el paso a la animación de disparo
            if (_animator != null)
            {
                if (_timerCad >= _cadencia - 0.4f) _animator.SetBool("AboutToShoot", true);
                else if (_timerCad < 0.2f && _timerCad > 0.1f ) _animator.SetBool("AboutToShoot", false);
            }

        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    public void AddFreezeTime(float freeze)//(Añadido de Adán) Añade tiempo de congelación
    {
        _freezeTimer += freeze;
    }
    
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    private bool Equal(Vector3 _v1, Vector3 _v2, Vector3 _e)    //método para comparar coordenadas con un rango de error
    {
        return System.Math.Abs(_v1.x - _v2.x) <= _e.x && System.Math.Abs(_v1.y - _v2.y) <= _e.y;
    }
    private void Disparar() //Instancia un proyectil desde el punto de disparo del enemigo.
    {
        GameObject spawned = Instantiate(_littleBullet, transform.position, Quaternion.Euler(0, 0, 0));
        if (SoundEffectsManager.instance != null) SoundEffectsManager.instance.PlayRandomSoundFXClip(SonidoDisparoLittleGuy, transform, 1f);

        if (Random.value <= 0.4f)
        {
            spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
            spawned.GetComponent<OtorgaMunicion>().enabled = true;
        }
    }
    #endregion   

} // class MovLittleGuy 
// namespace

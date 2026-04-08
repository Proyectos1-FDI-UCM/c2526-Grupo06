//---------------------------------------------------------
// Script exclusivo para el disparo del Little Guy
// Alejandro de Haro
// Dream'O Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


public class DispLittleGuy : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool Gravity = false;        //(Añadido de Adán) Si se activa la bala empezara a caer simulando gravedad.
    private float GravityStrenght = -8f; //(Añadido de Adán) unidades de unity que acelerara.

    private float _vel = -0.6f;
    private Vector3 _posInicial;
    private Vector2 _dir;

    private float _verticalVelocity = 0f;//(Añadido de Adán) velocidad vertical acumulada por aceleración gravitatoria
    private float _verticalDistance = 0f;//(Añadido de Adán) distancia que deberia haber recorrido la bala por el efecto de la gravedad (en realidad es re cutre, pero no pienso luchar contra la ecuación de movimiento de Miguel;
    private float _freezeTimer = 0f;//(Añadido de Adán) esta variable se utilizara para contar cuanto tiempo le queda congelada

    private Transform _player;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        _posInicial = transform.position;


        GameObject tf = GameObject.Find("Player");
        if (tf != null)
        {
            _player = tf.transform;
        }

        _dir = new Vector2(transform.position.x - _player.position.x, transform.position.y - _player.position.y);

        //(Añadido de Adán) Comprueba si la bala es recogible para darle color verde
        OtorgaMunicion otorga = this.gameObject.GetComponent<OtorgaMunicion>();
        if (otorga != null ) 
        {
            if (otorga.isActiveAndEnabled) this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            else this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        

    }

    void Update()
    {
        if (_freezeTimer > 0) //(Añadido de Adán) si esta congelada no hace nada más que reducir su timer de congelación.
        {
            _freezeTimer -= Time.deltaTime;
            if (_freezeTimer < 0) _freezeTimer = 0;
        }
        else
        {
            transform.Translate(_dir * _vel * Time.deltaTime);

            /*if (Gravity) //(Añadido de Adán) calculos necesarios en caso de tener gravedad
            {
                _verticalDistance += _verticalVelocity * Time.deltaTime;
                _verticalVelocity += GravityStrenght * Time.deltaTime;
            }
            else if (_verticalVelocity != 0f) { _verticalVelocity = 0f; }
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);*/
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void GravityChange()//(Añadido de Adán) Cambia la boleana de gravedad
    {
        Gravity = !Gravity;
    }
    public void AddFreezeTime(float freeze)//(Añadido de Adán) Añade tiempo de congelación
    {
        _freezeTimer += freeze;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion

} // class DispLittleGuy 
// namespace

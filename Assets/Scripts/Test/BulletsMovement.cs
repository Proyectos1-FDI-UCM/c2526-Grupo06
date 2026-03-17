//---------------------------------------------------------
// Componente que describe el movimiento de un proyectil
// Miguel Calderón Barba
// Dream O’ SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UIElements;
// Añadir aquí el resto de directivas using


/// <summary>
/// Movimiento de proyectil recto
/// </summary>
public class BulletsMovement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    //El número de unidades de unity que recorre por cada frame
    [SerializeField]
    private float VelocidadIni = -6f;

    //Si se activa la bala se comportará con la aceleración correspondiente, si está desactivado será velocidad constante
    [SerializeField]
    private bool Acelera = false;
    [SerializeField]
    private float VelocidadFin = 0f; //la velocidad inicial en cuestión

    //Si se activa la bala se comportará en un periodo constante e identico correspondiente al amplitud en el eje y
    [SerializeField]
    private bool Curvo = false;
    [SerializeField]
    private float Amplitud = 0f; //unidades de unity máximas que subirá o bajará
    [SerializeField]
    private float Periodo = 1f; //unidades de unity en el eje x por el que dará una ida y vuelta completa

    //(Añadido de Adán) Si se activa la bala empezara a caer simulando gravedad.
    [SerializeField]
    private bool Gravity = false;
    [SerializeField]
    private float GravityStrenght = -8f; //(Añadido de Adán) unidades de unity que acelerara.
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private float _x, _y, //Resultados de cada dimensión para simplificar código
        _aceleracion, //Cálculo de la aceleración mediante la velocidad inicial, final y la posición inicial
        _amplitud, _periodo; //Parámetros de la curva para preservar las variables del inspector
    private float _distancia = 16; //unidades de unity que describe el desplazamiento hasta la velocidad final, es decir el largo de la pantalla
    private Vector3 _posInicial;
    private float _index = 0f; //tiempo transcurrido sin depender del framerate

    private float _verticalVelocity = 0f;//(Añadido de Adán) velocidad vertical acumulada por aceleración gravitatoria
    private float _verticalDistance = 0f;//(Añadido de Adán) distancia que deberia haber recorrido la bala por el efecto de la gravedad (en realidad es re cutre, pero no pienso luchar contra la ecuación de movimiento de Miguel;
    private float _freezeTimer = 0f;//(Añadido de Adán) esta variable se utilizara para contar cuanto tiempo le queda congelada
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// 
    /// Movimiento Rectilineo Uniforme en dirección i
    /// </summary>
    void Start()
    {
        _posInicial = transform.position;
        //Despejado la aceleración de la fórmula de la posición y la velocidad de MRUA que quedaría en (velFin-velIni)^2 * 2(-posIni-VelIni*t)^1/3{raiz cúbica}
        //ASUMIMOS QUE LOS PROYECTILES VAN HACIA LA IZQUIERDA, SI NO FUESE ASÍ HABRÍA QUE CALCULAR LA DIRECCIÓN
        if (Acelera) _aceleracion = -(VelocidadFin * VelocidadFin - VelocidadIni * VelocidadIni) / (2 * _distancia); //_aceleracion = Mathf.Pow(VelocidadFin - VelocidadIni, 2f) * -Mathf.Pow(Mathf.Abs(2 * (-_posIni - VelocidadIni * _index)), 1f / 3f); 
        else _aceleracion = 0;
        if (Curvo) { _amplitud = Amplitud; _periodo = Periodo; }
        else { _amplitud = 0; _periodo = 1; }

        //(Añadido de Adán) Comprueba si la bala es recogible para darle color verde
        OtorgaMunicion otorga = this.gameObject.GetComponent<OtorgaMunicion>();
        if (otorga != null)
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
            _index += Time.deltaTime;
            //fórmula de la posición del MRUA tomando la posición inicial a 0
            _x = VelocidadIni * _index + (_aceleracion * _index * _index) / 2;
            //Usando la definición seno se puede sacar la siguiente expresión: A*sen(2π*v*t/T)
            _y = _amplitud * Mathf.Sin(2 * Mathf.PI * VelocidadIni * _index / _periodo) + _verticalDistance;

            if (Gravity) //(Añadido de Adán) calculos necesarios en caso de tener gravedad
            {
                _verticalDistance += _verticalVelocity * Time.deltaTime;
                _verticalVelocity += GravityStrenght * Time.deltaTime;
            }
            else if (_verticalVelocity != 0f) { _verticalVelocity = 0f; }
            transform.position = new Vector3(_x, _y, 0) + _posInicial;
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
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class BulletsMovement 
// namespace

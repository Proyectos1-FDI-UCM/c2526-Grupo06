//---------------------------------------------------------
// Patrones de movimiento del jefe
// MARTA REYES FUNK
// Dream O Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// Quedan recopilados todos los patrones de movimiento del boss
/// Incluendo sus llamadas para cambiar de patrón, así como los métodos para cada tipo de movimiento
/// </summary>
public class ComportamientoJefeII : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private float Velocidad;

    [SerializeField]
    private float TiempoPausa = 1f; // tiempo que se queda parado el boss al realizar un ataque

    [SerializeField]
    private TypeMov TipoMovimiento;


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    /// <summary>
    /// Default: Movimiento del boss normal, solo se balancea de arriba a abajo. 
    /// Ataque Cargado: Movimiento para ataque de barrida y horizontal, se para al disparar.
    /// Ataque Vertical: Movimiento para ataque vertical, se teletransporta al final.
    /// </summary>
    private enum TypeMov
    {
        Default,
        AtaqueCargado,
        AtaqueVerticalUp,
        AtaqueVerticalDown
    }

    private bool _isPaused = false; // indica si el boss está parado por un ataque
    private float _pauseTimer = 0f; // contador para el tiempo de pausa del boss
    /// <summary>
    /// Tiempo sin limitar, para que el movimiento no se vea afectado al pausar
    /// </summary>
    private float _unclampedTime = 0f;
    /// <summary>
    /// Posición senoidal en la que pausó el boss
    /// </summary>
    private int _lastPause = -1;

    private float _intialPositionY; //posición inicial del boss en el eje Y
    private float _amplitudMov = 2.5f; // amplitud del movimiento del boss, es aprox 1/3 de la altura del escenario
    
    // posiciones en las que se puede para el boss
    private float _topPosition; // posición superior del movimiento del boss
    private float _middlePosition; // posición media del movimiento del boss
    private float _bottomPosition; // posición inferior del movimiento del boss

    private bool _lanzarAtaque = false;
    private string[] estados = { "ATAQUE SIMPLE", "NADA", "SIMPLE OPUESTO 1", "SIMPLE OPUESTO 2", "ONDA INVERTIDA" };
    private int indiceEstados = -1;   //La primera vez con índice -1, no hace "ataque"

    private PatronManager _patrones;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// Al iniciar el juego, guarda las psoiciones clave
    /// </summary>
    void Start()
    {
        _patrones = this.GetComponent<PatronManager>();

        _intialPositionY = transform.position.y; // guarda la posición inicial del boss en el eje Y

        // posiciones de posible pausa
        _topPosition = _intialPositionY + _amplitudMov;
        _middlePosition = _intialPositionY;
        _bottomPosition = _intialPositionY - _amplitudMov;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        switch (TipoMovimiento)
        {
            case TypeMov.Default:
                MovimientoDefault();
                break;
            case TypeMov.AtaqueCargado:
                MovimientoAtaqueCargado();
                break;
            case TypeMov.AtaqueVerticalUp:
                MovimientoAtaqueVertical(-1);
                break;
            case TypeMov.AtaqueVerticalDown:
                MovimientoAtaqueVertical(1);
                break;
        }

    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    /// <summary>
    /// Cambia el patrón al movimiento por defecto, que es el movimiento normal del boss, sin pausas ni teletransportes
    /// </summary>
    public void ChangeToDefault()
    {
        TipoMovimiento = TypeMov.Default;
    }

    /// <summary>
    /// Cambia el patrón al movimiento con pausas, que es el movimiento para 
    /// los ataques de barrida y horizontal, en el que el boss se para al disparar
    /// </summary>
    public void ChangeToAtaqueCargado()
    {
        TipoMovimiento = TypeMov.AtaqueCargado;
    }

    /// <summary>
    /// Cambia el patrón al movimiento con teletransporte hacia arriba
    /// </summary>
    public void ChangeToAtaqueVerticalUp()
    {
        TipoMovimiento = TypeMov.AtaqueVerticalUp;
    }

    /// <summary>
    /// Cambia el patrón al movimiento con teletransporte hacia abajo
    /// </summary>
    public void ChangeToAtaqueVerticalDown()
    {
        TipoMovimiento = TypeMov.AtaqueVerticalDown;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    /// <summary>
    /// Movimiento normal, se balancea de arriba a abajo sin pausas ni teletransportes
    /// </summary>
    private void MovimientoDefault()
    {
        _unclampedTime += Time.deltaTime * Velocidad; // tiempo del movimiento
        SimplificaUnclampedTime(_unclampedTime); // se simplifica el tiempo sin limitar para que no se vuelva demasiado grande
        // Movimiento de balanceo de arriba a abajo
        float nuevaPosicionY = _intialPositionY + _amplitudMov * Mathf.Sin(_unclampedTime);
        transform.position = new Vector3(transform.position.x, nuevaPosicionY, transform.position.z);
    }

    /// <summary>
    /// El boss se balancea, pero al llegar a una posición clave, se para un tiempo
    /// Se usa el seno para el movimiento para hacerlo más fluido, por ello para 
    /// comprobar si el boss ha llegado a una posición clave, se comprueba si el valor 
    /// del seno es cercano a 1, 0 o -1, que son las posiciones de pausa
    /// </summary>
    private void MovimientoAtaqueCargado()
    {
        if (!_isPaused) 
        {
            _unclampedTime += Time.deltaTime * Velocidad; // tiempo del movimiento
            SimplificaUnclampedTime(_unclampedTime); // se simplifica el tiempo sin limitar para que no se vuelva demasiado grande
            float pos = Mathf.Sin(_unclampedTime); // posición del movimiento, es un valor entre -1 y 1
            if (pos > 0)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, _middlePosition, transform.position.z),
                    new Vector3(transform.position.x, _topPosition, transform.position.z), pos);
            }
            else
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, _middlePosition, transform.position.z),
                    new Vector3(transform.position.x, _bottomPosition, transform.position.z), -pos);
            }
            CheckAllCheckpoints(pos, _lastPause); // se comprueba si el boss ha llegado a un checkpoint para pausar
        }
        else
        {
            if (_lanzarAtaque)
            {
                LanzarAtaque();
                _lanzarAtaque = false;
            }

            _pauseTimer += Time.deltaTime; // pasa el tiempo
            if (_pauseTimer >= TiempoPausa)
            {
                _isPaused = false;
                _pauseTimer = 0f; // se restea el timer
            }
        }

    }

    private void LanzarAtaque()
    {
        if( indiceEstados >= 0)
        {
            Debug.Log("LANZAR ATAQUE!!!! -> " + estados[indiceEstados]);
            indiceEstados = (indiceEstados + 1) % estados.Length;

            switch (indiceEstados)
            {
                case 0: _patrones.PatronSimple(true, false); break;
                case 1: break;
                case 2: _patrones.PatronSimple(false, false); break;
                case 3: _patrones.PatronSimple(false, false); break;
                case 4: break;
                case -1: break;
            }
        }
        else
        {
            indiceEstados = 0;
        }
    }

    private void CheckAllCheckpoints(float pos, int lastCheckpoint)
    {
        float margin = 0.05f; // le damos un margen de error al pobre que es un float
        if ((Mathf.Abs(pos - 1f) < margin && lastCheckpoint != 1) ||
            (Mathf.Abs(pos - 0f) < margin && lastCheckpoint != 0) ||
            (Mathf.Abs(pos - (-1f)) < margin && lastCheckpoint != -1))
        {
            if (!_isPaused) {
                _lanzarAtaque = true;
            }
            _isPaused = true; // se pausa el boss
            _lastPause = Mathf.RoundToInt(pos); // se guarda el checkpoint en el que se ha pausado
        }
    }

    /// <summary>
    /// El boss se balancea, pero al llegar a la posición superior o inferior,
    /// se teletransporta a la posición opuesta
    /// </summary>
    private void MovimientoAtaqueVertical(int peak)
    {
        if (!_isPaused)
        {
            _unclampedTime += Time.deltaTime * Velocidad; // tiempo del movimiento
            SimplificaUnclampedTime(_unclampedTime); // se simplifica el tiempo sin limitar para que no se vuelva demasiado grande
            float pos = Mathf.Sin(_unclampedTime); // posición del movimiento, es un valor entre -1 y 1
            if (pos > 0)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, _middlePosition, transform.position.z),
                    new Vector3(transform.position.x, _topPosition, transform.position.z), pos);
            }
            else
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, _middlePosition, transform.position.z),
                    new Vector3(transform.position.x, _bottomPosition, transform.position.z), -pos);
            }

            float margin = 0.05f; // le damos un margen de error al pobre que es un float
            // se comprueba si el boss ha llegado a la posición de pausa, que depende del tipo de ataque vertical
            if (Mathf.Abs(pos - (peak)) < margin && _lastPause != peak)
            {
                _isPaused = true; // se pausa el boss
                // se teletransporta a la posición opuesta
                if (peak == 1)
                {
                    transform.position = new Vector3(transform.position.x, _bottomPosition, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, _topPosition, transform.position.z);
                }
                _lastPause = peak; // se guarda el checkpoint en el que se ha pausado
                _unclampedTime = _unclampedTime + Mathf.PI;
                // se avanza el tiempo para que el movimiento siga siendo fluido después de teletransportarse
                // si no volvería a la posición original tras la pausa y no se vería bien
            }
        }
        else
        {
            _pauseTimer += Time.deltaTime; // pasa el tiempo
            if (_pauseTimer >= TiempoPausa)
            {
                _isPaused = false;
                _pauseTimer = 0f; // se restea el timer
                
            }
        }
    }

    /// <summary>
    /// Simplifica el tiempo sin limitar, para que el valor no se vuelva demasiado grande
    /// se divide el tiempo entre 2*PI, que es el periodo del movimiento senoidal, y se redondea al entero más cercano
    /// </summary>
    private float SimplificaUnclampedTime(float unclampedTime)
    {
        return Mathf.RoundToInt(unclampedTime / (2 * Mathf.PI));
    }

    #endregion   

} // class BossMovement 
// namespace

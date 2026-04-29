//---------------------------------------------------------
// Patrones de movimiento del jefe
// MARTA REYES FUNK
// Dream O' Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// Quedan recopilados todos los patrones de movimiento del boss
/// Incluendo sus llamadas para cambiar de patrón, así como los métodos para cada tipo de movimiento
/// </summary>
public class BossMovement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

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
    private float _velocidadMov; // velocidad del movimiento del boss
    private float _defaultVel; // velocidad por defecto del movimiento del boss
    private float _chargedVel; // velocidad del movimiento del boss para el ataque cargado, es más rápido para compensar las pausas

    private Fases _fases; // referencia al script de fases

    // posiciones en las que se puede para el boss
    private float _topPosition; // posición superior del movimiento del boss
    private float _middlePosition; // posición media del movimiento del boss
    private float _bottomPosition; // posición inferior del movimiento del boss

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// Al iniciar el juego, guarda las psoiciones clave
    /// </summary>
    void Start()
    {
        _intialPositionY = transform.position.y; // guarda la posición inicial del boss en el eje Y

        // posiciones de posible pausa
        _topPosition = _intialPositionY + _amplitudMov;
        _middlePosition = _intialPositionY;
        _bottomPosition = _intialPositionY - _amplitudMov;

        _fases = GetComponent<Fases>(); // obtiene la referencia al script de fases
        _defaultVel = CalculaVel(_amplitudMov, _fases.GetNumPatronPerSecond()); // calcula la velocidad del movimiento del boss por defecto
        _chargedVel = CalculaChargedVel(_amplitudMov, _fases.GetNumPatronPerSecond(), TiempoPausa); // calcula la velocidad para el ataque cargado

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        switch (TipoMovimiento)
        {
            case TypeMov.Default:
                _velocidadMov = _defaultVel;
                MovimientoDefault();
                break;
            case TypeMov.AtaqueCargado:
                _velocidadMov = _chargedVel;
                MovimientoAtaqueCargado();
                break;
            case TypeMov.AtaqueVerticalUp:
                _velocidadMov = _defaultVel;
                MovimientoAtaqueVertical(-1);
                break;
            case TypeMov.AtaqueVerticalDown:
                _velocidadMov = _defaultVel;
                MovimientoAtaqueVertical(1);
                break;
        }

    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos Públicos

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

    /// <summary>
    /// Devuelve la velocidad actual del movimiento del boss, para que otros scripts puedan usarla
    /// </summary>
    public float GetCurrentAmpTime()
    {
        return _amplitudMov / _velocidadMov;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    /// <summary>
    /// Movimiento normal, se balancea de arriba a abajo sin pausas ni teletransportes
    /// </summary>
    private void MovimientoDefault()
    {
        _unclampedTime += Time.deltaTime * _velocidadMov; // tiempo del movimiento
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
            _unclampedTime += Time.deltaTime * _velocidadMov; // tiempo del movimiento
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
            _pauseTimer += Time.deltaTime; // pasa el tiempo
            if (_pauseTimer >= TiempoPausa)
            {
                _isPaused = false;
                _pauseTimer = 0f; // se restea el timer
            }
        }

    }

    /// <summary>
    /// Comprueba si el boss ha llegado a los puntos clave
    /// </summary>
    private void CheckAllCheckpoints(float pos, int lastCheckpoint)
    {
        float margin = 0.05f; // le damos un margen de error al pobre que es un float
        if ((Mathf.Abs(pos - 1f) < margin && lastCheckpoint != 1) ||
            (Mathf.Abs(pos - 0f) < margin && lastCheckpoint != 0) ||
            (Mathf.Abs(pos - (-1f)) < margin && lastCheckpoint != -1))
        {
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
            _unclampedTime += Time.deltaTime * _velocidadMov; // tiempo del movimiento
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

    /// <summary>
    /// Calcula la velocidad del movimiento del boss
    /// </summary>
    private float CalculaVel(float amp, float numPatronesPerSec)
    {
        return (Mathf.PI * amp) / (2 * numPatronesPerSec);
    }

    /// <summary>
    /// Calcula la velocidad del movimiento del boss para el ataque cargado, teniendo en cuenta el tiempo de pausa, para que el movimiento siga siendo el mismo a pesar de las pausas
    /// </summary>
    private float CalculaChargedVel(float amp, float numPatronesPerSec, float tiempoPausa)
    {
        // se calcula el tiempo total del movimiento con pausas, que es el tiempo del movimiento sin pausas más el tiempo de las pausas
        float tiempoTotal = (CalculaVel(amp, numPatronesPerSec) * 4) + (tiempoPausa * 3); // se multiplican por 4 y 3 porque hay 4 movimientos y 3 pausas en el ciclo completo
        return (Mathf.PI * amp) / (2 * (tiempoTotal / 4)); // se divide el tiempo total entre 4 para obtener el tiempo de cada movimiento, y se calcula la velocidad en base a ese tiempo
    }

    /// <summary>
    /// Cambia la velocidad del movimient actual
    /// </summary>
    private float ChangeVel(float newVel)
    {
        _velocidadMov = newVel;
        return _velocidadMov;
    }

    #endregion   

} // class BossMovement 
// namespace

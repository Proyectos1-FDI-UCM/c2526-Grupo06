using UnityEngine;

//---------------------------------------------------------
// Lista de patrones del boss en cada fase
// Miguel Calderón Barba, Javier De Sala Rodríguez & Sergio Navarro Herreros
// Dream'O Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Fases : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [SerializeField]
    private float PatronPerSecond = 1f;
    [SerializeField]
    private int NumPatrons = 48;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    public int _faseActual = 1;
    private float _realTime = 0f;
    private float _timer = 0f;
    private PatronManager _patrones;
    private BossMovement _movement;

    private int _casoAnterior;
    private int _casoActual;
    private char I = 'I', P = 'P', G = 'G';

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
        _patrones = this.GetComponent<PatronManager>();
        _movement = this.GetComponent<BossMovement>();
        _casoAnterior = -1;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        _realTime += Time.deltaTime;
        _timer = (_realTime * PatronPerSecond);
        switch (_faseActual)
        {
            case 1: PrimeraFase(_timer); break;
            case 2: SegundaFase(_timer); break;
            case 3: TerceraFase(_timer); break;
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

    ///<summary>
    ///Este método llama sucesivamente al bucle de patrones
    ///que se da en la primera fase del boss.
    ///</summary>

    public void PrimeraFase(float timer)
    {
        // Actualizamos el caso actual
        _casoActual = ((int)timer) % 48;

        // Si no ha habido un cambio de caso, entonces salimos de la función
        if (_casoAnterior == _casoActual) return;

        // En este punto, ha habido un cambio de caso y procedemos a lanzar el ataque correspondiente

        /*
        switch (timer)
        {
            case 0: _movement.ChangeToDefault(); break;
            case 1:; break;
            case 2:; break;
            case 3:; break;

            case 4: _patrones.PatronSimple(false, false); break;
            case 5:; break;
            case 6: _patrones.PatronSimple(false, false); break;
            case 7:; break;

            case 8: _patrones.PatronCadencia(false, false); break;
            case 9:; break;
            case 10: _patrones.PatronCadencia(false, false); break;
            case 11:; break;

            case 12:; break;
            case 13: _patrones.Horizontal(false, false); break;
            case 14:; break;
            case 15: _patrones.PatronVertical(false, false); break;

            case 16: _patrones.PatronSimple(false, false); break;
            case 17: _patrones.PatronSimple(false, false); break;
            case 18: _patrones.PatronSimple(false, false); break;
            case 19: _patrones.PatronSimple(false, false); break;

            case 20: _patrones.PatronSimple(false, false); break;
            case 21: _patrones.PatronCadencia(false, false); break;
            case 22: _patrones.PatronSimple(false, false); break;
            case 23: _patrones.PatronCadencia(false, false); break;

            case 24: _patrones.PatronVertical(false, false); break;
            case 25: _patrones.PatronCadencia(false, false); break;
            case 26: _patrones.PatronVertical(false, false); break;
            case 27: _patrones.PatronCadencia(false, false); break;

            case 28: _patrones.PatronVertical(false, false); break;
            case 29: _patrones.PatronCadencia(false, false); break;
            case 30: _patrones.PatronCadencia(false, false); break;
            case 31: _patrones.PatronVertical(false, false); break;

            case 32: _movement.ChangeToDefault(); break;
            case 33: _patrones.PatronHorizontal(false, true); break;
            case 34: _patrones.PatronHorizontal(false, true); break;
            case 35: _patrones.PatronHorizontal(false, true); break;

            case 36: _patrones.PatronVertical(true, false); break;
            case 37: _movement.ChangeToDefault(); break;
            case 38: _patrones.PatronVertical(true, false); break;
            case 39: _movement.ChangeToDefault(); break;

            case 40: _patrones.PatronSimple(false, true); break;
            case 41: _patrones.PatronSimple(true, false); break;
            case 42: _patrones.PatronVertical(true, false); break;
            case 43: _movement.ChangeToDefault(); break;

            case 44: _patrones.PatronSimple(true, false); break;
            case 45: _patrones.PatronVertical(false, true); break;
            case 46: _patrones.PatronSimple(true, false); break;
            case 47: _patrones.PatronVertical(false, true); break;
        }
        */

        // Guardar el caso actual para el siguiente frame, y así detectar un posible cambio de caso
        _casoAnterior = _casoActual;

    }

    public void SegundaFase(float timer)
    {
        // Actualizamos el caso actual
        _casoActual = ((int)timer) % 60;

        // Si no ha habido un cambio de caso, entonces salimos de la función
        if (_casoAnterior == _casoActual) return;

        // En este punto, ha habido un cambio de caso y procedemos a lanzar el ataque correspondiente
        switch (_casoActual)
        {
            case 0: _movement.ChangeToDefault(); break;
            case 1: break;
            case 2: _patrones.PatronSimple(false, false); break;
            case 3: _patrones.PatronSimple(false, false); break;
            case 4: _patrones.LanzarOnda(I); break;

            case 5: _movement.ChangeToDefault(); break;
            case 6: break;
            case 7: _patrones.PatronSimple(false, false); break;
            case 8: _patrones.PatronSimple(false, false); break;
            case 9: _patrones.LanzarOnda(I); break;

            case 10: _patrones.PatronVertical(true, false, false); break;
            case 11: _patrones.PatronHorizontal(true, false); break;
            case 12: _patrones.PatronVertical(true, true, false); break;
            case 13: _patrones.PatronHorizontal(true, false); break;
            case 14: _patrones.PatronVertical(true, false, false); break;

            case 15: _patrones.PatronVertical(true, false, false); break;
            case 16: _patrones.PatronHorizontal(true, false); break;
            case 17: _patrones.PatronVertical(true, true, false); break;
            case 18: _patrones.PatronHorizontal(true, false); break;
            case 19: _patrones.PatronVertical(true, false, false); break;

            case 20: _movement.ChangeToDefault(); break;
            case 21: _patrones.PatronBarrida(true, false); break;
            case 22: _patrones.PatronHorizontal(false, false); break;
            case 23: _patrones.PatronBarrida(true, false); break;
            case 24: _movement.ChangeToDefault(); break;

            case 25: _movement.ChangeToDefault(); break;
            case 26: break;
            case 27: _patrones.PatronSimple(false, false); break;
            case 28: _patrones.PatronSimple(false, false); break;
            case 29: _patrones.LanzarOnda(I); break;

            case 30: _movement.ChangeToDefault(); break;
            case 31: break;
            case 32: _patrones.PatronSimple(false, false); break;
            case 33: _patrones.PatronSimple(false, false); break;
            case 34: _patrones.LanzarOnda(I); break;

            case 35: _movement.ChangeToDefault(); break;
            case 36: _patrones.PatronBarrida(true, false); break;
            case 37: _patrones.PatronHorizontal(false, false); break;
            case 38: _patrones.PatronBarrida(true, false); break;
            case 39: _movement.ChangeToDefault(); break;

            case 40: _patrones.PatronVertical(true, false, false); break;
            case 41: _patrones.PatronHorizontal(true, false); break;
            case 42: _patrones.PatronVertical(true, true, false); break;
            case 43: _patrones.PatronHorizontal(true, false); break;
            case 44: _patrones.PatronVertical(true, false, false); break;

            case 45: _patrones.PatronVertical(true, false, false); break;
            case 46: _patrones.PatronHorizontal(true, false); break;
            case 47: _patrones.PatronVertical(true, true, false); break;
            case 48: _patrones.PatronHorizontal(true, false); break;
            case 49: _patrones.PatronVertical(true, false, false); break;

            case 50: _movement.ChangeToDefault(); break;
            case 51: break;
            case 52: _patrones.PatronSimple(false, false); break;
            case 53: _patrones.PatronSimple(false, false); break;
            case 54: _patrones.LanzarOnda(I); break;

            case 55: _movement.ChangeToDefault(); break;
            case 56: break;
            case 57: _patrones.PatronSimple(false, false); break;
            case 58: _patrones.PatronSimple(false, false); break;
            case 59: _patrones.LanzarOnda(I); break;

        }

        // Guardar el caso actual para el siguiente frame, y así detectar un posible cambio de caso
        _casoAnterior = _casoActual;
    }


    public void TerceraFase(float timer)
    {
        // Actualizamos el caso actual
        _casoActual = ((int)timer) % 60;

        // Si no ha habido un cambio de caso, entonces salimos de la función
        if (_casoAnterior == _casoActual) return;

        // En este punto, ha habido un cambio de caso y procedemos a lanzar el ataque correspondiente
        switch (_casoActual)
        {
            case 0: _patrones.PatronSimple(false, false); break;
            case 1: _patrones.PatronHorizontal(false, false); break;
            case 2: _patrones.PatronBarrida(false, false); break;
            case 3: _patrones.LanzarOnda(I); break;

            case 4: _patrones.PatronSimple(true, false); break;
            case 5: _patrones.PatronVertical(true, true, false); break;
            case 6: _patrones.PatronHorizontal(true, false); break;
            case 7: break;

            case 8: _patrones.PatronSimple(false, true); break;
            case 9: _patrones.PatronBarrida(false, true); break;
            case 10: _patrones.PatronVertical(false, false, true); break;
            case 11: _patrones.LanzarOnda(P); break;

            case 12: _patrones.PatronSimple(true, true); break;
            case 13: break;
            case 14: _patrones.PatronHorizontal(true, true); break;
            case 15: _patrones.LanzarOnda(G); break;

            case 16: _patrones.PatronBarrida(true, false); break;
            case 17: _patrones.PatronVertical(true, false, false); break;
            case 18: _patrones.PatronSimple(false, false); break;
            case 19: break;

            case 20: _patrones.PatronHorizontal(false, true); break;
            case 21: _patrones.PatronSimple(true, false); break;
            case 22: _patrones.PatronBarrida(true, true); break;
            case 23: _patrones.LanzarOnda(I); break;

            case 24: break;
            case 25: _patrones.PatronVertical(true, true, true); break;
            case 26: _patrones.PatronSimple(true, true); break;
            case 27: _patrones.LanzarOnda(P); break;

            case 28: _patrones.PatronBarrida(false, false); break;
            case 29: _patrones.PatronHorizontal(true, false); break;
            case 30: break;
            case 31: _patrones.LanzarOnda(G); break;

            case 32: _patrones.PatronSimple(false, false); break;
            case 33: _patrones.PatronVertical(false, true, false); break;
            case 34: _patrones.PatronBarrida(false, true); break;
            case 35: break;

            case 36: _patrones.PatronHorizontal(true, true); break;
            case 37: _patrones.PatronSimple(true, false); break;
            case 38: _patrones.PatronVertical(true, false, true); break;
            case 39: _patrones.LanzarOnda(I); break;

            case 40: _patrones.PatronBarrida(true, true); break;
            case 41: break;
            case 42: _patrones.PatronSimple(false, true); break;
            case 43: _patrones.LanzarOnda(P); break;

            case 44: _patrones.PatronHorizontal(false, false); break;
            case 45: _patrones.PatronVertical(true, true, false); break;
            case 46: break;
            case 47: _patrones.LanzarOnda(G); break;

            case 48: _patrones.PatronSimple(true, true); break;
            case 49: _patrones.PatronBarrida(true, false); break;
            case 50: _patrones.PatronHorizontal(false, true); break;
            case 51: break;

            case 52: _patrones.PatronSimple(false, false); break;
            case 53: break;
            case 54: _patrones.PatronVertical(true, false, false); break;
            case 55: _patrones.LanzarOnda(I); break;

            case 56: _patrones.PatronBarrida(true, true); break;
            case 57: _patrones.PatronSimple(false, true); break;
            case 58: break;
            case 59: _patrones.LanzarOnda(G); break;
        }

        // Guardar el caso actual para el siguiente frame, y así detectar un posible cambio de caso
        _casoAnterior = _casoActual;

    }

    public void NextFase()
    {
        _faseActual++;
        // reseteamos timer para que el switch de la siguiente fase comience desde el principio (case 0)
        _timer = 0;
        // Para que detecte cualquier caso cuando hay un cambio de fase
        _casoAnterior = -1;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class Fases 
  // namespace
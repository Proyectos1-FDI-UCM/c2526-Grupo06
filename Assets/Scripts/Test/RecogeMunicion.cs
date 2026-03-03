//---------------------------------------------------------
// Permite recoger instancias de pickups de munición, y se encarga de mantener esta dentro de unos límites
// Adán Calvo Durán
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class RecogeMunicion : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private int MaxAMO = 5; //Munición máxima que el player podra obtener durante la partida

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private int _currentAmo = 0; //Munición acutal del jugador
    private OtorgaMunicion _om; //El componente OtorgaMuncion del pickup de munición más reciente contra el que haya chocado
    private bool _SuckingAmo = true; //Boleano que controla si el jugador puede aborver o no pickups de munición
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    private void OnTriggerEnter2D(Collider2D collision) //Este metodo detecta la entrada a un trigger y cuando el game objet tiene el componente "OtorgaMunicion" toma la munición que este aporta y la suma a la currentAmo siempre que -SuckingAmo estea en true
    {
        _om = collision.gameObject.GetComponent<OtorgaMunicion>();
        if (_om != null && _SuckingAmo) 
        {
            Addamo(_om.MuncionOtorgada);
            Destroy(collision.gameObject);
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

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    private void Addamo(int amount) //Este metodo suma la cantidad de munción ingresada a _currentAmo asegurandose de no sobrepasar el límite
    {
        
        if (!(_currentAmo >= MaxAMO))
        {
            _currentAmo = Mathf.Clamp((_currentAmo + amount), 0, 5);
            Debug.Log("Munición Actual = " + _currentAmo);
        }
        else Debug.Log("Munción ya esta al máximo o el simplemente no se puede dispar");
    }

    public void StopAMOSucking() //Este metodo aparte de tener un gran nombre solo sirve para poner al jugador en un estado donde no puede absorver pickups de munición;
    {
        _SuckingAmo = false;
    }
    #endregion   

} // class RecogeMunicion 
// namespace

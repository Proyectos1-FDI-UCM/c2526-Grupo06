//---------------------------------------------------------
// Este script gestiona el daño que recibe el jefe, y como consecuancia de ese deño,
// genera dos items, el corazón y uno aleatorio entre el escudo y el resize
// Javier de Sala Rodríguez
// Dream O' SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class DroppeoJefe : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private int PuntosVidaMaximos = 10; // Puntos de vida máximos del jefe

    [SerializeField]
    private GameObject[] PowerUps; // lista de power ups posibles

    [SerializeField]
    private float[] umbralesDrop = { 6.67f, 3.33f}; //umbrales de energía que cuando los cruzamos haciendo
                                                    //daño al jefe, este suelta los dos items.
                                                    //los umbrales tienen que estar ordenados de mayor a menor

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private int _puntosVidaRestantes; // Puntos de vida restantes del enemigo
    private int _dropsRealizados; //número de droppeos que ha hecho el jefe

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

    //en el start inicializamos los dropps a 0 y los puntos de vida del jefe
    void Start()
    {
        _puntosVidaRestantes = PuntosVidaMaximos;
        Debug.Log($"Vida Jefe: {_puntosVidaRestantes}");
        _dropsRealizados = 0;
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController
    public void RecibeDanio(int danio) // Método que se llama para hacer que el jefe reciba daño
    {
        
        //este if comprueba si existen o no umbrales que leer del array
        if (_dropsRealizados < umbralesDrop.Length)
        {
            //este if comprueba que si hemos cruzado un umbral, el jefe dropee un item
            if (_puntosVidaRestantes*1f > umbralesDrop[_dropsRealizados] && (_puntosVidaRestantes*1f - danio) < umbralesDrop[_dropsRealizados])
            {
                SoltarPowerUp();
                _dropsRealizados++; // la siguiente vez que pierda vida, buscamos el siguiente umbral de la lista
            }
        }

        _puntosVidaRestantes -= danio;

        if (_puntosVidaRestantes <= 0)
        {
            Muere();
        }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private void Muere() // Método que se llama cuando el jefe muere
    {
        Destroy(gameObject);
    }
    private void SoltarPowerUp() // Método que se llama para soltar 2 items (corazón y aleatorio del resto de power ups)
    {
        if (PowerUps != null && PowerUps.Length > 1)
        {
            //Soltar el primer powerup - Corazón
            Instantiate(PowerUps[0], transform.position, Quaternion.identity);

            // Soltar el segundo powerup aleatorio - Escudo o Resize
            int indice = Random.Range(1, PowerUps.Length);
            Instantiate(PowerUps[indice], transform.position, Quaternion.identity);
        }
    }

    #endregion

} // class VidaEnemigo 
// namespace

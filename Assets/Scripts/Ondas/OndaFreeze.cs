//---------------------------------------------------------
// Comportamiento que permite a las ondas congelantes congelar el movimiento y acciones del jugador, balas y pickups de municion
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
public class OndaFreeze : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private float FreezeTime = 1f;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    private void OnTriggerEnter2D(Collider2D collision) //LLama a la funcion AddFreezeTime de las balas o jugadores que interactuen con el objeto.
    {
        BulletsMovement _bulletEnemy = collision.gameObject.GetComponent<BulletsMovement>();
        if (_bulletEnemy != null)// Bala enemiga o pickup de municion en contacto
        {
            _bulletEnemy.AddFreezeTime(FreezeTime);
        }
        else
        {
            PlayerBulletMovement  _bulletPlayer= collision.gameObject.GetComponent<PlayerBulletMovement>();
            if (_bulletPlayer != null)// Bala aliada en contacto
            {
                _bulletPlayer.AddFreezeTime(FreezeTime);
            }
            else
            {
                if (collision.gameObject.GetComponent<PlayerControler>() != null)//Jugador en contacto
                {
                        collision.gameObject.GetComponent<PlayerControler>().AddFreezeTime(FreezeTime);
                        collision.gameObject.GetComponent<PlayerShooting>().AddFreezeTime(FreezeTime);
                }
            }
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

    #endregion

} // class OndaFreeze 
// namespace

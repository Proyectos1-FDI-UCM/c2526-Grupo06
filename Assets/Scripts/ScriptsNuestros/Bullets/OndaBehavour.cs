//---------------------------------------------------------
// Script configurable que le aporta el comportamiento a las ondas
// Adán Calvo Durán
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.InteropServices;
using TMPro.EditorUtilities;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class OndaBehavour : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    //Selección mediante enumeración del tipo de onda.
    [SerializeField]
    private _tipoDeOnda OndaElegida = _tipoDeOnda.Gravity;
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

    //La enumeración compuesta por los tipos de onda.
    private enum _tipoDeOnda{ Gravity, Freeze, Swap };
    private MonoBehaviour _component;
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
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _component = collision.gameObject.GetComponent<BulletsMovement>();
        if (_component != null)// Bala enemiga o pickup de municion en contacto
        {
            if (OndaElegida == _tipoDeOnda.Gravity)//Onda de gravedad
            {
                collision.gameObject.GetComponent<BulletsMovement>().GravityChange();
            }
            else if (OndaElegida == _tipoDeOnda.Freeze)//Onda de congelamiento
            {
                collision.gameObject.GetComponent<BulletsMovement>().AddFreezeTime(FreezeTime);
            }
            else if (OndaElegida == _tipoDeOnda.Swap)
            {
                EnemyDamageToPlayer _damge = _component.GetComponent<EnemyDamageToPlayer>();
                OtorgaMunicion _givesAmmo = _component.GetComponent<OtorgaMunicion>();
                SpriteRenderer _sprite = _component.GetComponent<SpriteRenderer>();

                _damge.enabled = !_damge.isActiveAndEnabled;
                _givesAmmo.enabled = !_givesAmmo.isActiveAndEnabled;
                if (_damge.isActiveAndEnabled) _sprite.color = (Color.red);
                else _sprite.color = (Color.green);
            }
        }
        else
        {
            _component = collision.gameObject.GetComponent<PlayerBulletMovement>();
            if (_component != null)// Bala aliada en contacto
            {
                if (OndaElegida == _tipoDeOnda.Gravity)//Onda de gravedad
                {
                    collision.gameObject.GetComponent<PlayerBulletMovement>().GravityChange();
                }
                else if (OndaElegida == _tipoDeOnda.Freeze)//Onda de congelamiento
                {
                    collision.gameObject.GetComponent<PlayerBulletMovement>().AddFreezeTime(FreezeTime);
                }
            }
            else
            {
                _component = collision.gameObject.GetComponent<PlayerControler>();
                if (_component != null)//Jugador en contacto
                {
                    if (OndaElegida == _tipoDeOnda.Gravity)//Onda de gravedad
                    {
                    }
                    else if (OndaElegida == _tipoDeOnda.Freeze)//Onda de congelamiento
                    {
                        collision.gameObject.GetComponent<PlayerControler>().AddFreezeTime(FreezeTime);
                        collision.gameObject.GetComponent<PlayerShooting>().AddFreezeTime(FreezeTime);
                    }
                }
                //Pense que la onda de congelación podia afectar al enemigo, ignorar este codigo
                /*    
                    else
                    {
                        _component = collision.gameObject.GetComponent<DisparoEnemigo>();
                        if (_component != null)//Enemigo en contacto
                        {
                            if (OndaElegida == _tipoDeOnda.Gravity)//Onda de gravedad
                            {
                            }
                            else if (OndaElegida == _tipoDeOnda.Freeze)//Onda de congelamiento
                            {
                                collision.gameObject.GetComponent<DisparoEnemigo>().AddFreezeTime(FreezeTime);
                                collision.gameObject.GetComponent<MovimientoEnemigo>().AddFreezeTime(FreezeTime);
                            }
                        }
                    }
                */
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

} // class OndaBehavour 
// namespace

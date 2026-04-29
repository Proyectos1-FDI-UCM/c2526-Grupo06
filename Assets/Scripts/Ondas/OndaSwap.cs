//---------------------------------------------------------
// Permite a cualquier objeto hacer que cuando una bala o pickup de munición entren en contacto con sigo convertirlos en el otro
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
public class OndaSwap : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private Sprite balaRecogible; // Sprite que se le asignará a la bala cuando esta sea recogible, para que el jugador pueda identificarla mejor.
    [SerializeField]
    private Sprite balaDanio; // Sprite que se le asignará a la bala cuando esta no sea recogible, para que el jugador pueda identificarla mejor.

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

    private void OnTriggerEnter2D(Collider2D collision) //Hace que las balas que entren en contacto con el objeto se vuelvan pickups de municion y vicebersa.
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        BulletsMovement _component = collision.gameObject.GetComponent<BulletsMovement>();
        if (_component != null)// Bala enemiga o pickup de municion en contacto
        {
                EnemyDamageToPlayer _damge = _component.GetComponent<EnemyDamageToPlayer>();
                OtorgaMunicion _givesAmmo = _component.GetComponent<OtorgaMunicion>();

                _damge.enabled = !_damge.isActiveAndEnabled;
                _givesAmmo.enabled = !_givesAmmo.isActiveAndEnabled;
                if (_damge.isActiveAndEnabled) _spriteRenderer.sprite = balaRecogible;
                else _spriteRenderer.sprite = balaDanio;
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

} // class OndaSwap 
// namespace

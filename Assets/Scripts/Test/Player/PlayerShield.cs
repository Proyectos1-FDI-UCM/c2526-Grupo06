//---------------------------------------------------------
// Componente del player que gestiona el powerup de escudo
// Pablo Redondo Vaillo
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


public class PlayerShield : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float ShieldMaxTime = 5f;
    [SerializeField]
    private GameObject BulletDestroyer;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _shieldTime = 0;
    private bool _shieldEnabled = false;

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
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
        if (_shieldEnabled)
        {
            _shieldTime += Time.deltaTime;
            if (_shieldTime > ShieldMaxTime)
            {
                DisableShield();
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void EnableShield()
    {
        _shieldTime = 0;
        _shieldEnabled = true;   
    }
    public void DisableShield()
    {
        _shieldEnabled = false;
    }
    public void ShieldAttack()
    {
        _shieldEnabled = false;

    }
    public bool GetShieldState()
    {
        return _shieldEnabled; 
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class PlayerShield 
// namespace

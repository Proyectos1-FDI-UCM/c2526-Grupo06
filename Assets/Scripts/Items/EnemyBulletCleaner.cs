//---------------------------------------------------------
// Componente que destruye balas enemigas y hace crecer un objeto durante un tiempo
// Pablo Redondo Vaillo
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Hace crecer un objeto durante un tiempo, destruyendo toda bala enemiga que toca
/// Luego desactiva el objeto
/// </summary>
public class EnemyBulletCleaner : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float GrowSpeed = 30f; // Velocidad de crecimiento
    [SerializeField]
    private float GrowTime = 1f; // Tiempo de crecimiento

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _timer = 0f; // Tiempo activo
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// OnEnable se llama cuando se habilita el monobehaviour
    /// </summary>
    void OnEnable()
    {
        _timer = 0f; // Reinicia el tiempo que lleva vivo
        transform.localScale = Vector3.one; // Reinicia su escala a la inicial
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Crecimiento
        Vector3 GrowthRate = new(GrowSpeed * Time.deltaTime, GrowSpeed * Time.deltaTime, 0); 
        transform.localScale += GrowthRate;
        // Timer
        _timer += Time.deltaTime;
        if (_timer > GrowTime) { gameObject.SetActive(false); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprueba si la colisión es una bala
        BulletsProp bp = collision.GetComponent<BulletsProp>();
        if (bp != null)
        {
            // Comprueba si la colisión es una bala ENEMIGA
            EnemyDamageToPlayer dp = collision.GetComponent<EnemyDamageToPlayer>();
            // Si el script de daño existe y está activo, destruye la bala
            if (dp != null && dp.enabled)
            {
                bp.DestroyBullet();
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion

} // class EnemyBulletCleaner 
// namespace

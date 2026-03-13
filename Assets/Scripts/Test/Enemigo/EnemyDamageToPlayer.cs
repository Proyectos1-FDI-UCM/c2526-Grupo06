//---------------------------------------------------------
// En este script lo que hacemos es comprobar que una bala o un enemigo ha impactado con el jugador.
// Si lo ha hecho, restamos 1 de vida a la vida actual.
// Javier de Sala Rodríguez
// Dream O' SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class EnemyDamageToPlayer : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private static int _layerPlayer = 10; //el núemro de capa asigando al jugador
    private static int _layerEnemigo = 11; //layer del enemigo

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Detecta cuando hay colisión.
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return; //Añadido para permitir que si el script esta desactivado no haga daño
        
        if (other.gameObject.layer == _layerPlayer) 
        {
            Vida vidaPlayer = other.gameObject.GetComponent<Vida>();

            if (vidaPlayer != null)
            {
                bool HizoDanyo; //viva la ñ
                
                HizoDanyo = vidaPlayer.ActualizarVidas(-1);

                // buscar si lo que ha causado daño tiene componente BulletsProp y si lo tiene destruirlo, y si no, no
                if (HizoDanyo)
                {
                    BulletsProp esBala = GetComponent<BulletsProp>();

                    if (esBala != null)
                    {
                        Destroy(this.gameObject);
                    }
                   
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

} // class EnemyDamageToPlayer 
// namespace

//---------------------------------------------------------
// Movimiento de las balas disparadas por el jugador
// Pablo Redondo Vaillo
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Movimiento de los proyectiles que dispara el jugador 
/// El movimiento es rectilíneo uniforme, con una trayectoria horizontal (eje x de coordenadas) 
/// </summary>
public class PlayerBulletMovement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float Speed = 3f; // Velocidad del objeto
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
    /// <summary>
    /// Cada frame se mueve en una velocidad constante
    /// </summary>
    void Update()
    {
        // Cantidad de espacio a recorrer este frame en el eje x
        Vector3 move = new(Speed * Time.deltaTime, 0,0); 
        transform.position += move; // Movimiento en el eje de coordenadas global
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

} // class PlayerBulletMovement 
// namespace

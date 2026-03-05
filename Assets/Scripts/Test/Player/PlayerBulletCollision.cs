//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// MARTA REYES FUNK
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerBulletCollision : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // No hay

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // No hay

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // <summary>
    // Método que se llama automáticamente cuando el collider de la bala del jugador entra en contacto con otro collider
    // </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<VidaEnemigo>() != null) // si choca con un enemigo
        {
            DestroyPlayerBullet(); // destruye la bala del jugador
            // aquí se podría llamar a un método para reducir la vida del enemigo que estaría en el script VidaEnemigo
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // No hay

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    /// <summary>
    /// Destruye la bala del jugador cuando se llama
    /// </summary>
    private void DestroyPlayerBullet()
    {
        Destroy(gameObject); // adios pookie
    }

    #endregion   

} // class PlayerBulletCollision 
// namespace

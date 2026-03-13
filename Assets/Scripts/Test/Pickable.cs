//---------------------------------------------------------
// Breve descripción del contenido del archivo
//      Componente que se encarga de los pickups y su interacción con el jugador
//      Activa su efecto y luego se destruye a sí mismo

// Responsable de la creación de este archivo
//      MARTA REYES FUNK

// Nombre del juego
//      Dream O' Spacesheep

// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Pickable : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private PickableType PickUp;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    /// <summary>
    /// En el enumerado quedan recogidos los tipos de pickups que hay
    /// </summary>
    private enum PickableType
    {
        GanaVida,
        Escudo,
        ReduceTamanyo
    }

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// El método se llama al detectar una colisión con el pickup
    /// Primero se comprueba que quien ha chocado es el jugador
    /// Luego se llama al método concreto de efecto dependiendo del tipo de pickup que sea
    /// Hay además un debug warning por si el jugador no tiene el componente con la lógica de ese pickup
    /// Finalemente se destruye el pickup después de activar su efecto
    /// </summary>
    void OnTriggerEnter2D(Collider2D collider)
    {
        // muchas cosas están comentadas porque aún no se han implementado
        if (collider.GetComponent<PlayerControler>() != null) // si el collider que ha entrado es el del jugador
        {
            switch (PickUp)
            {
                /*case PickableType.GanaVida:
                    // se llama al componente concreto que tendrá la lógica de este pickup
                    if (collider.GetComponent<PickUpVida>() != null)
                    {
                        // collider.GetComponent<PickUpVida>().Heal(1); // esto es un ejemplo
                        // se llama al método que cura (aún no está hecho)
                    }
                    else
                    {
                        Debug.LogWarning("No está el componente PickUpVida, no se puede aplicar el efecto de este pickup");
                    }
                    break;*/
                case PickableType.Escudo:
                    // Se busca el componente escudo dentro del player
                    PlayerShield ps = collider.GetComponent<PlayerShield>();
                    if (ps != null)
                    {
                        // Se activa el escudo
                        ps.EnableShield();
                    }
                    else
                    {
                        Debug.LogWarning("No está el componente PickUpEscudo, no se puede aplicar el efecto de este pickup");
                    }
                    DestroyPickUp(); // se destruye el pickup después de activar su efecto
                    break;
                    /*case PickableType.ReduceTamanyo:
                        // se llama al componente concreto que tendrá la lógica de este pickup
                        if (collider.GetComponent<PickUpSmall>() != null)
                        {
                            // collider.GetComponent<PickUpSmall>().ReduceSize(); // esto es un ejemplo
                            // se llama al método que cura (aún no está hecho)
                        }
                        else
                        {
                            Debug.LogWarning("No está el componente PickUpSmall, no se puede aplicar el efecto de este pickup");
                        }
                        break;*/
            }
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
    /// se destruye el pickup después de activar su efecto
    /// </summary>
    private void DestroyPickUp()
    {
        Destroy(gameObject); // adiós pookies
    }

    #endregion

} // class Pickable 
// namespace

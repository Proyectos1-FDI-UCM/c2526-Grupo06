//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class SoundEffectsManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private AudioSource SoundFXobject;
    #endregion
    
    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    public static SoundEffectsManager instance;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    private void Awake()
    {
        if (instance != null) 
        {
            DestroyImmediate(this);
        }
        else
        { 
            instance = this; 
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

    public void PlaySoundFXClip(AudioClip audioClip, Transform soundPosition, float volume)
    {
        //Instancia del objeto con el sonido
        AudioSource audioSource = Instantiate(SoundFXobject, soundPosition.position, Quaternion.identity);
        //Asignamos el clip de sonido
        audioSource.clip = audioClip;
        //Configuramos  el volumen del sonido
        audioSource.volume = volume;
        //Reproducimos el sonido
        audioSource.Play();
        //Destruimos el objeto tras la duración del clip de audio
        float clipDuration = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipDuration);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform soundPosition, float volume)
    {
        //Instancia del objeto con el sonido
        AudioSource audioSource = Instantiate(SoundFXobject, soundPosition.position, Quaternion.identity);
        //Asignamos el clip de sonido, elegidiendo uno aleatorio del array
        audioSource.clip = audioClip[Random.Range(0,audioClip.Length)];
        //Configuramos  el volumen del sonido
        audioSource.volume = volume;
        //Reproducimos el sonido
        audioSource.Play();
        //Destruimos el objeto tras la duración del clip de audio
        float clipDuration = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipDuration);
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class SoundEffectsManager 
// namespace

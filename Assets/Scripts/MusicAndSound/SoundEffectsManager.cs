//---------------------------------------------------------
// Este script controla la instanciación de sonidos mediante metodos
// Adán Calvo Durán
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] 
    private AudioSource MusicObjet;
    [SerializeField]
    private AudioClip[] Musics;

    // añadido de javier para los sfx de los botones de los menús
    [SerializeField]
    private AudioClip SonidoClicarBoton;
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
    AudioSource _musicSource;
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
            Destroy(gameObject);
        }
        else
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (Musics != null && Musics.Length > 0 && Musics[0] != null) PlayMusic(0, 1);
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

    public void PlayMusic(int i, float volume)
    {
        //Instacia el objeto de la musica
        if (_musicSource == null) _musicSource = Instantiate(MusicObjet, Vector3.zero, Quaternion.identity);
        //Asignamos el clip de música
        _musicSource.clip = Musics[i];
        //Configuramos  el volumen de la música
        _musicSource.volume = volume;
        //Reproducimos el sonido
        _musicSource.Play();
    }

    // método que lo que hace es hacer que al clicar un botón este tenga un efecto de sonido
    public void OnClick()
    {
        if (SonidoClicarBoton != null)
        {
            // instanciamos el sonido para que al cambiar de escena no desaparezca
            AudioSource audioSource = Instantiate(SoundFXobject, transform.position, Quaternion.identity);
            audioSource.clip = SonidoClicarBoton;
            audioSource.volume = 1;
            audioSource.Play();

            // sonido persistente entre escenas
            DontDestroyOnLoad(audioSource.gameObject);

            // la escena se destruye cuando el sonido se ha escuchado
            Destroy(audioSource.gameObject, SonidoClicarBoton.length);
        }
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

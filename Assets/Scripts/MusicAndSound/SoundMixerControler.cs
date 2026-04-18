//---------------------------------------------------------
// Este componente controla los mixers de sonido y el paso de valores de los sliders de audio de una escena a otra.
// Adán Calvo Durán
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using
using UnityEngine.Audio;
using UnityEngine.VFX;
using UnityEngine.UI;


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class SoundMixerControler : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField] private AudioMixer MezcladorAudio;

    #endregion
    [SerializeField] private Slider SliderVolumeMaster;
    [SerializeField] private Slider SliderVolumeMusic;
    [SerializeField] private Slider SliderVolumeSFX;



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
    private void Start()
    {
        //Asignamos a los Sliders y al mezclador los valores almacenados
        //Master
        float VolumeMaster = PlayerPrefs.GetFloat("MasterVolumeSaved", 1f);
        SliderVolumeMaster.value = VolumeMaster;
        SetMasterVolume(VolumeMaster);
        //Music
        float VolumeMusic = PlayerPrefs.GetFloat("MusicVolumeSaved", 1f);
        SliderVolumeMusic.value = VolumeMusic;
        SetMusicVolume(VolumeMusic);
        //SFX
        float VolumeSFX = PlayerPrefs.GetFloat("SFXVolumeSaved", 1f);
        SliderVolumeSFX.value = VolumeSFX;
        SetSFXVolume(VolumeSFX);
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController
    public void SetMasterVolume(float volume) //Cambiar el valor del mezclador para Main
    {
        MezcladorAudio.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("MasterVolumeSaved", volume);
    }
    public void SetMusicVolume(float volume) //Cambiar el valor del mezclador para Musica
    {
        MezcladorAudio.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("MusicVolumeSaved", volume);

    }
    public void SetSFXVolume(float volume) //Cambiar el valor del mezclador para SFX
    {
        MezcladorAudio.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("SFXVolumeSaved", volume);
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class SoundMixerManager 
// namespace

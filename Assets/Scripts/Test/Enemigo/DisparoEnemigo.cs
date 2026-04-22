//---------------------------------------------------------
// Disparo básico del enemigo
// Sergio Navarro Herreros
// Dream O'SpaceSheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class DisparoEnemigo : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private GameObject BulletNormal; //Prefab de la bala normal
    [SerializeField]
    private Transform puntoDisparo;  // Punto de disparo del enemigo
    [SerializeField]
    private float tiempoEntreDisparos = 2f;

    [SerializeField]
    private AudioClip[] SonidoDisparoLittleGuy;


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private float _timer; // Temporizador para controlar el tiempo entre disparos

    private float _freezeTimer = 0f;//(Añadido de Adán) esta variable se utilizara para contar cuanto tiempo le queda congelada

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_freezeTimer > 0) //(Añadido de Adán) si esta congelada no hace nada más que reducir su timer de congelación.
        {
            _freezeTimer -= Time.deltaTime;
            if (_freezeTimer < 0) _freezeTimer = 0;
        }
        else
        {
            _timer += Time.deltaTime;

            if (_timer >= tiempoEntreDisparos)
            {
                Disparar();
                _timer = 0f;
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
    private void Disparar() //Instancia un proyectil desde el punto de disparo del enemigo.
    {
        Instantiate(BulletNormal, puntoDisparo.position, puntoDisparo.rotation);
        if (SoundEffectsManager.instance != null) SoundEffectsManager.instance.PlayRandomSoundFXClip(SonidoDisparoLittleGuy, transform, 1f);

    }

    public void AddFreezeTime(float freeze)//(Añadido de Adán) Añade tiempo de congelación
    {
        _freezeTimer += freeze;
    }
    #endregion   

} // class DisparoEnemigo 
// namespace

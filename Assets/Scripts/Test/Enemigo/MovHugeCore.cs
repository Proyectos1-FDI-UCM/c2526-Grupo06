//---------------------------------------------------------
// Script que define el comportamiento del Huge Core
// Alejandro de Haro Morales
// Dream'O Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class MovHugeCore : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private GameObject BulletNormal; //Prefab de la bala normal
    [SerializeField]
    private float _posX; //posición X hasta la cual debe de llegar
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private Vector3 _posIni;            //para poner la posición inicial al instanciarlo
    private Vector3 _endPos;            //para guardar la posición que debe de tomar
    private float _vel = 4f;            // Velocidad de movimiento del enemigo
    private float _amp = 3f;            //Amplitud del movimiento del enemigo
    private float _freezeTimer = 0f;    //(Añadido de Adán) esta variable se utilizara para contar cuanto tiempo le queda congelada
    private float _maxOffset = 0.2f;    //guarda un offset para comparar posiciones
    private float _cadencia = 2f;   //cada cuanto dispara
    private float _timerCad = 0f;   //temporizador de disparo

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour


    void Start()
    {
        _posIni = transform.position;
        _endPos = new Vector3(_posX, _posIni.y, _posIni.z);
    }

    void Update()
    {
        if (_freezeTimer > 0) //(Añadido de Adán) si esta congelada no hace nada más que reducir su timer de congelación.
        {
            _freezeTimer -= Time.deltaTime;
            if (_freezeTimer < 0) _freezeTimer = 0;
        }
        else if (!Equal(transform.position.x, _posX, _maxOffset))
        {
            float nuevaY = _posIni.y + Mathf.Sin(Time.time * _vel) * _amp;
            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
            _endPos = new Vector3(_posX, nuevaY, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, _endPos, _vel * Time.deltaTime);
        }
        else
        {
            float nuevaY = _posIni.y + Mathf.Sin(Time.time * _vel) * _amp;
            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);

            _timerCad += Time.deltaTime;

            if (_timerCad >= _cadencia)
            {
                Disparar();
                _timerCad = 0f;
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    

    public void AddFreezeTime(float freeze)//(Añadido de Adán) Añade tiempo de congelación
    {
        _freezeTimer += freeze;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    private bool Equal(float v1, float v2, float v3)    //método para comparar coordenadas con un rango de error
    {
        return System.Math.Abs(v1 - v2) <= v3 && System.Math.Abs(v1 - v2) >= 0;
    }
    private void Disparar() //Instancia un proyectil desde el punto de disparo del enemigo.
    {
        GameObject spawned;
        float j = 0.9f;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0 || i == 3)
            {
                Vector2 v2 = new Vector2(transform.position.x, transform.position.y + j);
                Vector3 posInst = new Vector3(v2.x, v2.y, transform.position.z);
                spawned = Instantiate(BulletNormal, posInst, transform.rotation);
            }
            else
            {
                Vector2 v2 = new Vector2(transform.position.x - 0.6f, transform.position.y + j);
                Vector3 posInst = new Vector3(v2.x, v2.y, transform.position.z);
                spawned = Instantiate(BulletNormal, posInst, transform.rotation);
            }
            if (Random.value <= 0.4f)
            {
                spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
                spawned.GetComponent<OtorgaMunicion>().enabled = true;
            }
            j = j - 0.6f;
        }
    }
    #endregion

} // class MovHugeCore 
// namespace

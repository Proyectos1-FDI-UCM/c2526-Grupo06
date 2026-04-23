//---------------------------------------------------------
// Script que establece los patrones de ataque del jefe por métodos públicos
// Alejandro de Haro & Adán Calvo Durán
// Dream'O Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Buffers.Text;
using UnityEngine;
// Añadir aquí el resto de directivas using

public class PatronManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private GameObject BulletNormal; //Prefab de la bala normal

    [SerializeField]
    private GameObject _intercambia;
    [SerializeField]
    private GameObject _gravedad;
    [SerializeField]
    private GameObject _paraliza;

    [SerializeField]
    private AudioClip SonidoDisparoJefe;

    [SerializeField]
    private AudioClip SonidoSpawnOnda;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _cadencia;        //cadencia modificable de los disparos
    private float _timerCad = 0f;   //timer para medir tiempo
    private GameObject _boss;       //el gameobject boss
    private Vector3 _posInst;       //posicion de instanciamiento
    private GameObject spawned, spawned1;     //variable gameobject para la bala instanciada
    private bool _horiz = false, _barrido = false, _acelera, _curvo; //parametros para el update
    private int _numTotal = 12, _indice;     //numero de balas a instanciar para patrones barrida y horizontal e indice
    private float _numRandom;
    private bool pedido = false;

    private BossMovement _movimiento;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Selecciona la instancia del boss usando el método del GameManager
    /// Tambien pilla la cadencia base de disparo y el script del movimiento del boss
    /// </summary>
    void Start()
    {
        if (GameManager.Instance != null) GameManager.Instance.SetBoss(this.gameObject);
        _movimiento = this.GetComponent<BossMovement>();
        _cadencia = _movimiento.GetCurrentAmpTime() / _numTotal;
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Método para el patrón simple, todas las balas se instancias al instante
    /// </summary>
    public void PatronSimple(bool acelera, bool curvo)
    {
        float altura = 1.8f;
        float lados;
        float x = Random.value;
        _movimiento.ChangeToDefault();

        for (int i = 0; i < 6; i++)
        {
            if (SoundEffectsManager.instance != null) SoundEffectsManager.instance.PlaySoundFXClip(SonidoDisparoJefe, transform, 1f);
            lados = CalcularFuncion(i, transform.position.x);
            _posInst = new Vector3(lados, transform.position.y + altura, transform.position.z);
            spawned = Instantiate(BulletNormal, _posInst, transform.rotation);

            spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);
            if (acelera ^ curvo && bullet != null) bullet.SelectBulletType(acelera, curvo);
            if (i < 3 && x <= 0.5f)
            {
                spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
                spawned.GetComponent<OtorgaMunicion>().enabled = true;
            }

            altura = altura - 0.72f;
        }
    }
    /// <summary>
    /// Metodo para invocar el patron de ataque vertical
    /// </summary>
    public void PatronVertical(bool up, bool acelera, bool curvo)
    {
        
        float _x = Random.value; //Valor aleatorio para determinar si disparara o no pickups
        float _inicioY = 0f; //Distancia desde donde spawnean las balas en y
        float _inicioX = 2f; //Distancia desde donde spawnean las balas en x
        int order = 0; //Variable que permite saber si las balas deben instanciarse de arriba abajo o viceversa
        if (SoundEffectsManager.instance != null) SoundEffectsManager.instance.PlaySoundFXClip(SonidoDisparoJefe, transform, 1f);
        if (up)
        {
            _movimiento.ChangeToAtaqueVerticalUp();
            _inicioY = -4.7f;
            order = 1;
        }
        else
        {
            _movimiento.ChangeToAtaqueVerticalDown();
            _inicioY = 4.7f;
            order = -1;
        }
        for (float i = 0; i < 12; i++)
        {
                GameObject spawned = Instantiate(BulletNormal, new Vector3(transform.position.x - _inicioX, _inicioY + i / 2 * order, 0), transform.rotation);
                spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);
                if (acelera ^ curvo && bullet != null) bullet.SelectBulletType(acelera, curvo);
                if (_x >= 0.5f && i < 6)
                {
                    spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
                    spawned.GetComponent<OtorgaMunicion>().enabled = true;
                }

            
        }
    }
    /// <summary>
    /// Método para el patrón horizontal
    /// </summary>
    public void PatronHorizontal(bool acelera, bool curvo)
    {
        float baseX = transform.position.x - 1.5f;
        float baseY = transform.position.y - 1.5f;
        float j = Random.value;


        for (int i = 0; i < 6; i++)
        {
            if (SoundEffectsManager.instance != null) SoundEffectsManager.instance.PlaySoundFXClip(SonidoDisparoJefe, transform, 1f);
            float x = baseX + i * 0.5f;

            _posInst = new Vector3(x, baseY, transform.position.z);
            spawned = Instantiate(BulletNormal, _posInst, transform.rotation);
            spawned1 = Instantiate(BulletNormal, new Vector3(x, baseY + 0.5f, transform.position.z), transform.rotation);

            spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);
            if (acelera ^ curvo && bullet != null) bullet.SelectBulletType(acelera, curvo);

            spawned1.TryGetComponent<BulletsMovement>(out BulletsMovement bullet1);
            if (acelera ^ curvo && bullet1 != null) bullet1.SelectBulletType(acelera, curvo);

            if (i < 3 && j <= 0.5f)
            {
                spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
                spawned.GetComponent<OtorgaMunicion>().enabled = true;


                spawned1.GetComponent<EnemyDamageToPlayer>().enabled = false;
                spawned1.GetComponent<OtorgaMunicion>().enabled = true;
            }
             
        }

        
    }
    /// <summary>
    /// Método para patrón barrida (las balas se instancian el los topes de la pantalla de forma diagonal)
    /// </summary>
    public void PatronBarrida(bool acelera, bool curvo)
    {
        float altura = 0f;
        float lados;
        float x = Random.value;
        _movimiento.ChangeToDefault();

        for (int i = 0; i < 3; i++)
        {
            if (SoundEffectsManager.instance != null) SoundEffectsManager.instance.PlaySoundFXClip(SonidoDisparoJefe, transform, 1f);
            lados = CalcularFuncion(i, transform.position.x);
            _posInst = new Vector3(lados, 2.8f + altura, transform.position.z);
            Vector3 second = new Vector3(lados, -2.8f + -altura, transform.position.z);

            spawned = Instantiate(BulletNormal, _posInst, transform.rotation);
            spawned1 = Instantiate(BulletNormal, second, transform.rotation);

            spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);
            if (acelera ^ curvo && bullet != null) bullet.SelectBulletType(acelera, curvo);
            if (i < 3 && x <= 0.5f)
            {
                spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
                spawned.GetComponent<OtorgaMunicion>().enabled = true;
            }
            altura = altura + 0.6f;
        }
    }
    /// <summary>
    /// Método que spawnea las ondas que el jefe lanza 
    /// </summary>
    public void LanzarOnda(char letra)
    {
        float _inicioY = 0f; //Distancia desde donde spawnean las balas en y
        float _inicioX = 2f; //Distancia desde donde spawnean las balas en x
        if (SoundEffectsManager.instance != null) SoundEffectsManager.instance.PlaySoundFXClip(SonidoSpawnOnda, transform, 1f);

        switch (letra)
        {
            case ('I'):
                GameObject intercambiadora = Instantiate(_intercambia,
                new Vector3(transform.position.x - _inicioX, _inicioY / 2, 0), transform.rotation);
                break;
            case ('P'):
                GameObject paralizante = Instantiate(_paraliza,
                new Vector3(transform.position.x - _inicioX, _inicioY / 2, 0), transform.rotation);
                break;
            case ('G'):
                GameObject gravedad = Instantiate(_gravedad,
                new Vector3(transform.position.x - _inicioX, _inicioY / 2, 0), transform.rotation);
                break;
        }

    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    /// <summary>
    /// funcion por trozos necesaria para el patron simple
    /// </summary>
    private float CalcularFuncion(int x, float y) 
    {
        // f(x) = y-3 si  x = 0 o x = 5
        // f(x) = y-4 si  x = 1 o x = 4
        // f(x) = y-5 si  x = 2 o x = 3

        if (x == 0 || x == 5) return y - 2;
        else if (x == 1 || x == 4) return y - 2.5f;
        else return y - 3;
    }

    #endregion

} // class PatronManager 
// namespace

//---------------------------------------------------------
// Script que establece los patrones de ataque del jefe por métodos públicos
// Alejandro de Haro
// Dream'O Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Drawing;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;   //necesario para corutinas
// Añadir aquí el resto de directivas using

public class PatronManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private GameObject BulletNormal; //Prefab de la bala normal
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _cadencia;        //cadencia modificable de los disparos
    private float _timerCad = 0f;   //timer para medir tiempo
    private Transform _boss;
    private Vector3 _posInst;
    private GameObject spawned;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        if (GameManager.Instance != null) GameManager.Instance.SetBoss(this.gameObject);

        //Para pruebas

        PatronSimple(true,false);
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

        for (int i = 0; i < 6; i++)
        {
            lados = CalcularFuncion(i, transform.position.x);
            _posInst = new Vector3(lados, transform.position.y + altura, transform.position.z);
            spawned = Instantiate(BulletNormal, _posInst, transform.rotation);

            spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);
            if (acelera ^ curvo) bullet.SelectBulletType(acelera, curvo);
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
        int order = 0; //Variable que permite saber si las balas deben instanciarse de arriba abajo o vicebersa
        if (up)
        {
            this.gameObject.GetComponent<BossMovement>().ChangeToAtaqueVerticalUp();
            _inicioY = -4.7f;
            order = 1;
        }
        else
        {
            this.gameObject.GetComponent<BossMovement>().ChangeToAtaqueVerticalDown();
            _inicioY = 4.7f;
            order = -1;
        }
        for (float i = 0; i < 12; i++)
        {
            GameObject spawned = Instantiate(BulletNormal, new Vector3(transform.position.x - _inicioX, _inicioY + i / 2 * order, 0), transform.rotation);
            spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);
            if (acelera ^ curvo) bullet.SelectBulletType(acelera, curvo);
            if (_x >= 0.5f && i < 6)
            {
                spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
                spawned.GetComponent<OtorgaMunicion>().enabled = true;
            }
        }
    }
    /// <summary>
    /// Patrón que sirve para patrones que instancien balas de seguido
    /// </summary>
    public void PatronCadencia(bool acelera, bool curvo, int n, int x, int indice)
    {
        spawned = Instantiate(BulletNormal, transform.position, transform.rotation);

        spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);
        if (acelera ^ curvo) bullet.SelectBulletType(acelera, curvo);

        if (indice > n/2 && x <= 0.5f)
        {
            spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
            spawned.GetComponent<OtorgaMunicion>().enabled = true;
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

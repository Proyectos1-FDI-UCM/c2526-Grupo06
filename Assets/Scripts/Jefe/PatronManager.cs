//---------------------------------------------------------
// Script que establece los patrones de ataque del jefe por métodos públicos
// Alejandro de Haro
// Dream'O Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Drawing;
using UnityEngine;
using System.Collections;   //necesario para corutinas
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
        StartCoroutine(PatronHorizontal(false, true));
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
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
    public IEnumerator PatronBarrida(bool acelera, bool curvo)
    {
        int n = 12;

        float separacionY = 0.5f; // separación vertical
        float separacionX = 0.3f; // cuánto se desplaza en X cada bala

        float inicioY = -1.5f; // empieza abajo

        _cadencia = 0.08f;

        for (int i = 0; i < n; i++)
        {
            //Y: sube (de abajo a arriba)
            float posY = transform.position.y + inicioY + (i * separacionY);

            //X: las de abajo más a la izquierda
            float posX = transform.position.x - 2f - (i * separacionX);

            Vector3 posInst = new Vector3(posX, posY, transform.position.z);

            GameObject spawned = Instantiate(BulletNormal, posInst, transform.rotation);

            spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);

            if (acelera ^ curvo)
                bullet.SelectBulletType(acelera, curvo);

            yield return new WaitForSeconds(_cadencia);
        }
    }
    public void PatronVertical(bool acelera, bool curvo)
    {

    }
    public IEnumerator PatronHorizontal(bool acelera, bool curvo)
    {
        int n = 12;
        if (acelera) n = 16;
        _cadencia = 0.1f; //aquí va la cadencia de tomar el tiempo de accion y dividirlo entre el numero de balas
        float x = Random.value;
        _posInst = new Vector2(transform.position.x - 2, transform.position.y);

        for (int i = 0; i < n; i++)
        {
            GameObject spawned = Instantiate(BulletNormal, _posInst, transform.rotation);
            spawned.TryGetComponent<BulletsMovement>(out BulletsMovement bullet);

            if (acelera ^ curvo)
                bullet.SelectBulletType(acelera, curvo);

            if (i > n / 2 && x <= 0.5f)
            {
                spawned.GetComponent<EnemyDamageToPlayer>().enabled = false;
                spawned.GetComponent<OtorgaMunicion>().enabled = true;
            }

            yield return new WaitForSeconds(_cadencia); // 🔑 aquí sí espera
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

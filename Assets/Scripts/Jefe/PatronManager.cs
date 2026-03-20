//---------------------------------------------------------
// Script que establece los patrones de ataque del jefe por métodos públicos
// Alejandro de Haro && ???
// Dream'O Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using

public class PatronManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _cadencia;        //cadencia modificable de los disparos
    private float _timerCad = 0f;   //timer para medir tiempo
    private GameObject _boss;

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    void Start()
    {
        _boss = GameManager.Instance.GetBoss();
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void PatronSimple()
    {

    }
    public void PatronBarrida()
    {

    }
    public void PatronVertical()
    {

    }
    public void PatronHorizontal()
    {

    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class PatronManager 
// namespace

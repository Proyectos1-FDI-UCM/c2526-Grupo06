//---------------------------------------------------------
// Script para cambiar el tamaño del botón al pasar el ratón por encima
// MARTA REYES FUNK
// Dream O' Spacesheep
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Script para cambiar el tamaño del botón al pasar el ratón por encima
/// </summary>
public class HoverChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float sizeChangeFactor = 1.2f; // factor de cambio de tamaño

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private Vector3 _originalScale; // guarda el tamaño original del botón
    private RectTransform _rectTransform; // referencia al RectTransform del botón para cambiar su tamaño

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>(); // se obtiene la referencia
        _originalScale = _rectTransform.localScale; // guarda el tamaño original al iniciar
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.localScale = _originalScale * sizeChangeFactor; // aumenta el tamaño al pasar el ratón
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.localScale = _originalScale; // devuelve el tamaño original al salir el ratón
    }
    public void OnSelect(BaseEventData eventData)
    {
        _rectTransform.localScale = _originalScale * sizeChangeFactor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _rectTransform.localScale = _originalScale;
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos


    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados


    #endregion

} // class HoverChange 
// namespace

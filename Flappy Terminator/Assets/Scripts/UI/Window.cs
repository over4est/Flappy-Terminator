using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Window : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private Button _button;

    public event Action ButtonClicked;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionOnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ActionOnClick);
    }

    public void Close()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
        _button.interactable = false;
    }

    public void Open()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        _button.interactable = true;
    }

    private void ActionOnClick()
    {
        ButtonClicked?.Invoke();
    }
}
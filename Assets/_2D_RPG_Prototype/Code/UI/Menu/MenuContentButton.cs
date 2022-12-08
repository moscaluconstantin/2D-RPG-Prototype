using Assets._2D_RPG_Prototype.Code.Enums;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuContentButton : MonoBehaviour
{
    [SerializeField] private MenuContentType _contentType;

    [Header("Components")]
    [SerializeField] private GameObject _default;
    [SerializeField] private GameObject _selected;

    public event Action<MenuContentType> OnClicked;

    private Button _button;

    public MenuContentType ContentType => _contentType;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnDestroy() =>
        _button.onClick.RemoveListener(OnClick);

    private void OnClick() =>
        OnClicked?.Invoke(_contentType);

    public void Select() =>
        SetSelectedState(true);

    public void Deselect() =>
        SetSelectedState(false);

    private void SetSelectedState(bool selected)
    {
        _default.SetActive(!selected);
        _selected.SetActive(selected);
    }
}

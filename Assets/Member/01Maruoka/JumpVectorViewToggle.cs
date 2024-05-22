using System;
using UnityEngine;
using UnityEngine.UI;

public class JumpVectorViewToggle : MonoBehaviour
{
    private static bool _isShow = true;
    [SerializeField] private JumpVectorView _jumpVectorView;
    [SerializeField] private Toggle _toggle;

    private void Start()
    {
        _toggle.isOn = _isShow;
    }

    private void Update()
    {
        _isShow = _toggle.isOn;
        _jumpVectorView.gameObject.SetActive(_isShow);
    }
}
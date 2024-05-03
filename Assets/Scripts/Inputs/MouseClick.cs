using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseClick : MonoSingleton<MouseClick>
{
    [SerializeField] private PlayerInput m_playerInput;

    public bool isInput {
        get {
            bool temp = _isInput;
            if(_isInput) _isInput = false;
            return temp;
        }
    }

    private bool _isInput = false;

    protected override void Init(){
        m_playerInput.actions["MouseClick"].started += OnPressEnter;
        m_playerInput.actions["MouseClick"].performed += OnMouseLeftClick;
        m_playerInput.actions["MouseClick"].canceled += OnMouseFinished;
    }

    private void OnMouseLeftClick(InputAction.CallbackContext context){
        if (EventSystem.current.IsPointerOverGameObject()){
            _isInput = true;
        }

    }
    private void OnPressEnter(InputAction.CallbackContext context){
        if (EventSystem.current.IsPointerOverGameObject()){
            _isInput = true;
        }

    }
    public void OnMouseFinished(InputAction.CallbackContext context){
        _isInput = false;
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[System.Serializable]
public class ClickEvent : UnityEvent<GameObject>{}

public class MouseClick : MonoSingleton<MouseClick>
{
    [SerializeField] private PlayerInput m_playerInput;
    public ClickEvent onClick = new ClickEvent();

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
        DialogueAction();
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)){
            GameObject clickedObject = hit.collider.gameObject;
            onClick.Invoke(clickedObject);
        }
    }
    private void OnPressEnter(InputAction.CallbackContext context){
        DialogueAction();
    }

    private void DialogueAction(){
        if (EventSystem.current.IsPointerOverGameObject()){
            _isInput = true;
        }
    }

    public void OnMouseFinished(InputAction.CallbackContext context){
        _isInput = false;
    }
}

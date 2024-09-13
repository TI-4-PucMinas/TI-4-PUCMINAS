using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public abstract class InputListener
{
    public InputType SelectedInputType { get; private set; }
    public bool IsActive { get; private set; }

    protected InputDatabase _inputDatabase { get; private set; }

    InputSystem _inputSystem;

    public InputListener(InputType inputType) 
    {
        SelectedInputType = inputType;

        _inputSystem = new();

        _inputDatabase = new(_inputSystem, inputType);
    } 

    public void Enable()
    {
        IsActive = true;

        _inputSystem.Enable();

        foreach (InputAction input in _inputDatabase.Inputs)
        {
            SubscribeToInputSystem(input);
        }
    }

    public void Disable()
    {
        IsActive = false;

        _inputSystem.Disable();

        foreach (InputAction input in _inputDatabase.Inputs)
        {
            UnsubscribeFromInputSystem(input);
        }
    }

    void SubscribeToInputSystem(InputAction input)
    {
        input.started += OnInputStarted;
        input.performed += OnInputPerformed;
        input.canceled += OnInputCanceled;
    }

    void UnsubscribeFromInputSystem(InputAction input)
    {
        input.started -= OnInputStarted;
        input.performed -= OnInputPerformed;
        input.canceled -= OnInputCanceled;
    }

    protected abstract void OnInputStarted(InputAction.CallbackContext context);

    protected abstract void OnInputPerformed(InputAction.CallbackContext context);

    protected abstract void OnInputCanceled(InputAction.CallbackContext context);
}

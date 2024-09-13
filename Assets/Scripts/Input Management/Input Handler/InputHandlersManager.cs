using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandlersManager : MonoBehaviour
{
    InputListenersManager _inputListenersManager;
    public Dictionary<InputType, InputHandler> InputHandlers { get; private set; }  

    void Awake()
    {
        InputHandlers = new();
    }

    void Start()
    {
        _inputListenersManager = InputListenersManager.Instance;
        InputHandlers[InputType.Gameplay] = new GameplayInputHandler();
        InputHandlers[InputType.Menu] = new MenuInputHandler();
    }

    void FixedUpdate()
    {
        UpdateActiveInputListener();

        HandleHeardInputs();
    }

    void HandleHeardInputs()
    {
        InputHandler activeInputHandler = InputHandlers.Values
            .FirstOrDefault(inputHandler => inputHandler.IsActive);

        if (activeInputHandler == null) return;

        if(activeInputHandler is not IFrameDependant)
            activeInputHandler.HandleHeardInputs();
    }

    void UpdateActiveInputListener()
    {
        if (_inputListenersManager.ActiveInputListener == null) 
        {
            DisableActiveInputHandlers();
            return;
        }

        foreach (InputType inputType in InputHandlers.Keys)
        {
            if (_inputListenersManager.ActiveInputListener.SelectedInputType == inputType && InputHandlers[inputType].IsActive) break;

            if (_inputListenersManager.ActiveInputListener.SelectedInputType == inputType && !InputHandlers[inputType].IsActive)
            {
                DisableActiveInputHandlers();

                InputHandlers[inputType].IsActive = true;

                if (InputHandlers[inputType] is IFrameDependant)
                {
                    ((IFrameDependant)InputHandlers[inputType]).Enable();
                }

                switch(InputHandlers[inputType])
                {
                    case GameplayInputHandler:
                        ((GameplayInputHandler)InputHandlers[inputType]).HeardInputs = ((GameplayInputListener)_inputListenersManager.ActiveInputListener).HeardInputs;
                        break;
                    case MenuInputHandler:
                        ((MenuInputHandler)InputHandlers[inputType]).HeardInputs = ((MenuInputListener)_inputListenersManager.ActiveInputListener).HeardInputs;
                        break;
                };

                break;
            }
        }
    }

    void DisableActiveInputHandlers()
    {
        foreach (InputType inputType in InputHandlers.Keys)
        {
            if (InputHandlers[inputType].IsActive)
            {
                InputHandlers[inputType].IsActive = false;

                if (InputHandlers[inputType] is IFrameDependant)
                {
                    ((IFrameDependant)InputHandlers[inputType]).Disable();
                }
            }
        }
    }
}

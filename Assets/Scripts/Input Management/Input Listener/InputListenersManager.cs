using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListenersManager : MonoBehaviour
{
    [SerializeField] InputListenersManagerData _data;

    public static InputListenersManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject sceneObject = new();
                sceneObject.name = "Input Listeners Manager";
                _instance = sceneObject.AddComponent<InputListenersManager>();
            }

            return _instance;
        }
    }
    public InputListener ActiveInputListener { get; private set; }

    static InputListenersManager _instance;

    [SerializeField] InputType _activeInputType;
    Dictionary<InputType ,InputListener> _inputListeners;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance != null && _instance != this) Destroy(this);
        else _instance = this;

        _inputListeners = new();
    }

    void Start()
    {
        _activeInputType = _data.DefaultInputListener;

        _inputListeners[InputType.Gameplay] = new GameplayInputListener();
        _inputListeners[InputType.Menu] = new MenuInputListener();
    }

    void OnEnable()
    {
        UpdateActiveInputListener();
    }

    void OnDisable()
    {
        DisableActiveInputListeners();
    }

    void FixedUpdate()
    {
        UpdateActiveInputListener();
    }

    void UpdateActiveInputListener()
    {
        if (_activeInputType == InputType.None)
        {
            DisableActiveInputListeners();
            ActiveInputListener = null;
        }

        foreach (InputType inputType in _inputListeners.Keys)
        {
            if (_inputListeners[inputType].SelectedInputType == _activeInputType && _inputListeners[inputType].IsActive) break;

            if (_inputListeners[inputType].SelectedInputType == _activeInputType && !_inputListeners[inputType].IsActive)
            {
                DisableActiveInputListeners();

                _inputListeners[inputType].Enable();
                ActiveInputListener = _inputListeners[inputType];
                break;
            }
        }
    }

    void DisableActiveInputListeners()
    {
        foreach (InputType inputType in _inputListeners.Keys)
        {
            if (_inputListeners[inputType].IsActive) 
            {
                _inputListeners[inputType].Disable();
            }
        }
    }
}

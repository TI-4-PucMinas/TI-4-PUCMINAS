using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class FrameLoopManager : MonoBehaviour
{
    [SerializeField] FrameLoopManagerData _data;

    public static FrameLoopManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject sceneObject = new();
                sceneObject.name = "Frame Loop Manager";
                _instance = sceneObject.AddComponent<FrameLoopManager>();
            }

            return _instance;
        }
    }
    public Action FrameDependantUpdate { get; set; }
    public long FrameCount { get; private set; }

    static FrameLoopManager _instance;

    float _frameLength;
    float _timeUntilNextFrame;
    KeyCode _bufferedInput;

    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this);
        else _instance = this;
    }

    void Start()
    {
        _frameLength = 1f / _data.FrameRate;
        _timeUntilNextFrame = 0f;
        _bufferedInput = KeyCode.None;
        FrameCount = 0L;
    }

    void FixedUpdate()
    {
        UpdateFrameLoop();
    }

    void UpdateFrameLoop()
    {
        _timeUntilNextFrame += Time.fixedDeltaTime;

        if (_timeUntilNextFrame >= _frameLength)
        {
            _timeUntilNextFrame = 0f;
            FrameCount++;

            ProcessBufferedInput();
            if (FrameDependantUpdate != null)
                FrameDependantUpdate.Invoke();
        }
    }

    void ProcessBufferedInput()
    {
        if (_bufferedInput != KeyCode.None)
        {
            Debug.Log($"Key {_bufferedInput} input detected!" + $" Frame: {FrameCount}");
            _bufferedInput = KeyCode.None;
        }
    }

    public void BufferInput(KeyCode key)
    {
        _bufferedInput = key;
    }
}

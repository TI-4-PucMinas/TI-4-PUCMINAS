using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputListener : InputListener
{
    public List<GameplayInput> HeardInputs { get; private set; }

    FrameLoopManager _frameLoopManager;

    public GameplayInputListener() : base(InputType.Gameplay) 
    {
        _frameLoopManager = FrameLoopManager.Instance;
        HeardInputs = new();
    }

    protected override void OnInputStarted(InputAction.CallbackContext context)
    {
        GameplayInput heardInput = new GameplayInput(context.action.name);

        heardInput.State = InputState.Started;
        heardInput.StartedFrameNumber = _frameLoopManager.FrameCount;

        HeardInputs.Add(heardInput);
    }

    protected override void OnInputPerformed(InputAction.CallbackContext context)
    {
        UpdateInputState(InputState.Performed, context.action);
    }

    protected override void OnInputCanceled(InputAction.CallbackContext context)
    {
        UpdateInputState(InputState.Canceled, context.action);
    }

    void UpdateInputState(InputState inputState, InputAction inputAction)
    {
        int index = 0;

        foreach (GameplayInput input in HeardInputs)
        {
            if (input.Name == inputAction.name)
            {
                HeardInputs[index].State = inputState;

                UpdateInputFrameData(inputState, index);

                break;
            }

            index++;
        }
    }

    void UpdateInputFrameData(InputState inputState, int inputIndex)
    {
        switch (inputState)
        {
            case InputState.Started:
                HeardInputs[inputIndex].StartedFrameNumber = _frameLoopManager.FrameCount;
                break;
            case InputState.Canceled:
                HeardInputs[inputIndex].CanceledFrameNumber = _frameLoopManager.FrameCount;
                break;
        }
    }
}

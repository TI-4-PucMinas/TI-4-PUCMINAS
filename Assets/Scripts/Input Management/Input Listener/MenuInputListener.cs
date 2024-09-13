using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputListener : InputListener
{
    public List<InputData> HeardInputs;

    public MenuInputListener() : base(InputType.Menu)
    {
        HeardInputs = new();
    }

    protected override void OnInputStarted(InputAction.CallbackContext context)
    {
        InputData heardInput = new(context.action.name);
        heardInput.State = InputState.Started;

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

        foreach (InputData input in HeardInputs)
        {
            if (input.Name == inputAction.name)
            {
                HeardInputs[index].State = inputState;

                break;
            }

            index++;
        }
    }
}

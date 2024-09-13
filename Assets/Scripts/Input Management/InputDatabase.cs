using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDatabase
{
    public List<InputAction> Inputs { get; private set; }

    public InputDatabase(InputSystem inputSystem, InputType inputType)
    {
        Inputs = new();

        if (inputType == InputType.None) return;

        foreach (InputAction input in inputSystem)
        {
            if (input.actionMap.name == inputType.ToString())
            {
                Inputs.Add(input);
            }
        }
    }
}


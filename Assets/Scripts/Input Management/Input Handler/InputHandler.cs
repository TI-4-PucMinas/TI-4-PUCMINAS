using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputHandler
{
    public bool IsActive { get; set; }
    public InputListener Input_Listener { get; set; }

    public InputHandler()
    {
        IsActive = false;
    }

    public abstract void HandleHeardInputs();
}

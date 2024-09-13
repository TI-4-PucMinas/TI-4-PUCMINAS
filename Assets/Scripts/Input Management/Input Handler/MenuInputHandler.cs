using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MenuInputNames;
public class MenuInputHandler : InputHandler
{
    public List<InputData> HeardInputs;

    public MenuInputHandler() : base()
    {
        HeardInputs = new();
    }

    public override void HandleHeardInputs()
    {
       
    }
}

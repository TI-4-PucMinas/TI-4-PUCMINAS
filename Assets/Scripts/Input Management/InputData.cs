using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData
{
    public InputData(string name)
    {
        Name = name;
        State = InputState.None;
    }

    public string Name { get; private set; }
    public InputState State { get; set; }
}



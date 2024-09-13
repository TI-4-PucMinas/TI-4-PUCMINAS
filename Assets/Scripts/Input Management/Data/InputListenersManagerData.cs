using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputListenersManagerData", menuName = "ScriptableObjects/InputData/InputListenersManagerData", order = 1)]
public class InputListenersManagerData : ScriptableObject
{
    public InputType DefaultInputListener = InputType.None;
}

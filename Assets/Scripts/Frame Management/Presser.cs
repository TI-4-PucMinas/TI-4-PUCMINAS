using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameplayInputNames;

public class Presser : MonoBehaviour
{
    [SerializeField] InputHandlersManager inputHandlers;

    GameplayInputHandler _gameplayInputHandler;

    void Update()
    {
        _gameplayInputHandler = (GameplayInputHandler)inputHandlers.InputHandlers[InputType.Gameplay];

        _gameplayInputHandler.CheckLink(Right, Right);
    }
}

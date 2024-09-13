using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Windows;
using static GameplayInputNames;

public class GameplayInputHandler : InputHandler, IFrameDependant
{
    public List<GameplayInput> HeardInputs { get; set; }
    public FrameLoopManager _frameLoopManager { get; set; }

    public GameplayInputHandler() : base()
    {
        _frameLoopManager = FrameLoopManager.Instance;
        HeardInputs = new();
    }

    public override void HandleHeardInputs()
    {
        int index;

        for (index = 0; index < HeardInputs.Count; index++)
        {
            if(HeardInputs[index].State == InputState.Canceled) 
            {
                HeardInputs.RemoveAt(index);
            }
            else if (HeardInputs[index].StartedFrameNumber < _frameLoopManager.FrameCount && HeardInputs[index].State == InputState.Started)
            {
                HeardInputs.RemoveAt(index);
            }
        }
    }

    public void CheckLink(params string[] keys)
    {
        // Check if there are enough heard inputs to potentially match the sequence
        if (HeardInputs.Count < keys.Length)
            return;

        // Iterate through the heard inputs to find a potential match
        for (int i = 0; i <= HeardInputs.Count - keys.Length; i++)
        {
            bool sequenceMatch = true;

            // Check if each key in the sequence matches the corresponding heard input and is not pressed at the same time
            for (int j = 0; j < keys.Length; j++)
            {
                GameplayInput heardInput = HeardInputs[i + j];

                if (heardInput.Name != keys[j] || IsInputSimultaneous(heardInput, 2))
                {
                    sequenceMatch = false;
                    break;
                }
            }

            // If the sequence matches, perform the action (replace this with your own logic)
            if (sequenceMatch)
            {
                PerformSpecialMove();
                break; // You may want to remove this break statement if you want to find multiple matches
            }
        }
    }

    bool IsInputSimultaneous(GameplayInput input, int maxSimultaneousCount)
    {
        // Count the occurrences of inputs at the same frame
        int simultaneousCount = 0;

        foreach (GameplayInput otherInput in HeardInputs)
        {
            if (otherInput != input &&
                otherInput.StartedFrameNumber == input.StartedFrameNumber &&
                otherInput.Name == input.Name) // Check if the keys are the same
            {
                simultaneousCount++;

                // Break early if the count exceeds the threshold
                if (simultaneousCount >= maxSimultaneousCount)
                    return true;
            }
        }

        return false;
    }



    void PerformSpecialMove()
    {
        // Implement your logic to perform the special move here
        Debug.Log("Special move performed!");
    }

    public void Enable()
    {
        _frameLoopManager.FrameDependantUpdate += HandleHeardInputs;
    }

    public void Disable()
    {
        _frameLoopManager.FrameDependantUpdate -= HandleHeardInputs;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "FrameLoopManagerData", menuName = "ScriptableObjects/FrameData/FrameLoopManagerData", order = 1)]
public class FrameLoopManagerData : ScriptableObject
{
    public int FrameRate = 60;
}

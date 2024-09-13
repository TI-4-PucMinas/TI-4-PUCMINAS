using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IFrameDependant
{
    public FrameLoopManager _frameLoopManager { get; set; }
    void Enable();
    void Disable();
}

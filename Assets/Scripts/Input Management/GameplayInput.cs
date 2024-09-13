using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayInput : InputData
{
    public GameplayInput(string name) : base(name)
    {
        StartedFrameNumber = 0L;
        CanceledFrameNumber = 0L;
    }

    public long StartedFrameNumber { get; set; }
    public long CanceledFrameNumber { get; set; }
}

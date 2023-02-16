using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISwitchable
{
    public bool IsActive { get; }
    public void Activate();
    public void Deactivate();
}

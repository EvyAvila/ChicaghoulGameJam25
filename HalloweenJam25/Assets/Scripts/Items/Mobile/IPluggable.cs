using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPluggable 
{
    bool ConnectToPoint(Transform t);
    void Disconnect();
}

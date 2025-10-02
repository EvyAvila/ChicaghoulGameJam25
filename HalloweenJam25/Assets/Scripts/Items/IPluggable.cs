using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPluggable 
{
    void ConnectToPoint(Transform t);
    void Disconnect();
}

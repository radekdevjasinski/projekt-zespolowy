using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivatableObject
{
    bool activate(GameObject activator);
    bool isActivated();
}

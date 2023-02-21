using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    Transform LeftP { get;}
    Transform RightP { get;}
    void InterractAnimation(float damage);
    void BarVisualisation(bool state);
    bool CheckIfMined(Transform playerPos);
}

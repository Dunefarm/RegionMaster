using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAbilityCounter : MonoBehaviour
{
    public int Value
    {
        get { return Count(); }
    }

    protected int _value = 0;

    public virtual int Count()
    {
        return _value;
    }
}

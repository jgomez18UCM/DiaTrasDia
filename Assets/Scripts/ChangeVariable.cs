﻿using System.Collections;
using System.Collections.Generic;
using uAdventure.Core;
using uAdventure.Runner;
using UnityEngine;

public class ChangeVariable : MonoBehaviour
{
    int value = 0;

    public void Cambia(string variable)
    {
        Game.Instance.Execute(new EffectHolder(new Effects{
                new SetValueEffect(variable, 0)
        }));
    }

    public void SetValue(int v) 
    {
        value = v;
    }
}

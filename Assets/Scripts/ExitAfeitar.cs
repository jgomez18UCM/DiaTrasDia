using System.Collections;
using System.Collections.Generic;
using uAdventure.Core;
using uAdventure.Runner;
using UnityEngine;

public class ExitAfeitar : MonoBehaviour
{
    public void Salir()
    {
        Game.Instance.Execute(new EffectHolder(new Effects{
                new TriggerSceneEffect("Casa",0,0)
        }));
    }
}

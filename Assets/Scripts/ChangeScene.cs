using System.Collections;
using System.Collections.Generic;
using uAdventure.Core;
using uAdventure.Runner;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeEscena(string scene)
    {
        Game.Instance.Execute(new EffectHolder(new Effects{
                new TriggerSceneEffect(scene,0,0)
        }));
    }
}

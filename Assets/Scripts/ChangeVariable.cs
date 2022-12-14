using System.Collections;
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
                new SetValueEffect(variable, value)
        }));
    }

    public void Add(string variable) 
    {
        if(value > 0) 
        {
            Game.Instance.Execute(new EffectHolder(new Effects{
                new IncrementVarEffect(variable, value)
            }));
        }

        else if(value < 0) 
        {
            value = Mathf.Abs(value);
            Game.Instance.Execute(new EffectHolder(new Effects{
                new DecrementVarEffect(variable, value)
            }));
        }

    }

    public void SetValue(int v) 
    {
        value = v;
    }

    public void ActivaFlag(string name)
    {
        Game.Instance.Execute(new EffectHolder(new Effects{
                new ActivateEffect(name)
        }));
    }

    public void DesactivaFlag(string name)
    {
        Game.Instance.Execute(new EffectHolder(new Effects{
                new DeactivateEffect(name)
        }));
    }
}

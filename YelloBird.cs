using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YelloBird : bird 
{
    protected override void ShowSkill()
    {
        base.ShowSkill();
        rg.velocity *= 2;
    }

}

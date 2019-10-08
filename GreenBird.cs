﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : bird
{
    protected override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rg.velocity;
        speed.x *= -1;
        rg.velocity =speed;
    }
}

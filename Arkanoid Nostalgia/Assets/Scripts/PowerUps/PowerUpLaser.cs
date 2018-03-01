﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLaser : ControlPowerUps {

    public static bool laser = true;
    public int scoreToGive;

    public override void PowerUpBehavior(Collider2D collision)
    {
        if (collision.transform.tag == paddleTag)
        {
            SmokePuffs();
            score.HitBrickScore(scoreToGive);
            laser = true;
            
        }


    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpBehavior(collision);
    }




}

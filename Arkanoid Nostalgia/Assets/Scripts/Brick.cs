﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public AudioClip[] crack;
    public Sprite[] hitSprites;

    //Totalbricks that we will access in levelmanager
    public static int totalBricks=0;

    private LevelManager levelmanager;

    private Score score;

    //How many times have been hit
    private int timesHit;

    bool isBreakable;


    // Use this for initialization
    void Start()
    {
        
        score = GameObject.Find("Score").GetComponent<Score>();
        //Set our bricks that have tag Breakable to isBreakable
        isBreakable = (tag == "Breakable");
        
        //Count all bricks that are breakables
        if (isBreakable)
        {
            totalBricks++;
        }
       
        levelmanager = GameObject.FindObjectOfType<LevelManager>();
        timesHit = 0;

        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        //Handle hits only if you can break it
        if (isBreakable)
        {
            HandleHits();
        }
    }

    void HandleHits()
    {
        

        timesHit++;
        AudioSource.PlayClipAtPoint(crack[0], transform.position);
        //How many times it can be hit
        int maxHits = hitSprites.Length + 1;
        
        //Destroy brick on maxhits and remove the total counter for bricks
        if (timesHit >= maxHits)
        {
            totalBricks--;
            levelmanager.AllBricksDestroyed();
            AudioSource.PlayClipAtPoint(crack[1], transform.position);
            Destroy(gameObject);
            score.HitBrickScore(100);
            
        }
        else
        {
            LoadSprites();
            score.HitBrickScore(20);
        }
       
    }


    private void LoadSprites()
    {
        //Find Array indexer according to times hit so if hit one time the array ndexer will be 0
        int spriteIndex = timesHit - 1;

        //Find component sprite renderer and change it according to index
        if (hitSprites[spriteIndex] != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
                Debug.LogError("Missing sprite");
        }

    }

    //Reset bricks for next lext etc
    public static void ResetBricks()
    {
        totalBricks = 0;
    }

    //TODO Remove this method when we can actually win
    private void SimulateWin()
    {
        levelmanager.LoadNextLevel();
    }

}

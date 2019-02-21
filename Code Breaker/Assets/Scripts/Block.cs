using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites; 
    
    // cached reference
    Level level;
    GameStatus gameStatus;

    // state variables
    [SerializeField] int timesHit = 0; //only serialized for debug purposes

    private void Start()
    {
        CountBlocks();

        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void CountBlocks()
    {
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.addOneBreakableBlock();
        }
        else if (tag == "Unbreakable")
        {
            level.addOneUnbreakableBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;

        int maxHits = hitSprites.Length;

        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit];
    }

    private void DestroyBlock()
    {
        TriggerSparklesVFX();// produz efeito de particula para destruicao do bloco
        Destroy(gameObject);//destroi o objeto
        playBlockDestroySFX();//produz som de colisao
        gameStatus.AddToScore(); // aumenta a pontuacao do jogador
        level.removeOneBreakableBlock(); // diminui em 1 o numero total de blocos da fase
    }

    private void playBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB.Core
{
    public class Block : MonoBehaviour
    {
        //config params
        [SerializeField] AudioClip breakSound;
        [SerializeField] GameObject blockSparklesVFX;
        [SerializeField] Sprite[] hitSprites;

        // state variables
        int timesHit = 0;

        // cached reference
        Level level;
        GameStatus gameStatus;

        private void Start()
        {
            gameStatus = FindObjectOfType<GameStatus>();

            if (gameStatus.IsMenu()) return;
            CountBlocks();
        }

        private void CountBlocks()
        {
            level = FindObjectOfType<Level>();

            if (tag == "Breakable")
                level.addOneBreakableBlock();
            else if (tag == "Unbreakable")
                level.addOneUnbreakableBlock();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (tag != "Breakable") return;
            HandleHit();
        }

        private void HandleHit()
        {
            timesHit++;
            int maxHits = hitSprites.Length;

            if (timesHit >= maxHits)
                DestroyBlock();
            else
                ShowNextHitSprite();
        }

        private void ShowNextHitSprite()
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit];
        }

        private void DestroyBlock()
        {
            if (gameStatus.IsMenu())
            {
                TriggerSparklesVFX();
                Destroy(gameObject);
            }
            else
            {
                TriggerSparklesVFX();
                Destroy(gameObject);
                playBlockDestroySFX();
                gameStatus.AddToScore(); 
                level.removeOneBreakableBlock(); 
            }
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
}
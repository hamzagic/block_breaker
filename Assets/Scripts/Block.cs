using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;

    [SerializeField]
    private GameObject blockSparklesVFX;

    [SerializeField] Sprite[] hitSprites;

    [SerializeField]
    private int timesHit; // only for debug purposes

    Level level;

    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        BreakableBlocksCounter();
    }

    private void BreakableBlocksCounter()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (tag == "Breakable")
        {
            HandleHits();
        }
    }

    private void HandleHits()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
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
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
            gameStatus.AddToScore();
        }
        else
        {
            Debug.LogError("Block sprite is missing from array");
        }

    }
    private void DestroyBlock()
    {
        TriggerSparklesVFX();
        PlayBlockDestroySFX();
        Destroy(this.gameObject);
        level.BlockDestroyed();
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
        gameStatus.AddToScore();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}

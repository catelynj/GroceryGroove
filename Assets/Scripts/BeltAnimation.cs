using UnityEngine;

/**
 * beltAnimation.cs 
 * Author: Catelyn Jones
 * 
 * Purpose:
 * Iterate through belt sprite sheet to create animation effect
 * 
 * */

public class BeltAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite[] beltSprites;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = beltSprites[Time.frameCount / 50 % beltSprites.Length];
    }
}

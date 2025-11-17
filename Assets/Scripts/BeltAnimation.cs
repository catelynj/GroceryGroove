using UnityEngine;

public class BeltAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _beltSprites;
    private SpriteRenderer _spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = _beltSprites[Time.frameCount / 50 % _beltSprites.Length];
    }
}

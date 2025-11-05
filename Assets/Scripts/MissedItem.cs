using UnityEngine;

/**
 * MissedItem.cs 
 * Author: Catelyn Jones
 * 
 * Purpose:
 * Handles missed items in Level 1 scene
 * Calls BadScan from GM.cs and destroys missed items
 * 
 * */

public class MissedItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.BadScan();
        Destroy(collision.gameObject);
    }
}

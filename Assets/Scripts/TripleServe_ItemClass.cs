using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;


public class TripleServe_ItemClass : MonoBehaviour
{
    [SerializeField] private Vector3 itemPosition;
    [SerializeField] private MinigameTripleServe mg;

    private Sprite itemSprite;
    private string itemName;

    private bool isTaken;

    /*public TripleServe_ItemClass(string name, Sprite sprite, Vector3 pos)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        itemName = name;
        itemPosition = pos;
    }*/


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = itemPosition;
        int itemIndex = Random.Range(0, 3);
        itemSprite = mg._foodItemsSprites[itemIndex];
        itemName = mg._foodItemsNames[itemIndex];
        GetComponent<SpriteRenderer>().sprite = itemSprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;


public class TripleServe_ItemClass : MonoBehaviour
{
    [SerializeField] private Vector3 itemPosition;
    [SerializeField] private MinigameTripleServe mg;

    [SerializeField] private int ARRAY_INDEX_CONSTANT;

    private Sprite coffeeSprite, cakeSprite, breadSprite;
    private string itemName;
    private bool isTaken;

    private Sprite itemSprite;


    // Start is called before the first frame update
    void Start()
    {

        isTaken = false;
        gameObject.transform.position = itemPosition;

        //instead of a random item at all 3 spots, have one of each item at each spot getting positions from an array instead

        itemName = mg.GetVariationArray()[ARRAY_INDEX_CONSTANT];

        

        if (itemName == "coffee")
        {
            itemSprite = coffeeSprite;
        }
        if (itemName == "cake")
        {
            itemSprite = cakeSprite;
        }
        if(itemName == "bread")
        {
            itemSprite = breadSprite;
        }

        GetComponent<SpriteRenderer>().sprite = itemSprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetItemName()
    {
        return itemName;
    }

    public Vector3 GetItemPosition()
    {
        return itemPosition;
    }

}

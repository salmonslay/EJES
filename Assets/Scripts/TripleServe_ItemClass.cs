using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;


public class TripleServe_ItemClass : MonoBehaviour
{
    [SerializeField] private Vector3 itemPosition;
    [SerializeField] private MinigameTripleServe mg;

    [SerializeField] private int ARRAY_INDEX_CONSTANT;

    [SerializeField] private Sprite coffeeSprite, cakeSprite, breadSprite;
    private string itemName;

    private Sprite itemSprite;


    // Start is called before the first frame update
    void Start()
    {

        mg = gameObject.GetComponentInParent<MinigameTripleServe>();

        gameObject.transform.localPosition = itemPosition;

        //instead of a random item at all 3 spots, have one of each item at each spot getting positions from an array instead

        string[] itemArray = mg.GetVariationArray();

        //itemName = itemArray[ARRAY_INDEX_CONSTANT];

        

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

        GetComponent<Image>().sprite = itemSprite;

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

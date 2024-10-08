using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleServeCustomerClass : MonoBehaviour
{

    [SerializeField] private Vector3 customerPosition;
    [SerializeField] private MinigameTripleServe mg;
    [SerializeField] private Sprite servedCustomer;

    private Sprite customerSprite;
    private Sprite orderSprite;
    private string orderName;

    private bool isServed;
    private int loopCheck;



    // Start is called before the first frame update
    void Start()
    {
        isServed = false;
        int itemIndex = Random.Range(0, 3);
        orderSprite = mg._foodItemsSprites[itemIndex];
        orderName = mg._foodItemsNames[itemIndex];
        GetComponent<SpriteRenderer>().sprite = orderSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (isServed && loopCheck < 1)
        {
            GetComponent<SpriteRenderer>().sprite = servedCustomer;
        }
    }

    public string GetItemName()
    {
        return orderName;
    }

    public Vector3 GetItemPosition()
    {
        return customerPosition;
    }
}

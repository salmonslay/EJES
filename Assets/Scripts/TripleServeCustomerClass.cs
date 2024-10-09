using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleServeCustomerClass : MonoBehaviour
{

    [SerializeField] private Vector3 customerPosition;
    [SerializeField] private MinigameTripleServe mg;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] statusSprite;
    [SerializeField] private GameObject statusSpriteRenderer;

    private Sprite customerSprite;
    private Sprite orderSprite;
    private string orderName;

    private bool isServed;
    private int loopCheck;



    // Start is called before the first frame update
    void Start()
    {
        isServed = false;
        int itemIndex = Random.Range(0, 2);
        orderSprite = mg._foodItemsSprites[itemIndex];
        orderName = mg._foodItemsNames[itemIndex];
        spriteRenderer.sprite = customerSprite;
        statusSpriteRenderer.gameObject.GetComponent<SpriteRenderer>().sprite = orderSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (isServed && loopCheck < 1)
        {
            statusSpriteRenderer.gameObject.GetComponent<SpriteRenderer>().sprite = mg._foodItemsSprites[3];
            loopCheck++;
        }
    }

    public void SetServedStatus(bool servedStatus)
    {
        isServed = servedStatus;
    }

    public string GetOrderName()
    {
        return orderName;
    }

    public Vector3 GetCustomerPosition()
    {
        return customerPosition;
    }

    public bool IsServed() { return isServed; }


}

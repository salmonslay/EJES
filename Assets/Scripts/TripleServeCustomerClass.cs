using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripleServeCustomerClass : MonoBehaviour
{

    [SerializeField] private Vector3 customerPosition;
    [SerializeField] private MinigameTripleServe mg;
    //[SerializeField] private Sprite spriteRenderer;
    //[SerializeField] private Sprite[] statusSprite;
    [SerializeField] private GameObject statusImageObject;

    private Sprite customerSprite;
    private Sprite orderSprite;
    [SerializeField] private Sprite checkMark;
    private string orderName;

    private bool isServed;
    private int loopCheck;



    private void Awake()
    {
        //gameObject.transform.localPosition = customerPosition;
        
    }

    // Start is called before the first frame update
    void Start()
    {

        mg = gameObject.GetComponentInParent<MinigameTripleServe>();

        

        isServed = false;
        int itemIndex = Random.Range(0, 3);
        orderSprite = mg._foodItemsSprites[itemIndex];
        orderName = mg._foodItemsNames[itemIndex];
        //GetComponent<Image>().sprite = customerSprite;
        statusImageObject.gameObject.GetComponent<Image>().sprite = orderSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (isServed && loopCheck < 1)
        {
            statusImageObject.gameObject.GetComponent<Image>().sprite = checkMark;
            statusImageObject.GetComponentInChildren<ParticleSystem>().Play();
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

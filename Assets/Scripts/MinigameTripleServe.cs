using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEditor.UI;
using UnityEngine.UI;

public class MinigameTripleServe : Minigame
{
    protected override float MinigameTime => GameManager.Instance ? GameManager.Instance.MinigameTime : 5f;
    protected override string MinigameName => "Serve your guests";

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerItem;

    [SerializeField] public Sprite[] _foodItemsSprites;
    [SerializeField] public string[] _foodItemsNames; //ska vara i samma ordning some "_foodItemsSprites" och

    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _placeDownSound;

    [SerializeField] private Vector3[] areaOfPlayPos;

    [SerializeField] private List<GameObject> _foodItems;
    [SerializeField] private List<GameObject> _customers;
    private List<TripleServe_ItemClass> foodItems = new List<TripleServe_ItemClass>();
    private List<TripleServeCustomerClass> customers = new List<TripleServeCustomerClass>();

    [SerializeField] private int stepDistanceMultiplyer = 1;

    private Vector3 origPos, targetPos;

    private AudioSource audioSource;

    private bool isMoving;
    private bool isHoldingItem;
    private bool isAtItem;
    private bool isAtCustomer;
    private float timeToMove = 0.2f;

    private int serveCounter;

    private string currentItemHeld;

    private List<string[]> listOfFoodVariations = new List<string[]>();
    private string[] chosenVariation = new string[3];

    private string[] variationOne = new string[3] { "coffee", "bread", "cake" };
    private string[] variationTwo = new string[3] { "bread", "cake", "coffee" };
    private string[] variationThree = new string[3] { "cake", "coffee", "bread" };


    private void Awake()
    {

        listOfFoodVariations.Add(variationOne);
        listOfFoodVariations.Add(variationTwo);
        listOfFoodVariations.Add(variationThree);

        int itemIndex = Random.Range(0, 3);

        chosenVariation = listOfFoodVariations[itemIndex];

        serveCounter = 0;

        currentItemHeld = "none";

        playerItem.SetActive(false);

        audioSource = player.GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < _foodItems.Count; i++)
        {
            foodItems.Add(_foodItems[i].GetComponent<TripleServe_ItemClass>());
        }
        for (int i = 0; i < _customers.Count; i++)
        {
            customers.Add(_customers[i].GetComponent<TripleServeCustomerClass>());
        }

        //currentItemHeld = "none";

    }


    // Update is called once per frame
    void Update()
    {

        //Debug.Log(isHoldingItem);
        //Debug.Log(isAtItem);


        if (!isMoving) //gets inputs for movement
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove((Vector3.left * stepDistanceMultiplyer) + origPos) && player.transform.localPosition.y == 0)
                StartCoroutine(MovePlayer(Vector3.left * stepDistanceMultiplyer));

            if (Input.GetKeyDown(KeyCode.UpArrow) && CanMove((Vector3.up * stepDistanceMultiplyer) + origPos))
                StartCoroutine(MovePlayer(Vector3.up * stepDistanceMultiplyer));

            if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove((Vector3.right * stepDistanceMultiplyer) + origPos) && player.transform.localPosition.y == 0)
                StartCoroutine(MovePlayer(Vector3.right * stepDistanceMultiplyer));

            if (Input.GetKeyDown(KeyCode.DownArrow) && CanMove((Vector3.down * stepDistanceMultiplyer) + origPos))
                StartCoroutine(MovePlayer(Vector3.down * stepDistanceMultiplyer));

            
            IsAtItem();
            if (isAtItem)
            {
                PickUpItem(player.transform.localPosition);
            }
            
            IsAtCustomer();

            if (isHoldingItem)
            {
                if (isAtCustomer)
                {
                    GiveItem(player.transform.localPosition);
                }
            }
            
            
        }
    }


    private IEnumerator MovePlayer(Vector3 direction)
    {

        isMoving = true;

        float elapsedTime = 0;

        origPos = player.transform.localPosition;
        targetPos = origPos + direction;


        if (targetPos.x > 300 || targetPos.x < -300 || targetPos.y > 300 || targetPos.y < -300)
        {
            isMoving = false;
        }
        else
        {
            while (elapsedTime < timeToMove)
            {
                player.transform.localPosition = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            player.transform.localPosition = targetPos;
            Debug.Log("player took a step to " + player.transform.localPosition);
            isMoving = false;
        }

    }


    private bool CanMove(Vector3 pos)
    {
        //Debug.Log(pos);
        if (pos.x > 300 || pos.x < -300 || pos.y > 300 || pos.y < -300)
        {
            //Debug.Log("cant move to" + pos);
            return false;
        }

        /*if(isAtItem || isAtCustomer)
        {
            if(pos.x != player.transform.localPosition.x)
            {
                return false;
            }
        }*/
        
        /*for (int i = 0; i < areaOfPlayPos.Length; i++)
        {
            //Debug.Log(areaOfPlayPos[i]);
            //Debug.Log(pos);

            if (areaOfPlayPos[i] == pos) return true;


        }*/
        //Debug.Log("moving to" + pos);
        /*
        for (int i = 0; i < foodItemPos.Length; i++)
        {
            if (foodItemPos[i] == pos) return false;
        }

        for (int i = 0; i < customerPos.Length; i++)
        {
            if (customerPos[i] == pos) return false;
        }
        */

        return true;
    }

    private void PickUpItem(Vector3 itemPos)
    {
        isHoldingItem = true;

        for (int i = 0; i < foodItems.Count; i++)
        {
            if (itemPos == foodItems[i].GetItemPosition() && currentItemHeld != foodItems[i].GetItemName())
            {
                currentItemHeld = foodItems[i].GetItemName();

                playerItem.SetActive(true);

                if (currentItemHeld == "coffee")
                {
                    playerItem.GetComponent<Image>().sprite = _foodItemsSprites[2];
                }
                if (currentItemHeld == "cake")
                {
                    playerItem.GetComponent<Image>().sprite = _foodItemsSprites[1];
                }
                if (currentItemHeld == "bread")
                {
                    playerItem.GetComponent<Image>().sprite = _foodItemsSprites[0]; 
                }

                audioSource.clip = _pickUpSound;
                audioSource.Play();

                return;
            }
        }
    }

    private void IsAtItem()
    {
        
        for (int i = 0; i < foodItems.Count; i++)
        {
            Debug.Log(foodItems[i].GetItemPosition());
            if (player.transform.localPosition == foodItems[i].GetItemPosition())
            {
                
                isAtItem = true;
                return;
            }
        }
        isAtItem = false;
    }

    //maybe make the "IsAtCustomer" method a bool variable and handle return the customer order info instead
    //same can be done for the items

    private void GiveItem(Vector3 customerPos)
    {
        isHoldingItem = false;

        string currentCustomerOrder = string.Empty;

        for (int i = 0; i < customers.Count; i++)
        {
            if (customerPos == customers[i].GetCustomerPosition() && !customers[i].IsServed() && customers[i].GetOrderName() == currentItemHeld)
            {
                customers[i].SetServedStatus(true);
                currentItemHeld = "none";
                playerItem.SetActive(false);
                serveCounter++;
                audioSource.clip = _placeDownSound;
                audioSource.Play();

                if(serveCounter >= 3)
                {
                    EndMinigame(true);
                }

                return;
            }
        }
    }


    private void IsAtCustomer() //need to edit later
    {
        for (int i = 0; i < customers.Count; i++)
        {
            if (player.transform.localPosition == customers[i].GetCustomerPosition())
            {
                isAtCustomer = true;
                return;
            }
        }
        isAtCustomer = false;
    }

    public string[] GetVariationArray()
    {
        //Debug.Log(chosenVariation.Length);
        return chosenVariation;
    }


}

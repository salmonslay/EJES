using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTripleServe : Minigame
{
    protected override float MinigameTime => GameManager.Instance ? GameManager.Instance.MinigameTime * 0.7f : 5f;
    protected override string MinigameName => "Eat kanelbulle";

    [SerializeField] private GameObject player;

    [SerializeField] public Sprite[] _foodItemsSprites;
    [SerializeField] public string[] _foodItemsNames; //ska vara i samma ordning some "_foodItemsSprites" och

    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _placeDownSound;
    
    [SerializeField] private Vector3[] areaOfPlayPos;
    //[SerializeField] private Vector3[] customerPos;
    [SerializeField] private TripleServe_ItemClass[] foodItems;
    [SerializeField] private TripleServeCustomerClass[] customers;

    [SerializeField] private int stepDistanceMultiplyer = 1;

    private Vector3 origPos, targetPos;

    private bool isMoving;
    private bool isHoldingItem;
    private float timeToMove = 0.2f;

    private string currentItemHeld;

    private List<string[]> listOfFoodVariations = new List<string[]>();
    private string[] chosenVariation;

    // Start is called before the first frame update
    void Start()
    {

        string[] variationOne = new string[3] { "coffee", "bread", "cake" };
        string[] variationTwo = new string[3] { "bread", "cake", "coffee" };
        string[] variationThree = new string[3] { "cake", "coffee", "bread" };

        listOfFoodVariations.Add(variationOne);
        listOfFoodVariations.Add(variationTwo);
        listOfFoodVariations.Add(variationThree);

        int itemIndex = Random.Range(0, 2);

        chosenVariation = listOfFoodVariations[itemIndex];

        currentItemHeld = "none";

    }


    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove((Vector3.left * stepDistanceMultiplyer) + origPos))
                StartCoroutine(MovePlayer(Vector3.left * stepDistanceMultiplyer));

            if (Input.GetKeyDown(KeyCode.UpArrow) && CanMove((Vector3.up * stepDistanceMultiplyer) + origPos))
                StartCoroutine(MovePlayer(Vector3.up * stepDistanceMultiplyer));

            if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove((Vector3.right * stepDistanceMultiplyer) + origPos))
                StartCoroutine(MovePlayer(Vector3.right * stepDistanceMultiplyer));

            if (Input.GetKeyDown(KeyCode.DownArrow) && CanMove((Vector3.down * stepDistanceMultiplyer) + origPos))
                StartCoroutine(MovePlayer(Vector3.down * stepDistanceMultiplyer));
        }

        if (!isHoldingItem) 
        {
            if (IsAtItem())
            {
                PickUpItem(player.transform.position);
            }
        }

        if (isHoldingItem)
        {
            if (IsAtCustomer())
            {
                GiveItem(player.transform.position);
            }
        }
    }


    private IEnumerator MovePlayer(Vector3 direction)
    {

            isMoving = true;

            float elapsedTime = 0;

            origPos = player.transform.position;
            targetPos = origPos + direction;

            while (elapsedTime < timeToMove)
            {
                player.transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            player.transform.position = targetPos;

            isMoving = false;
    }


    private bool CanMove(Vector3 pos)
    {
        for (int i = 0; i < areaOfPlayPos.Length; i++) 
        {
            if(areaOfPlayPos[i] != pos) return false;
        }

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
        
        for (int i = 0; i < foodItems.Length; i++)
        {
            if (itemPos == foodItems[i].GetItemPosition())
            {
                currentItemHeld = foodItems[i].GetItemName();
                break;
            }
        }


    }

    private bool IsAtItem()
    {
        for(int i = 0; i < foodItems.Length; i++)
        {
            if(player.transform.position == foodItems[i].GetItemPosition())
            {
                return true;
            }
        }
        return false;
    }

    //maybe make the "IsAtCustomer" method a bool variable and handle return the customer order info instead
    //same can be done for the items

    private bool IsAtCustomer() //need to edit later
    {
        for (int i = 0; i < customers.Length; i++)
        {
            if (player.transform.position == customers[i].GetItemPosition())
            {
                return true;
            }
        }
        return false;

    }

    private void GiveItem(Vector3 customerPos)
    {
        for (int i = 0; i < foodItems.Length; i++)
        {
            if (customerPos == foodItems[i].GetItemPosition())
            {
                currentItemHeld = foodItems[i].GetItemName();
                break;
            }
        }
    }

    public string[] GetVariationArray()
    {
        return chosenVariation;
    }


}

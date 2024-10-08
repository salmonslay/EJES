using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTripleServe : Minigame
{
    protected override float MinigameTime => GameManager.Instance ? GameManager.Instance.MinigameTime * 0.7f : 5f;
    protected override string MinigameName => "Eat kanelbulle";

    [SerializeField] private GameObject player;

    [SerializeField] public Sprite[] _foodItemsSprites;
    [SerializeField] public string[] _foodItemsNames; //ska vara i samma ordning some "_foodItems"

    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _placeDownSound;
    
    [SerializeField] private Vector3[] areaOfPlayPos;
    [SerializeField] private Vector3[] customerPos;
    [SerializeField] private Vector3[] foodItemPos;

    private Vector3 origPos, targetPos;

    private bool isMoving;
    private bool isHoldingItem;
    private float timeToMove = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < foodItemPos.Length; i++)
        {
            int itemIndex = Random.Range(0, 3);
            
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove(Vector3.left + origPos))
                StartCoroutine(MovePlayer(Vector3.left));

            if (Input.GetKeyDown(KeyCode.UpArrow) && CanMove(Vector3.up + origPos))
                StartCoroutine(MovePlayer(Vector3.up));

            if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove(Vector3.right + origPos))
                StartCoroutine(MovePlayer(Vector3.right));

            if (Input.GetKeyDown(KeyCode.DownArrow) && CanMove(Vector3.down + origPos))
                StartCoroutine(MovePlayer(Vector3.down));
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
        if (!isHoldingItem)
        {
            isHoldingItem = true;
            
        }
    }





}

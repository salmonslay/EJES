using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTripleServe : Minigame
{
    protected override float MinigameTime => GameManager.Instance ? GameManager.Instance.MinigameTime * 0.7f : 5f;
    protected override string MinigameName => "Eat kanelbulle";

    [SerializeField] private Sprite[] _foodItems;
    [SerializeField] private string[] _foodItemsNames; //ska vara i samma ordning some "_foodItems"

    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _placeDownSound;
    

    private bool isMoving;
    private bool isHoldingItem;
    private float timeToMove = 0.2f;

    private Vector3 origPos, targetPos;
    private Vector3[] outOfBoundsPos;
    private Vector3[] customerPos;
    private Vector3[] foodItemPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove(Vector3.left))
                StartCoroutine(MovePlayer(Vector3.left));

            if (Input.GetKeyDown(KeyCode.UpArrow) && CanMove(Vector3.up))
                StartCoroutine(MovePlayer(Vector3.up));

            if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove(Vector3.right))
                StartCoroutine(MovePlayer(Vector3.right));

            if (Input.GetKeyDown(KeyCode.DownArrow) && CanMove(Vector3.down))
                StartCoroutine(MovePlayer(Vector3.down));
        }
    }


    private IEnumerator MovePlayer(Vector3 direction)
    {

            isMoving = true;

            float elapsedTime = 0;

            origPos = transform.position;
            targetPos = origPos + direction;

            while (elapsedTime < timeToMove)
            {
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;

            isMoving = false;
    }


    private bool CanMove(Vector3 pos)
    {
        for (int i = 0; i < outOfBoundsPos.Length; i++) 
        {
            if(outOfBoundsPos[i] == pos) return false;
        }

        for (int i = 0; i < foodItemPos.Length; i++)
        {
            if (foodItemPos[i] == pos) return false;
        }

        for (int i = 0; i < customerPos.Length; i++)
        {
            if (customerPos[i] == pos) return false;
        }

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

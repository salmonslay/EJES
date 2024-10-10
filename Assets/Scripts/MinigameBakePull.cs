using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MinigameBakePull : Minigame 
{ 
    protected override float MinigameTime => GameManager.Instance ? GameManager.Instance.MinigameTime * 0.7f : 5f;
    protected override string MinigameName => "Eat kanelbulle";

    private static int loopCounter;

    private float rangeLower, rangeUpper;

    [SerializeField] private float dificultyMultiplyer = 0.9f;

    [SerializeField] private GameObject slider;

    [SerializeField] private GameObject ovenNeedle;
    [SerializeField] private GameObject heatBar;
    
    [SerializeField] private GameObject marker;

    [SerializeField] private float needleSpeed;

    private float sliderLength;

    private Vector3 currentPos;

    private float sliderPos;


    private void Awake()
    {
        //slider.GetComponent<Slider>().maxValue = sliderLength;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(marker.transform.localPosition);
        if (loopCounter > 0)
        {
            dificultyMultiplyer -= loopCounter*0.05f;
        }
        rangeLower = Random.Range(125, 325);
        rangeUpper = rangeLower + (50 * dificultyMultiplyer);
        loopCounter++;

        Debug.Log("World: " + ovenNeedle.transform.position);
        Debug.Log("Local: " + ovenNeedle.transform.localPosition);

        RectTransform markerRect = marker.GetComponent<RectTransform>();

        markerRect.transform.localPosition = new Vector3(rangeLower -200, 0, 0);
        
        markerRect.sizeDelta = new Vector2(50 * dificultyMultiplyer, 22); //22 is the constant height of the marker

        Debug.Log(marker.transform.localPosition);

        sliderPos = slider.GetComponent<UnityEngine.UI.Slider>().value;

        //currentPos = ovenNeedle.GetComponent<RectTransform>().localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        MoveNeedle();
        //Debug.Log("lower coords:" + rangeLower);
        if (IsRunning)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CheckPullTiming();
            }
        }
        
    }

    private void CheckPullTiming() //determines if you pulled it on time 
    {
        if (IsWithinRange(ovenNeedle.transform.localPosition.x))
        {
            EndMinigame(true);
        }
        else
        {
            EndMinigame(false);
        }
    }

    private bool IsWithinRange(float value) //determines if the range is correct
    {
        if(value > rangeLower && value < rangeUpper)
        {
            return true;
        }
        return false;
    }


    private void MoveNeedle()
    {

        sliderPos = Mathf.Lerp(sliderPos, 400, Time.deltaTime);

        Debug.Log(sliderPos);
        

    }


}

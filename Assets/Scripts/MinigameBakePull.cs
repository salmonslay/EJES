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
        base.Start();

        Debug.Log(marker.transform.localPosition);
        if (loopCounter > 0)
        {
            dificultyMultiplyer -= loopCounter*0.05f;
        }
        rangeLower = Random.Range(125, 325);
        rangeUpper = rangeLower + (20 * dificultyMultiplyer);
        loopCounter++;

        //Debug.Log("World: " + ovenNeedle.transform.position);
        //Debug.Log("Local: " + ovenNeedle.transform.localPosition);

        RectTransform markerRect = marker.GetComponent<RectTransform>();

        markerRect.transform.localPosition = new Vector3(rangeLower -200, 0, 0);
        
        markerRect.sizeDelta = new Vector2(20 * dificultyMultiplyer, 22); //22 is the constant height of the marker

        Debug.Log(marker.transform.localPosition);

        sliderPos = slider.GetComponent<UnityEngine.UI.Slider>().value;

        //currentPos = ovenNeedle.GetComponent<RectTransform>().localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log("lower coords:" + rangeLower);
        if (IsRunning)
        {
            MoveNeedle();
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CheckPullTiming();
            }

            if(sliderPos >= rangeUpper)
            {
                EndMinigame(false);
            }
        }
        
    }

    private void CheckPullTiming() //determines if you pulled it on time 
    {
        Debug.Log("pulled at: " + sliderPos);
        Debug.Log(IsWithinRange(sliderPos));
        Debug.Log("Lower: " + rangeLower + " Upper: " + rangeUpper);
        if (IsWithinRange(sliderPos))
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
        Debug.Log(value);
        if(value > rangeLower && value < rangeUpper)
        {
            return true;
        }
        return false;
    }


    private void MoveNeedle()
    {

        sliderPos += Time.deltaTime * (2 - dificultyMultiplyer) * 90;

        slider.GetComponent<UnityEngine.UI.Slider>().value = sliderPos;

        //Debug.Log(sliderPos);
        

    }


}

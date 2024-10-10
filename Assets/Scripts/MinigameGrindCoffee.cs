using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MinigameGrindCoffee : Minigame
{
    // Start is called before the first frame update
    protected override string MinigameName { get; } = "Grind coffee";
    public Image coffeeBeans;
    public Image coffeePowder;
    private float _powderGoalSize = 0;

    private int _index;
    private KeyCode[] _orderBase = {KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow};
    private KeyCode[] _order;
    [SerializeField] private int _clicksToComplete = 40;
    private int _clicks;

    private void Start()
    {
        base.Start();

        _order = new KeyCode[_clicksToComplete];

        // add copies until we have enough
        for (var i = 0; i < _order.Length; i++)
            _order[i] = _orderBase[i % _orderBase.Length];

        coffeePowder.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        coffeePowder.transform.localScale = Vector3.Lerp(coffeePowder.transform.localScale, new Vector3(_powderGoalSize, _powderGoalSize), Time.deltaTime);
        coffeeBeans.transform.localScale = Vector3.Lerp(coffeeBeans.transform.localScale, new Vector3(1 - _powderGoalSize, 1 - _powderGoalSize), Time.deltaTime);
        if (IsRunning)
        {
            KeyCode key = _order.FirstOrDefault((KeyCode x) => Input.GetKeyDown(x));
            if (key == _order[_index])
            {
                Turn();
                _index++;
            }
        }
    }

    public void Turn()
    {
        _clicks++;
        _powderGoalSize = _clicks / (float)_clicksToComplete;
        Debug.Log(_clicks);

        if (_clicks >= _clicksToComplete)
        {
            EndMinigame(true);
        }
    }
}
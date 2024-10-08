using UnityEngine;
using UnityEngine.UI;

public class MinigameExample : Minigame
{
    protected override float MinigameTime => GameManager.Instance ? GameManager.Instance.MinigameTime * 0.7f : 5f;
    protected override string MinigameName => "Eat kanelbulle";

    [SerializeField] private Sprite[] _kanelbullar;
    [SerializeField] private Sprite[] _keys;

    [SerializeField] private Image _kanelbullen;
    [SerializeField] private Image _knappen;

    [SerializeField] private Transform[] _keyRefs;

    private int _currentKey = 0;
    private int _currentIndex = 0;

    [SerializeField] private AudioClip[] _eatingSounds;

    protected override void Start()
    {
        base.Start();

        foreach (Transform keyRef in _keyRefs)
            keyRef.gameObject.SetActive(false);

        SetKey();
    }

    private void Update()
    {
        if (IsRunning)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                SubmitKey(0);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                SubmitKey(1);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                SubmitKey(2);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                SubmitKey(3);
        }

        _kanelbullen.sprite = _kanelbullar[_currentIndex];
    }

    private void SetKey()
    {
        _currentKey = Random.Range(0, 4);
        _knappen.sprite = _keys[_currentKey];

        Transform refTransform = _keyRefs[Random.Range(0, _keyRefs.Length)];

        _knappen.transform.position = refTransform.position;
        _knappen.transform.rotation = refTransform.rotation;
    }

    private void SubmitKey(int key)
    {
        if (key == _currentKey)
        {
            _currentIndex++;

            // nom nom
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = _currentIndex == 5 ? _eatingSounds[1] : _eatingSounds[0];
            a.Play();
            Destroy(a, 2f);

            if (_currentIndex >= _kanelbullar.Length - 1)
            {
                EndMinigame(true);
                _knappen.GetComponent<Image>().enabled = false;
            }
            else
            {
                SetKey();
            }
        }
        else
        {
            EndMinigame(false);
            _knappen.GetComponent<Image>().enabled = false;
        }
    }
}
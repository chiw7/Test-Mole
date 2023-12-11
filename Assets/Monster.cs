using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    GameManager gameManager;
    public float maxSecondsOnScreen = 2.5f;
    public float currentSecondsOnScreen = 0;
    void Start()
    {
        Init();
    }

    private void Init()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        ResetCurrentSecondsOnScreen();
    }

    private void OnMouseDown()
    {
        gameManager.AddScore();
        ResetCurrentSecondsOnScreen();
        Hide();
    }

    private void Hide()
    {
        gameManager.HideMonster(gameObject);
    }
    public bool IsActive => gameObject.activeInHierarchy;
    bool OnScreenTimeUp => currentSecondsOnScreen < 0;
    void FixedUpdate()
    {
        TryCountDownToHide();
    }

    private void TryCountDownToHide()
    {
        if (IsActive)
        {
            CountDownCurrentSecondsOnScreen();
        }
        if (OnScreenTimeUp)
        {
            ResetCurrentSecondsOnScreen();
            Hide();
        }
    }

    private void CountDownCurrentSecondsOnScreen()
    {
        currentSecondsOnScreen -= Time.fixedDeltaTime;
    }

    private void ResetCurrentSecondsOnScreen()
    {
        currentSecondsOnScreen = maxSecondsOnScreen;
    }
}

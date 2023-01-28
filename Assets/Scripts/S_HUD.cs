using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_HUD : MonoBehaviour
{
    public GameObject ec;
    public S_EventController manager;
    public float ingameTime;
    private Rigidbody playerRB;

    public TMP_Text _timeText;
    public TMP_Text _SpeedText;
    public GameObject timeParent;
    public GameObject speedParent;
    public GameObject ItemUI;
    public Image itemHudImage;
    public TextMeshProUGUI ItemText;



    bool foundPlayer = false;
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ec == null)
        {
            ec = GameObject.FindWithTag("EventController");

        }
        else
        {
            if (manager == null)
            {
                manager = ec.GetComponent<S_EventController>();
                manager.timer = 0;

            }
            else
            {
                HandleTime();
                HandleSpeed();
                HandleItemUI();
            }
        }
    }
    private void HandleItemUI()
    {
        ItemUI.SetActive(manager.playerHasItem);
        if (manager.playerHasItem == true)
        {
            if (playerRB.gameObject.GetComponent<S_CharInfoHolder>().itemHeld != null)
            {
                ItemText.SetText(playerRB.gameObject.GetComponent<S_CharInfoHolder>().itemHeld.name + "");

            }
            if (playerRB.gameObject.GetComponent<S_CharInfoHolder>().itemSprite != null)
            {
                itemHudImage.GetComponent<Image>().sprite = playerRB.gameObject.GetComponent<S_CharInfoHolder>().itemSprite;

            }
        }

    }
    private void HandleTime()
    {
        if (manager.currentTime < 0)
        {
            timeParent.gameObject.SetActive(true);
            ingameTime = manager.timer;
            DisplayTime(ingameTime);
        }
        else
        {
            timeParent.gameObject.SetActive(false);

        }
    }
    private void DisplayTime(float ingametime)
    {
        float minutes = Mathf.FloorToInt(ingametime / 60);
        float seconds = Mathf.FloorToInt(ingametime % 60);
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    private void HandleSpeed()
    {
        //Debug.Log(playerRB.velocity.magnitude);

        if (manager.currentTime < 0)
        {
            if (!foundPlayer)
            {
                playerRB = FindObjectOfType<S_PlayerInput>().GetComponent<Rigidbody>();
                foundPlayer = true;
            }

            float velocity = playerRB.velocity.magnitude;

            speedParent.gameObject.SetActive(true);
            _SpeedText.text = (velocity / 1.5f).ToString("0.0" + " KM/H");
        }
        else
        {
            speedParent.gameObject.SetActive(false);

        }
    }
}

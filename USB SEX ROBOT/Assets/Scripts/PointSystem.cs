using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour {

    [SerializeField]
    TextMesh budgetDisplay;

    [SerializeField]
    GameObject textWolk;

    [SerializeField]
    public PerpDescription suspect = null;

    [SerializeField]
    float timeBetweenMessages = 10.0f;

    [SerializeField]
    float timeMessageVisibility = 2.0f;

    [SerializeField]
    int startingBudget = 1500;

    [SerializeField]
    float totalGameTime = 60.0f;

    float gameTime = 0.0f;

    int budget = 0;

    string hintMessage = "";

    string[] messages;

    float newStringTimer = 0.0f;

    int randomTextNumber = -1;


	// Use this for initialization
	void Start () {
        if (budgetDisplay == null)
            Debug.LogWarning("no budgetDisplay connected");

        if (textWolk == null)
            Debug.LogWarning("no textWolk connected");

        if (textWolk.GetComponentInChildren<TextMesh>() == null)
            Debug.LogWarning("textWolk doesnt have a TextMesh");


        BudgetChange(startingBudget);
        newStringTimer = 3.0f;
        textWolk.SetActive(false);

    }

    void RandomMessage()
    {
        if (suspect == null)
            return;

        textWolk.SetActive(true);
        newStringTimer = timeBetweenMessages + timeMessageVisibility; 

        messages = new string[5] { "We are looking \n for a " + suspect.sex + ".",
                                    "Our Suspect \n is a " + suspect.age + ".",
                                    "Look out for  \n a " + suspect.height + " person.",
                                    "Our suspect is \nwearing " + suspect.GetColor() + " \n clothes.",
                                    "They are planning \n an attack! \n " + suspect.timeLeft.ToString("F2") + " seconds left!" };

        int selectableLength = suspect.highValueTarget ? messages.Length : messages.Length - 1;

        int newRandomTextNumber = Random.Range(0, selectableLength);

        while(newRandomTextNumber == randomTextNumber)
            newRandomTextNumber = Random.Range(0, selectableLength);

        randomTextNumber = newRandomTextNumber;

        textWolk.GetComponentInChildren<TextMesh>().text = messages[randomTextNumber];
    }

    public void GetNewTarget(PerpDescription newTarget)
    {
        suspect = newTarget;

        if (suspect.highValueTarget)
            suspect.timeLeft = 60.0f;
    }

    public void BudgetChange(int changeOfBudget = 0) 
    {
        budget += changeOfBudget;
        budgetDisplay.text = budget.ToString();

        if (changeOfBudget > 0)
        {
            textWolk.SetActive(true);
            newStringTimer = timeBetweenMessages + timeMessageVisibility;
            textWolk.GetComponentInChildren<TextMesh>().text = "GOOD JOB! \n you'll get your \n next target soon";
            suspect = null;
        }
    }

    void trackSuspect()
    {

    }

	// Update is called once per frame
	void Update () {

        if (newStringTimer <= 0.0f)
        {
            newStringTimer = timeBetweenMessages;
            RandomMessage();
        }
        if (newStringTimer <= timeBetweenMessages && textWolk.activeSelf == true)
            textWolk.SetActive(false);

        newStringTimer -= Time.deltaTime;

	}
}

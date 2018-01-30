using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PointSystem : MonoBehaviour {

    [SerializeField]
    TextMesh budgetDisplay;

    [SerializeField]
    TextMesh timeDisplay;

    [SerializeField]
    GameObject textWolk;

    [SerializeField]
    GameObject targetSeekMask;

    [HideInInspector]
    public PerpDescription suspect;

    [SerializeField]
    float timeBetweenMessages = 10.0f;

    [SerializeField]
    float timeMessageVisibility = 2.0f;

    [SerializeField]
    int startingBudget = 1500;

    [SerializeField]
    float totalGameTime = 60.0f;

    [SerializeField]
    float bonusGameTimeReward = 20.0f;

    [SerializeField]
    ParticleSystem Coins;

    float gameTime = 0.0f;

    int budget = 0;

    string hintMessage = "";

    string[] messages;

    float newStringTimer = 0.0f;

    int randomTextNumber = -1;

    float maxMaskLength = 0.0f;


	// Use this for initialization
	void Start () {
        if (budgetDisplay == null)
            Debug.LogWarning("no budgetDisplay connected");

        if (textWolk == null)
            Debug.LogWarning("no textWolk connected");

        if (textWolk.GetComponentInChildren<TextMesh>() == null)
            Debug.LogWarning("textWolk doesnt have a TextMesh");

        if (targetSeekMask == null)
            Debug.LogWarning("no targetSeekMask connected");

        maxMaskLength = targetSeekMask.transform.localScale.z;


        BudgetChange(startingBudget);
        //hack, remove added time because budget was added.
        totalGameTime -= bonusGameTimeReward;

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
            totalGameTime += bonusGameTimeReward;
            Coins.Play();
            suspect = null;
        }
    }

    void TrackSuspect()
    {
        if (suspect == null)
        {
            targetSeekMask.transform.localScale = new Vector3(targetSeekMask.transform.localScale.x, targetSeekMask.transform.localScale.y, maxMaskLength);
            return;
        }

        

        float maxLength = 100.0f;

        float suspectDistant = Vector3.Distance(transform.position, suspect.GetComponent<Transform>().position);

        if (suspectDistant < maxLength)
        {
            targetSeekMask.transform.localScale = new Vector3(targetSeekMask.transform.localScale.x, targetSeekMask.transform.localScale.y, maxMaskLength * (suspectDistant / maxLength));
        }

    }

	// Update is called once per frame
	void Update () {

        gameTime += Time.deltaTime;

         double hurry = totalGameTime - gameTime;

        timeDisplay.text = System.Math.Round(hurry, 2).ToString();

        if (gameTime > totalGameTime)
        {
            SceneManager.LoadScene("menu", LoadSceneMode.Single);
        }


        TrackSuspect();

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

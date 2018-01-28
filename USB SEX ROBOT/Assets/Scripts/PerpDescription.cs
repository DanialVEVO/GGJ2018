using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpDescription : MonoBehaviour
{
    // Different types of descriptions
    public enum Sex
    {
        Male,
        Female
    };

    public enum Age
    {
        Baby,
        Child,
        Teenager,
        Adult,
        Elderly
    };

    public enum Height
    {
        Short,
        Average,
        Tall
    };

    public AudioClip[] ScreamSoundList;
    public AudioClip[] ImpactSoundList;

    private static Color[] PossibleColors =
    {
        Color.black,
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow
    };

    private static string[] PossibleColorsStr =
    {
        "Black",
        "Blue",
        "Cyan",
        "Gray",
        "Green",
        "Magenta",
        "Red",
        "Yellow"
    };

    // Authorable
    public Sex sex;
    public Age age;
    public Height height;
    public bool highValueTarget;
    public bool suspect = true;
    public int score = 0;
    public int penalty = 0;
    public bool isTarget = false;
    public float timeLeft = -1.0f;
    public float explosionRadius = 10.0f;
    public GameObject explosionParticle;

    // Optionally authorable
    public Color clothesColor = Color.clear;
    private string clothesColorStr = "";

    // Privates
    private LocationInfo locationInfo = null;
    private float disintegration = 0.0f;

    // Use this for initialization
	void Awake ()
    {
        locationInfo = this.gameObject.AddComponent<LocationInfo>();

        if (clothesColor == Color.clear)
            PickRandomColor();
    }
	
	// Update is called once per frame
	void Update ()
    {
        NavMeshNavigator navi = this.gameObject.GetComponentInChildren<NavMeshNavigator>();

        if (navi.isDead)
        {
            disintegration += Time.deltaTime;

            if (disintegration >= 1.0f)
                disintegration = 1.0f;
        }

        UpdateSprites();

        if (timeLeft >= 0.0f)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0.0f)
            {
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
                navi.Die(true);

                // Explosion
                RaycastHit[] castRes = Physics.SphereCastAll(transform.position, explosionRadius, transform.right);

                foreach (RaycastHit hit in castRes)
                {
                    NavMeshNavigator navi2 = hit.collider.gameObject.GetComponentInChildren<NavMeshNavigator>();

                    if (navi2 && navi != navi2)
                        navi2.Die(false);
                }
            }
        }
    }

    private void UpdateSprites()
    {
        NavMeshNavigator navi = this.gameObject.GetComponentInChildren<NavMeshNavigator>();

        float dotResult = Quaternion.Dot(Camera.main.transform.rotation, this.gameObject.transform.rotation);

        bool backVisible = dotResult < 0;
        bool frontVisible = !backVisible;

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            GameObject gameObj = this.gameObject.transform.GetChild(i).gameObject;

            if (gameObj.name == "Front")
            {
                SpriteRenderer spriteRenderer = gameObj.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = frontVisible;
                spriteRenderer.material.SetFloat("_Disintegration", disintegration);

                if (suspect)
                    spriteRenderer.material.SetColor("_MaskColor", clothesColor);

                if (!navi.isDead)
                {
                    gameObj.transform.rotation = Camera.main.transform.rotation;
                    spriteRenderer.transform.localPosition = new Vector3(0, Mathf.PingPong(Time.time * 30.0f, 3) * 0.03f, 0);
                }
            }
            else if (gameObj.name == "Back")
            {
                SpriteRenderer spriteRenderer = gameObj.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = backVisible;
                spriteRenderer.material.SetFloat("_Disintegration", disintegration);

                if (suspect)
                    spriteRenderer.material.SetColor("_MaskColor", clothesColor);

                if (!navi.isDead)
                {
                    gameObj.transform.rotation = Camera.main.transform.rotation;
                    spriteRenderer.transform.localPosition = new Vector3(0, Mathf.PingPong(Time.time * 30.0f, 3) * 0.03f, 0);
                }
            }
        }
    }

    // Utility functions
    private void PickRandomColor()
    {
        int index = Random.Range(0, PossibleColors.Length);

        clothesColor = PossibleColors[index];
        clothesColorStr = PossibleColorsStr[index];
    }

    public string GetPerpHash(string customData)
    {
        return (sex.ToString() + age.ToString() + clothesColor.ToString() + height.ToString() + (customData != null ? customData : ""));
    }

    public string GetLocation()
    {
        return locationInfo.currentLocation;
    }

    public string GetColor()
    {
        return clothesColorStr;
    }

    public AudioClip GetScreamSound()
    {
        if (ScreamSoundList.Length == 0)
            return null;

        int index = Random.Range(0, ScreamSoundList.Length);
        return ScreamSoundList[index];
    }

    public AudioClip GetImpactSound()
    {
        if (ImpactSoundList.Length == 0)
            return null;

        int index = Random.Range(0, ImpactSoundList.Length);
        return ImpactSoundList[index];
    }
}

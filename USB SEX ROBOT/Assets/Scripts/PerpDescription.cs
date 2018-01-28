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
        Color.white,
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
        "White",
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

    // Optionally authorable
    public Color clothesColor = Color.clear;
    private string clothesColorStr = "";

    // Privates
    private LocationInfo locationInfo = null;

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

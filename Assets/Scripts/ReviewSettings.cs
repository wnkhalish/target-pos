using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewSettings : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Text radiusSizeText;
    [SerializeField]
    private Text timeOnTargetText;
    [SerializeField]
    private Text scaleFactorText;
    [SerializeField]
    private Text foamOptionsText;
    [SerializeField]
    public Text numberOfRepeatsText;
    [SerializeField]
    private GameObject editButton;
    [SerializeField]
    private GameObject submitButton;
    [SerializeField]
    private GameObject finishButton;
    [SerializeField]
    private GameObject trashIcon;
    [SerializeField]
    private Text appBarText;
    [SerializeField]
    private GameObject EditSettings;

    void Start()
    {
        SettingsData currentSettings = gameManager.currentSettings;
        radiusSizeText.text = "Radius Size: " + currentSettings.radiusSize;
        timeOnTargetText.text = "Time on Target: " + currentSettings.timeOnTarget;
        scaleFactorText.text = "Scale Factor: " + currentSettings.scaleFactor;
        foamOptionsText.text = "Foam Type: " + currentSettings.foamType;
        numberOfRepeatsText.text = "Number of Repeats: " + currentSettings.numberOfRepeats.ToString();
    }


    public void EnableEditScreen()
    {
        editButton.SetActive(false);
        submitButton.SetActive(false);
        finishButton.SetActive(true);
        trashIcon.SetActive(true);
        EditSettings.SetActive(true);
        radiusSizeText.text = "Radius Size: ";
        timeOnTargetText.text = "Time on Target: ";
        scaleFactorText.text = "Scale Factor: ";
        foamOptionsText.text = "Foam Type: ";
        numberOfRepeatsText.text = "Number of Repeats: ";
        appBarText.text = "CUSTOM TRAINING - EDIT";
    }

    public void DisableEditScreen()
    {
        editButton.SetActive(true);
        submitButton.SetActive(true);
        finishButton.SetActive(false);
        trashIcon.SetActive(false);
        EditSettings.SetActive(false);
        SettingsData currentSettings = gameManager.currentSettings;
        radiusSizeText.text = "Radius Size: " + currentSettings.radiusSize;
        timeOnTargetText.text = "Time on Target: " + currentSettings.timeOnTarget;
        scaleFactorText.text = "Scale Factor: " + currentSettings.scaleFactor;
        foamOptionsText.text = "Foam Type: " + currentSettings.foamType;
        numberOfRepeatsText.text = "Number of Repeats: " + currentSettings.numberOfRepeats.ToString();
        appBarText.text = "CUSTOM TRAINING - REVIEW";

    }
}

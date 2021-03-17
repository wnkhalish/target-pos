using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class EditSettings : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    public Dropdown radiusSizeDropdown;
    [SerializeField]
    public Dropdown timeOnTargetDropdown;
    [SerializeField]
    public Text numberOfRepeatsText;
    [SerializeField]
    public Dropdown scaleFactorDropdown;
    [SerializeField]
    public Dropdown foamTypeDropdown;


    private void OnEnable()
    {
        radiusSizeDropdown.value = radiusSizeDropdown.options.Select(x => x.text).ToList().IndexOf(gameManager.currentSettings.radiusSize);
        timeOnTargetDropdown.value = timeOnTargetDropdown.options.Select(x => x.text).ToList().IndexOf(gameManager.currentSettings.timeOnTarget);
        numberOfRepeatsText.text = gameManager.currentSettings.numberOfRepeats.ToString();
        scaleFactorDropdown.value = scaleFactorDropdown.options.Select(x => x.text).ToList().IndexOf(gameManager.currentSettings.scaleFactor);
        foamTypeDropdown.value = foamTypeDropdown.options.Select(x => x.text).ToList().IndexOf(gameManager.currentSettings.foamType);
    }

   // public void OnSettingsSubmit()
   // {
   //     SettingsData currentSettings = gameManager.currentSettings;
   //     currentSettings.radiusSize = radiusSizeDropdown.options[radiusSizeDropdown.value].text;
   //     currentSettings.timeOnTarget = timeOnTargetDropdown.options[timeOnTargetDropdown.value].text;
   //     currentSettings.numberOfRepeats = numberOfRepeatsText.text.Length == 0 ? 0 : int.Parse(numberOfRepeatsText.text);
   //     currentSettings.scaleFactor = scaleFactorDropdown.options[scaleFactorDropdown.value].text;
    ///    currentSettings.foamType = foamTypeDropdown.options[foamTypeDropdown.value].text;
    //    string json = JsonUtility.ToJson(currentSettings, true);
    //    System.IO.File.WriteAllText("currentSettings.json", json);
   // }
}

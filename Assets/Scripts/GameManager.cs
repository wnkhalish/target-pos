using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class SettingsData
{
    public string radiusSize;
    public string timeOnTarget;
    public int numberOfRepeats;
    public string scaleFactor;
    public string foamType;
    public bool isFoamOptionsEnabled;
}

public class GameManager : MonoBehaviour
{
    GameObject coordinates;
    public bool isScoreUpdated;
    public bool isEditingBullets;
    public List<Vector2> currentSession;
    public SettingsData currentSettings;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject targetScreen;
    [SerializeField]
    private GameObject settingsScreen;
    [SerializeField]
    private GameObject reviewScreen;
    [SerializeField]
    private Dropdown radiusSizeDropdown;
    [SerializeField]
    private Dropdown timeOnTargetDropdown;
    [SerializeField]
    private Text numberOfRepeatsText;
    [SerializeField]
    private Dropdown scaleFactorDropdown;
    [SerializeField]
    private Dropdown foamTypeDropdown;
    [SerializeField]
    private Toggle foamOptionsToggle;
    [SerializeField]
    private GameObject axisLines;
    [SerializeField]
    private Text axisPositionText;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject reviewTarget;
    [SerializeField]
    private GameObject trainingTarget;
    [SerializeField]
    private GameObject bulletShadowPrefab;
    [SerializeField]
    private GameObject toggledSettings;
    private List<GameObject> bulletShadowInstances;
    private List<GameObject> reviewBulletInstances;
    private int selectedReviewBullet;
    private string saveDirectory;


    // Start is called before the first frame update
    void Start()
    {
        isScoreUpdated = false;
        isEditingBullets = false;
        currentSession = new List<Vector2>();
        bulletShadowInstances = new List<GameObject>();
        reviewBulletInstances = new List<GameObject>();
        currentSettings = new SettingsData();
        saveDirectory = "coordinates";
        selectedReviewBullet = -1;
    }


    public void UpdateScore(Vector2 score) {
        if (!isScoreUpdated) {
            scoreText.text = "postion from target center: " + score.ToString();
            isScoreUpdated = true;
            currentSession.Add(score);
            GenerateBulletShadow(score);
        }
    }


    public void ResetScore() {
        if (!isScoreUpdated) return;
        isScoreUpdated = false;
        scoreText.text = "Fire at the target to log the position";
    }

    public void EnablePostGameScreen()
    {
        DestroyBulletShadows();
        targetScreen.SetActive(false);
        settingsScreen.SetActive(true);
        axisLines.SetActive(false);
    }

    public void EnableReviewScreen()
    {
        settingsScreen.SetActive(false);
        reviewScreen.SetActive(true);
        GenerateBullets();
    }

    public void SaveSession()
    {
        if (!isScoreUpdated) return;
        if (!Directory.Exists(saveDirectory)) {
            Directory.CreateDirectory(saveDirectory);
        }

        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
        StreamWriter outputFile = File.CreateText(saveDirectory + "\\" + currentTime + ".txt");

        outputFile.WriteLine("Logs for " + currentTime + " session");

        for (int i = 1; i <= currentSession.Count; i++) {
            outputFile.WriteLine("attempt number " + i.ToString() + ": " + currentSession[i - 1].ToString());
        }

        scoreText.text = "Session Saved: " + currentTime;
        outputFile.Close();

    }

    public void SubmitSettings()
    {
        currentSettings.radiusSize = radiusSizeDropdown.options[radiusSizeDropdown.value].text;
        currentSettings.timeOnTarget = timeOnTargetDropdown.options[timeOnTargetDropdown.value].text;
        currentSettings.numberOfRepeats = numberOfRepeatsText.text.Length == 0 ? 0 : int.Parse(numberOfRepeatsText.text);
        currentSettings.scaleFactor = scaleFactorDropdown.options[scaleFactorDropdown.value].text;
        currentSettings.foamType = foamTypeDropdown.options[foamTypeDropdown.value].text;
        currentSettings.isFoamOptionsEnabled = foamOptionsToggle.isOn;
        string json = JsonUtility.ToJson(currentSettings, true);
        System.IO.File.WriteAllText("currentSettings.json", json);
    }

    public void ListToText(List<Vector2> list)
    {
        // Debug.Log(list[1].ToString());
        string result = "";
        foreach (var item in list)
        {
            result += item.ToString();
            result = result + ";";
        }
        Debug.Log(result);

    }
    public void ShowAimAxis()
    {
        axisLines.SetActive(true);
    }

    public void HideAimAxis()
    {
        axisLines.SetActive(false);
    }

    public void UpdateAxisPosition(Vector2 currentPos)
    {
        axisPositionText.text = currentPos.ToString();
    }

    public void GenerateBullets()
    {
        foreach(Vector2 bulletPos in currentSession)
        {
            Vector3 currentPos = reviewTarget.transform.position + new Vector3(bulletPos.x,bulletPos.y,0);
            GameObject currentRef = Instantiate(bulletPrefab, currentPos, Quaternion.identity);
            reviewBulletInstances.Add(currentRef);
        }
    }

    public void GenerateBulletShadow(Vector2 bulletPos)
    {
        Vector3 currentPos = trainingTarget.transform.position + new Vector3(bulletPos.x, bulletPos.y, 0);
        GameObject shadowRef = Instantiate(bulletShadowPrefab, currentPos, Quaternion.identity);
        bulletShadowInstances.Add(shadowRef);
    }

    public void DestroyBulletShadows()
    {
        foreach(GameObject shadowRef in bulletShadowInstances)
        {
            Destroy(shadowRef);
        }
        bulletShadowInstances.Clear();
    }

    public void ToggleFoamSettings()
    {
        toggledSettings.SetActive(foamOptionsToggle.isOn);
    }

    public void EnableBulletEditing()
    {
        isEditingBullets = true;
    }

    public void DisableBulletEditing()
    {
        isEditingBullets = false;
    }

    public void SelectBullet(GameObject bullet)
    {
        ClearBulletSelection();

        selectedReviewBullet = reviewBulletInstances.FindIndex(x => x.GetHashCode() == bullet.GetHashCode());

        reviewBulletInstances[selectedReviewBullet].GetComponent<ReviewBullet>().SelectBullet();
    }

    public void ClearBulletSelection()
    {
        foreach (GameObject currentBullet in reviewBulletInstances)
            currentBullet.GetComponent<ReviewBullet>().UnSelectBullet();

        selectedReviewBullet = -1;
    }

    public void DeleteSelectedBullet()
    {
        if (selectedReviewBullet < 0) return;

        Destroy(reviewBulletInstances[selectedReviewBullet]);
        reviewBulletInstances.RemoveAt(selectedReviewBullet);
        currentSession.RemoveAt(selectedReviewBullet);
        ClearBulletSelection();
    }

    public bool CheckIsBulletSelected(GameObject bullet)
    {
        return selectedReviewBullet == -1 ? false : reviewBulletInstances[selectedReviewBullet].GetHashCode() == bullet.GetHashCode();
    }
}

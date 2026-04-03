using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantInfoUI : MonoBehaviour
{
    [Header("Popup")]
    public GameObject infoPanel;
    public Image plantImage;
    public TMP_Text plantNameText;
    public TMP_Text descriptionText;

    [Header("Prompt")]
    public GameObject photoPrompt;

    private void Start()
    {
        HideInfo();
        HidePrompt();
    }

    public void ShowPrompt()
    {
        if (!infoPanel.activeSelf)
        {
            photoPrompt.SetActive(true);
        }
    }

    public void HidePrompt()
    {
        photoPrompt.SetActive(false);
    }

    public void ShowInfo(PlantInfoData data)
    {
        if (data == null) return;

        plantNameText.text = data.plantName;
        descriptionText.text = data.description;
        plantImage.sprite = data.plantImage;

        infoPanel.SetActive(true);
        photoPrompt.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool IsOpen()
    {
        return infoPanel.activeSelf;
    }
}
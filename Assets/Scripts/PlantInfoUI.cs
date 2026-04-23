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
    public TMP_Text photoPromptText;

    [Header("Camera UI")]
    public GameObject cameraOverlay;

    public AudioSource audioSource;

    private void Start()
    {
        HideInfo();
        HidePrompt();
        HideCameraOverlay();
    }

    public void ShowPrompt(string message)
    {
        if (!infoPanel.activeSelf)
        {
            photoPrompt.SetActive(true);

            if (photoPromptText != null)
            {
                photoPromptText.text = message;
            }
        }
    }

    public void HidePrompt()
    {
        if (photoPrompt != null)
        {
            photoPrompt.SetActive(false);
        }
    }

    public void ShowCameraOverlay()
    {
        if (cameraOverlay != null)
        {
            cameraOverlay.SetActive(true);
        }
    }

    public void HideCameraOverlay()
    {
        if (cameraOverlay != null)
        {
            cameraOverlay.SetActive(false);
        }
    }

    public void ShowInfo(PlantInfoData data)
    {
        if (data == null) return;

        plantNameText.text = data.plantName;
        descriptionText.text = data.description;
        plantImage.sprite = data.plantImage;

        infoPanel.SetActive(true);
        audioSource.Play();
        HidePrompt();
        HideCameraOverlay();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool IsInfoOpen()
    {
        return infoPanel != null && infoPanel.activeSelf;
    }
}
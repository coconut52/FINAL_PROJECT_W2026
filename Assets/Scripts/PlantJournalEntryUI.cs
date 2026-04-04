using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantJournalEntryUI : MonoBehaviour
{
    public Image entryImage;
    public TMP_Text entryName;
    public TMP_Text entryDescription;
    public TMP_Text entryStatus;

    public Sprite hiddenImage;

    public void SetupLocked()
    {
        if (entryName != null)
            entryName.text = "???";

        if (entryDescription != null)
            entryDescription.text = "No photo taken yet.";

        if (entryStatus != null)
            entryStatus.text = "Not Discovered";

        if (entryImage != null)
        {
            if (hiddenImage != null)
            {
                entryImage.sprite = hiddenImage;
                entryImage.enabled = true;
            }
            else
            {
                entryImage.sprite = null;
                entryImage.enabled = false;
            }
        }
    }

    public void SetupUnlocked(PlantInfoData data)
    {
        if (data == null)
            return;

        if (entryName != null)
            entryName.text = data.plantName;

        if (entryDescription != null)
            entryDescription.text = data.description;

        if (entryStatus != null)
            entryStatus.text = "Photo Taken";

        if (entryImage != null)
        {
            entryImage.sprite = data.plantImage;
            entryImage.enabled = data.plantImage != null;
        }
    }
}
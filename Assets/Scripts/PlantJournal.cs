using System.Collections.Generic;
using UnityEngine;

public class PlantJournal : MonoBehaviour
{
    [Header("Journal Data")]
    public List<PlantInfoData> allPlants = new List<PlantInfoData>();

    [Header("UI References")]
    public GameObject photoBookPanel;
    public Transform entryContainer;
    public GameObject journalEntryPrefab;

    private HashSet<PlantInfoData> discoveredPlants = new HashSet<PlantInfoData>();
    private bool isBookOpen = false;

    private void Start()
    {
        discoveredPlants.Clear();

        if (photoBookPanel != null)
            photoBookPanel.SetActive(false);

        RebuildJournalUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleBook();
        }
    }

    public void AddPhotographedPlant(PlantInfoData plant)
    {
        if (plant == null)
            return;

        if (!discoveredPlants.Contains(plant))
        {
            discoveredPlants.Add(plant);
            RebuildJournalUI();
        }
    }

    public bool HasPhotographed(PlantInfoData plant)
    {
        if (plant == null)
            return false;

        return discoveredPlants.Contains(plant);
    }

    public void ToggleBook()
    {
        isBookOpen = !isBookOpen;

        if (photoBookPanel != null)
            photoBookPanel.SetActive(isBookOpen);

        if (isBookOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            RebuildJournalUI();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void RebuildJournalUI()
    {
        if (entryContainer == null || journalEntryPrefab == null)
            return;

        for (int i = entryContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(entryContainer.GetChild(i).gameObject);
        }

        foreach (PlantInfoData plant in allPlants)
        {
            GameObject entryObj = Instantiate(journalEntryPrefab, entryContainer);
            PlantJournalEntryUI entryUI = entryObj.GetComponent<PlantJournalEntryUI>();

            if (entryUI == null)
                continue;

            if (HasPhotographed(plant))
            {
                entryUI.SetupUnlocked(plant);
            }
            else
            {
                entryUI.SetupLocked();
            }
        }
    }
}
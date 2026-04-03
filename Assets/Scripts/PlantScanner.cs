using UnityEngine;

public class PlantScanner : MonoBehaviour
{
    public Camera playerCamera;
    public float scanDistance = 8f;
    public KeyCode photographKey = KeyCode.Mouse0;
    public LayerMask plantLayers;
    public PlantInfoUI plantInfoUI;

    private PhotographablePlant currentPlant;

    void Update()
    {
        if (playerCamera == null || plantInfoUI == null)
            return;

        if (plantInfoUI.IsOpen())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                plantInfoUI.HideInfo();
            }

            return;
        }

        ScanForPlant();

        if (currentPlant != null && Input.GetKeyDown(photographKey))
        {
            plantInfoUI.ShowInfo(currentPlant.plantInfo);
        }
    }

    void ScanForPlant()
    {
        currentPlant = null;
        plantInfoUI.HidePrompt();

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, scanDistance, plantLayers))
        {
            PhotographablePlant plant = hit.collider.GetComponentInParent<PhotographablePlant>();

            if (plant != null && plant.plantInfo != null)
            {
                currentPlant = plant;
                plantInfoUI.ShowPrompt();
            }
        }
    }
}
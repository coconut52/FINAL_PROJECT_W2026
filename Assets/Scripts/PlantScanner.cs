using UnityEngine;

public class PlantScanner : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public PlantInfoUI plantInfoUI;
    public PlantJournal plantJournal;

    [Header("Plant Detection")]
    public float scanDistance = 8f;
    public LayerMask plantLayers;

    [Header("Camera Mode")]
    public KeyCode cameraModeKey = KeyCode.Mouse1;
    public KeyCode takePictureKey = KeyCode.Mouse0;
    public float normalFOV = 60f;
    public float zoomedFOV = 25f;
    public float zoomSpeed = 10f;

    private PhotographablePlant currentPlant;
    private bool isInCameraMode = false;
    private float targetFOV;

    private void Start()
    {
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = normalFOV;
            targetFOV = normalFOV;
        }
    }

    private void Update()
    {
        if (playerCamera == null || plantInfoUI == null)
            return;

        if (plantInfoUI.IsInfoOpen())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                plantInfoUI.HideInfo();
            }

            return;
        }

        ScanForPlant();

        if (currentPlant != null)
        {
            if (!isInCameraMode)
            {
                plantInfoUI.ShowPrompt("Right Click to Use Camera");
            }

            if (Input.GetKeyDown(cameraModeKey))
            {
                EnterCameraMode();
            }
        }
        else
        {
            if (!isInCameraMode)
            {
                plantInfoUI.HidePrompt();
            }
        }

        if (isInCameraMode)
        {
            HandleCameraMode();
        }

        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * 8f);
    }

    private void ScanForPlant()
    {
        currentPlant = null;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, scanDistance, plantLayers))
        {
            PhotographablePlant plant = hit.collider.GetComponentInParent<PhotographablePlant>();

            if (plant != null && plant.plantInfo != null)
            {
                currentPlant = plant;
            }
        }
    }

    private void EnterCameraMode()
    {
        isInCameraMode = true;
        plantInfoUI.ShowCameraOverlay();
        plantInfoUI.ShowPrompt("Left Click to Take Picture | Scroll to Zoom");
        targetFOV = normalFOV;
    }

    private void ExitCameraMode()
    {
        isInCameraMode = false;
        plantInfoUI.HideCameraOverlay();
        plantInfoUI.HidePrompt();
        targetFOV = normalFOV;
    }

    private void HandleCameraMode()
    {
        if (currentPlant == null)
        {
            ExitCameraMode();
            return;
        }

        plantInfoUI.ShowPrompt("Left Click to Take Picture | Scroll to Zoom");

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            targetFOV -= scroll * zoomSpeed * 10f;
            targetFOV = Mathf.Clamp(targetFOV, zoomedFOV, normalFOV);
        }

        if (Input.GetKeyDown(takePictureKey))
        {
            TakePicture();
        }

        if (Input.GetKeyUp(cameraModeKey))
        {
            ExitCameraMode();
        }
    }

    private void TakePicture()
    {
        if (currentPlant == null || currentPlant.plantInfo == null)
            return;

        if (plantJournal != null)
        {
            plantJournal.AddPhotographedPlant(currentPlant.plantInfo);
        }

        plantInfoUI.HideCameraOverlay();
        plantInfoUI.HidePrompt();
        plantInfoUI.ShowInfo(currentPlant.plantInfo);

        isInCameraMode = false;
        targetFOV = normalFOV;
    }
}
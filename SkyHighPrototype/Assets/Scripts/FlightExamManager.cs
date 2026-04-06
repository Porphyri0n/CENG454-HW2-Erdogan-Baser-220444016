using UnityEngine;
using UnityEngine.UIElements;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    [SerializeField] private string statusLabelName = "StatusLabel";
    [SerializeField] private string missionLabelName = "MissionLabel"; 

    private Label statusLabel;
    private Label missionLabel;

    private bool hasTakenOff = false;
    private bool threatCleared = false;
    private bool missionComplete = false;

    private bool isInDangerZone = false;
    private bool isCountdownActive = false;
    private bool isMissileActive = false;

    private void OnEnable()
    {
        if (uiDocument != null && uiDocument.rootVisualElement != null)
        {
            VisualElement root = uiDocument.rootVisualElement;
            statusLabel = root.Q<Label>(statusLabelName);
            missionLabel = root.Q<Label>(missionLabelName);

            if (statusLabel != null) statusLabel.text = "";
        }
    }


    public void EnterDangerZone()
    {
        isInDangerZone = true;

        if (statusLabel != null)
        {
            statusLabel.text = "Entered a Dangerous Zone!";
            statusLabel.style.color = new StyleColor(Color.red);
        }
    }

    public void ExitDangerZone()
    {
        isInDangerZone = false;
        threatCleared = true;

        if (statusLabel != null)
        {
            statusLabel.text = "Threat Cleared. Safe Space.";
            statusLabel.style.color = new StyleColor(Color.green);
        }
    }

    public void SetCountdownState(bool state) => isCountdownActive = state;
    public void SetMissileActiveState(bool state) => isMissileActive = state;



    public void RegisterTakeoff()
    {
        hasTakenOff = true;
        if (missionLabel != null)
        {
            missionLabel.text = "Mission: Survive the Threat Corridor";
        }
    }

    public void RegisterLanding()
    {
        if (hasTakenOff && threatCleared && !isMissileActive)
        {
            missionComplete = true;
            if (statusLabel != null)
            {
                statusLabel.text = "Mission Accomplished!";
                statusLabel.style.color = new StyleColor(Color.green);
            }
        }
        else if (!threatCleared)
        {
            if (statusLabel != null)
            {
                statusLabel.text = "Landing Rejected: Threat Not Cleared!";
                statusLabel.style.color = new StyleColor(Color.yellow);
            }
        }
    }

    public void RegisterAircraftDestroyed()
    {
        if (statusLabel != null)
        {
            statusLabel.text = "Mission Failed: Aircraft Destroyed.";
            statusLabel.style.color = new StyleColor(Color.red);
        }
    }


    public bool IsInDangerZone() => isInDangerZone;
    public bool IsMissileActive() => isMissileActive;
    public bool IsThreatCleared() => threatCleared;
}
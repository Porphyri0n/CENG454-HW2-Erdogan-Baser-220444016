using UnityEngine;

public class TakeoffZone : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (examManager != null)
            {
                examManager.RegisterTakeoff();
            }
        }
    }
}
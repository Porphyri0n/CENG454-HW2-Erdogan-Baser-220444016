using UnityEngine;
using System.Collections;

public class DangerZoneController : MonoBehaviour
{
    [Header("Sistem Referanslarý")]
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;

    [Header("Tehdit Ayarlarý")]
    [SerializeField] private float missileDelay = 5f;

    private Coroutine activeCountdown;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (examManager != null)
            {
                examManager.EnterDangerZone();
                examManager.SetCountdownState(true);
            }

            activeCountdown = StartCoroutine(CountdownRoutine(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activeCountdown != null)
            {
                StopCoroutine(activeCountdown);
                activeCountdown = null;
            }

            if (examManager != null)
            {
                examManager.SetCountdownState(false);
                examManager.ExitDangerZone();
            }

            if (missileLauncher != null)
            {
                missileLauncher.DestroyActiveMissile();
            }
        }
    }

    private IEnumerator CountdownRoutine(Transform targetTransform)
    {
        yield return new WaitForSeconds(missileDelay);

        if (missileLauncher != null)
        {
            missileLauncher.Launch(targetTransform);
        }

        if (examManager != null)
        {
            examManager.SetMissileActiveState(true);
        }

        activeCountdown = null;
    }
}
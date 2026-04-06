using UnityEngine;
using System.Collections;

public class DangerZoneController : MonoBehaviour
{
    [Header("Sistem Referanslarý")]
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;

    [Header("Tehdit Ayarlarý")]
    [SerializeField] private float missileDelay = 5f; // Yönerge gereði 5 saniye bekleme süresi

    private Coroutine activeCountdown;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Bölgeye giren nesne "Player" (Uçak) mý?
        if (other.CompareTag("Player"))
        {
            // 2. Merkezi sistemleri uyar ve HUD'u "Entered a Dangerous Zone!" olarak güncelle
            if (examManager != null)
            {
                examManager.EnterDangerZone();
                examManager.SetCountdownState(true);
            }

            // 3. 5 saniyelik füze fýrlatma geri sayýmýný baþlat
            activeCountdown = StartCoroutine(CountdownRoutine(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 1. Bölgeden çýkan nesne "Player" mý?
        if (other.CompareTag("Player"))
        {
            // 2. Yönerge (Midterm Report #003) Çözümü: 
            // Eðer oyuncu füze fýrlatýlmadan bölgeden çýktýysa, mevcut geri sayýmý iptal et ki "hayalet füze" oluþmasýn.
            if (activeCountdown != null)
            {
                StopCoroutine(activeCountdown);
                activeCountdown = null;
            }

            // 3. Merkezi sistemi güncelle (HUD "Threat Cleared" olarak deðiþir)
            if (examManager != null)
            {
                examManager.SetCountdownState(false);
                examManager.ExitDangerZone();
            }

            // 4. Görev 3 Hazýrlýðý: Eðer füze zaten fýrlatýldýysa ve oyuncu kaçmayý baþardýysa füzeyi yok et.
            if (missileLauncher != null)
            {
                missileLauncher.DestroyActiveMissile();
            }
        }
    }

    private IEnumerator CountdownRoutine(Transform targetTransform)
    {
        // Belirlenen süre kadar bekle (5 saniye)
        yield return new WaitForSeconds(missileDelay);

        // Süre kesintisiz dolarsa fýrlatma komutunu ver
        if (missileLauncher != null)
        {
            missileLauncher.Launch(targetTransform);
        }

        // Görev yöneticisine sahnede aktif bir füze olduðunu bildir
        if (examManager != null)
        {
            examManager.SetMissileActiveState(true);
        }

        // Fýrlatma iþlemi bittiði için coroutine referansýný temizle
        activeCountdown = null;
    }
}
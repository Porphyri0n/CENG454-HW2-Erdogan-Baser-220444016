using UnityEngine;

public class MissileLauncher : MonoBehaviour
{

    public void Launch(Transform target)
    {
        Debug.Log("Tehdit: Füze fýrlatýldý! Hedef: " + target.name);
    }

    public void DestroyActiveMissile()
    {
        Debug.Log("Tehdit atlatýldý: Aktif füze yok edildi.");
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class WeaponLogic : MonoBehaviour
{
    public int bulletsPerMagazine;
    public int totalMagazines;
    public float currentReloadTime;
    public float reloadTime = 2f;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    protected int bulletsRemainingInMagazine;
    protected bool isReloading;

    public float damage;

    [SerializeField] List<AudioClip> shootClips;

    [SerializeField] TMP_Text bulletsPerMagazine_text;
    [SerializeField] TMP_Text bulletsRemainingInMagazine_text;

    [SerializeField] public GameObject reloadUI;

    public PlayerController playerController;



    void Start()
    {
        currentReloadTime = reloadTime;

        bulletsRemainingInMagazine = bulletsPerMagazine;
        isReloading = false;

        if (reloadUI != null)
        {
            reloadUI.GetComponent<ReloadUI>().reloadTime = reloadTime;
        }

        if (bulletsPerMagazine_text != null && bulletsRemainingInMagazine_text != null)
        {
            bulletsPerMagazine_text.text = bulletsPerMagazine.ToString();
            bulletsRemainingInMagazine_text.text = bulletsRemainingInMagazine.ToString();
        }

        gameObject.tag = "Weapon";
    }

    public void Fire(Vector3 dir, bool isHoming = false, Transform target = null)
    {
        if (isReloading)
        {
            return;
        }

        GameObject bullet = WeaponManager.Instance.GetPlayerBullet();
        bullet.transform.position = bulletSpawnPoint.position;

        PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
        if (bulletScript != null)
        {
            bulletScript.DamageDealt = damage;

            if (!isHoming)
            {
                bulletScript.dir = dir;
            }
            else
            {
                bulletScript.IsHoming = isHoming;
                bulletScript.target = target;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
            }
        }

        PlayShootClip();

        bulletsRemainingInMagazine--;
        bulletsRemainingInMagazine_text.text = bulletsRemainingInMagazine.ToString();

        if (bulletsRemainingInMagazine == 0)
        {
            Reload();
            reloadUI.SetActive(true);
        }
    }

    void Reload()
    {
        if (totalMagazines > 0)
        {
            isReloading = true;

            Invoke("FinishReloading", reloadTime);
        }
        else
        {
            playerController.switchCurrentWeapon();
            Debug.Log("Weapon is empty!");
        }
    }

    void FinishReloading()
    {
        bulletsRemainingInMagazine = bulletsPerMagazine;
        totalMagazines--;
        bulletsRemainingInMagazine_text.text = bulletsRemainingInMagazine.ToString();


        isReloading = false;
    }

    private void PlayShootClip()
    {
        int randIndex = Random.Range(0, shootClips.Count);
        float randVolume = Random.Range(0.8f, 1.0f);
        AudioSource.PlayClipAtPoint(shootClips[randIndex], bulletSpawnPoint.position, randVolume);
    }
}
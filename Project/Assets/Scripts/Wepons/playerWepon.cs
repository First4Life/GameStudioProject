using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWepon : MonoBehaviour
{
    int slot1Value = 0;
    int slot2Value = 1;
    int slot3Value = 2;

    float fireCycle = 0;
    float reloadCycle = 0;
    bool reloadState = false;

    float currentFireRate = 1;
    float currentBulletCount = 1;
    float currentMagSize = 1;
    float currentReloadTime = 1;
    float currentReserveSize = 1;
    float currentReserveAmount = 1;
    float currentDamage = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // have current wepons in the loadout a numerical value, the value of the gun in that slot
        // then to update the current firerate put the numerical value of the gun you are swiching thiugfh into a shwich statment that will pull; the firerate ect from the approprate wepon script
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("mouse1") && fireCycle <= 0)
        {
            fire();
        }
        if (Input.GetKeyDown("R")&& reloadState == false)
        {
            reload();
        }





        // change wepons in equipmed slots
        if (Input.GetKeyDown("1"))
        {
            weponSwich(slot1Value);
        }
        if (Input.GetKeyDown("2"))
        {
            weponSwich(slot2Value);
        }
        if (Input.GetKeyDown("3"))
        {
            weponSwich(slot3Value);
        }



        fireCycle = fireCycle - Time.deltaTime;
    }

    void fire()
    {
        fireCycle = currentFireRate;
        currentMagSize--;

        //do shooty things with ray casting
    }

    void reload()
    {
        bool spareAmmo = false;
        if (currentBulletCount < 0)
        {
            spareAmmo = true;
        }
        currentBulletCount = currentMagSize;
        if (spareAmmo == true)
        {
            currentBulletCount++;
            spareAmmo = false;
        }
        currentReserveAmount = currentReserveAmount - currentMagSize;

    }

    void weponSwich(int _slotValue)
    {
        switch (_slotValue)
        {
            case 0:
                Debug.Log("changeing case 0");
                break;
            case 1:
                Debug.Log("changeing case1" );
                break;
            case 2:
                Debug.Log("changeing case 2");
                break;
            case 3:
                Debug.Log("changeing case 3");
                break;
                // add as many as needed for all guns



            default:
                Debug.Log("no valid wepon value");
                break;

        }
            
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteraction : MonoBehaviour
{

    int slot1Gun;
    int slot1Clip;
    int slot1Reserve;

    int slot2Gun;
    int slot2Clip;
    int slot2Reserve;

    int slot3Gun;
    int slot3Clip;
    int slot3Reserve;

    int currentSlot = 0;
    int currentAmmo = 0;
    int currentReserve = 0;

    int currentClipSize = 0;    
    int currentReserveMax = 0;
    float currentDamage = 0;
    float currentFireRate = 0;
    float currentRelaodTime = 0;
    float reloadProgress = 0;

    bool reloading = false;


    //hello

    // Start is called before the first frame update
    void Start()
    {
        currentSlot = 3;
        slot1Gun = 1;
        slot2Gun = 2;
        slot3Gun = 3;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("Mouse 1")&&reloading == false)
        {
            shoot();
        }
        if(Input.GetKeyDown("r") && reloading == false)
        {
            reloading = true;
            reloadProgress = currentRelaodTime;

        }

        if(Input.GetKeyDown("1"))
        {
            saveCurrentAmmo(1);
            switchGun(slot1Gun);
        }
        if (Input.GetKeyDown("2"))
        {
            saveCurrentAmmo(2);
            switchGun(slot2Gun);
        }
        if (Input.GetKeyDown("3"))
        {
            saveCurrentAmmo(3);
            switchGun(slot3Gun);
        }







        if (reloadProgress <=0)
        {
            reload();
            reloading = false;
        }
        reloadProgress = reloadProgress - Time.deltaTime;
    }


    void shoot()
    {
        // do shooty things 
    }

    void reload()
    {
        // do relaody thinsg 
        bool roundChambered = false;
        if(currentAmmo > 0)
        {
            roundChambered = true;
        }

        currentReserve = currentReserve - currentClipSize;
        currentAmmo = currentClipSize;

        if(roundChambered == true)
        {
            currentAmmo++;
        }

    }

    void saveCurrentAmmo(int _currentSlot)
    {
        // switch....
        switch(currentSlot)
        {
            case 1:
                slot1Clip = currentAmmo;
                slot1Reserve = currentReserve;
                break;
            case 2:
                slot2Clip = currentAmmo;
                slot2Reserve = currentReserve;
                break;
            case 3:
                slot3Clip = currentAmmo;
                slot3Reserve = currentReserve;
                break;
            default:
                Debug.Log("no gun slot");
                break;
           
        }

    }

    void switchGun(int _gunBeingSwitchedTo)
    {
        switch(_gunBeingSwitchedTo)
        {
            case 1:
                currentFireRate = GetComponent<m1911>().fireRate;
                currentClipSize = GetComponent<m1911>().clipSize;
                currentReserveMax = GetComponent<m1911>().ammoReserve;
                currentDamage = GetComponent<m1911>().damage;
                currentRelaodTime = GetComponent<m1911>().reloadTime;



                break;
            case 2:
                break;
        }

    }

}

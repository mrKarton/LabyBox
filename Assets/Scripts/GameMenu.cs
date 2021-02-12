using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    bool showing = false;
    KartonWeapons.ShootingController shooter;
    KartonWeapons.GunInventoryController inventoryController;

    private void Start()
    {
        menu.SetActive(false);

        shooter = GameObject.FindObjectOfType<KartonWeapons.ShootingController>();
        inventoryController = GameObject.FindObjectOfType<KartonWeapons.GunInventoryController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showing = !showing;

            menu.SetActive(showing);

            switch (showing)
            {
                case true:
                    inventoryController.canChange = false;
                    shooter.canShoot = false;
                    break;

                case false:
                    inventoryController.canChange = true;
                    shooter.canShoot = true;
                    break;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}

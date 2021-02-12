using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropMenu : MonoBehaviour
{
    public Prop[] props;
    public GridLayoutGroup content;
    public GameObject menu;

    public MenuPropElement menuProp;

    public GameObject player;

    bool showing = false;
    KartonWeapons.ShootingController shooter;
    KartonWeapons.GunInventoryController inventoryController;

    void Start()
    {
        menu.SetActive(false);

        player = GameObject.FindObjectOfType<Move>().gameObject;
        shooter = GameObject.FindObjectOfType<KartonWeapons.ShootingController>();
        inventoryController = GameObject.FindObjectOfType<KartonWeapons.GunInventoryController>();

        props = Resources.LoadAll<Prop>("Props");

        foreach(Prop p in props)
        {
            var menuElement = Instantiate(menuProp.gameObject);
            menuElement.transform.parent = content.transform;
            menuElement.GetComponent<MenuPropElement>().title.text = p.menuTitle;
            menuElement.GetComponent<MenuPropElement>().button.onClick.AddListener(delegate {
                SpawnProp(p);
            });
        }

    }

    public void SpawnProp(Prop prop)
    {
        Transform body = player.transform;

        Instantiate(prop.obj, body.position + body.forward * 3, Quaternion.identity);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            showing = !showing;

            menu.SetActive(showing);

            switch(showing)
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

}

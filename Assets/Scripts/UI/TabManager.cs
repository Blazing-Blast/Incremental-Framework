using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[System.Serializable]
// Contains all info needed to spawn a tabButton, if you already have one in the scene, you only need obj and linked tab.
public class TabInfo
{
    // The name the resulting GameObject will have.
    public string name;
    // The text on the button.
    public string displayName;
    // The colour of said text.
    public Color textColor;
    // The background colour of the button.
    public Color backgroundColor;
    // The outline colour of the button.
    public Color outlineColor;
    // The actual GameObject of the button.
    public GameObject obj;
    // The tab that will open once the button is pressed.
    public GameObject linkedTab;
}

public class TabManager : MonoBehaviour
{
    public List<TabInfo> tabs;
    // The prefab from which new buttons will be spawned (by delault located in assets/Prefabs).
    public GameObject buttonPrefab;
    // The tab that is currently open.
    public GameObject activeTab;
    // The GameObject the tabs are children from.
    public Transform tabParent;

    // This spawns the button gameObjects if they weren't present already and makes sure only the first element in 'tabs' is active.
    public void Start()
    {
        foreach (TabInfo info in tabs)
        {
            info.linkedTab = tabParent.Find(info.name).gameObject;
            InitializeTabButton(info);
            info.linkedTab.SetActive(false);
        }

        SetTab(tabs[0].linkedTab);
    }




    // Sets a tab active and disables the previous one.
    public void SetTab(GameObject tab)
    {
        if (activeTab != tab)
        {
            tab.SetActive(true);
            if (activeTab != null)
            {
                activeTab.SetActive(false);
            }

            activeTab = tab;
        }
    }

    // Spawns the TabButton and gives it its properties if it didn't exist already.
    public GameObject InitializeTabButton(TabInfo info)
    {
        GameObject obj;

        // It only does this if no pre-existing GameObject was given.
        if (info.obj == null)
        {
            obj = Instantiate(buttonPrefab);

            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            Image[] images = obj.GetComponentsInChildren<Image>();

            text.text = info.displayName;
            images[0].color = info.backgroundColor;
            images[1].color = info.outlineColor;
            text.color = info.textColor;
            obj.name = info.name;

            // Warns you when you have something seetrough.
            if (info.textColor.a == 0 || info.backgroundColor.a == 0 || info.outlineColor.a == 0)
            {
                Debug.LogWarning($"You have an alpha of 0 in {info.name}");
            }
        }

        else 
        { 
            obj = info.obj;
        }

        obj.transform.SetParent(gameObject.transform);
        obj.GetComponent<OpenTabOnClick>().linkedTab = info.linkedTab;
        info.obj = obj;
        return obj;
    }


    // Binds the Tabs to their buttons.
    public GameObject InitializeTab(string name)
    {
        Transform tab = tabParent.Find(name);

        // Warns you when there is a button that doesn't have a tab that already exists
        if (tab is null)
        {
            Debug.LogWarning($"There is no tab in GameObject {tabParent.name} with name {name}");
        }
        tab.gameObject.SetActive(false);
        return tab.gameObject;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Transform itemsParent;

    public KeyCode[] itemKeybinds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int keybindIndex = 0; keybindIndex < itemKeybinds.Length; keybindIndex++)
        {
            if (Input.GetKeyDown(itemKeybinds[keybindIndex]))
            {
                for (int itemIndex = 0; itemIndex < itemsParent.childCount; itemIndex++)
                {
                    if (itemIndex == keybindIndex)
                    {
                        itemsParent.GetChild(itemIndex).gameObject.SetActive(true);
                    }
                    else
                    {
                        itemsParent.GetChild(itemIndex).gameObject.SetActive(false);
                    }
                }

                break;
            }
        }
    }
}

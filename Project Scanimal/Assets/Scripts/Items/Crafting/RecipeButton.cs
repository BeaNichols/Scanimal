using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    public RecipeSo item;
    private Image objectImage;
    [SerializeField]
    private GameObject[] recipes;

    private void Start()
    {
        objectImage = GetComponent<Image>();
        objectImage.sprite = item.itemToCraft.image;
        recipes = GameObject.FindGameObjectsWithTag("RecipeHolder");
        //foreach (var recipe in recipes)
        //{ 
        //    recipe.SetActive(false);
        //}
    }

    public void OnClickDisplay()
    {
        foreach (var recipe in recipes)
        {
            recipe.SetActive(false);
        }

        TextMeshProUGUI itemName = GameObject.Find("Object Name").GetComponent<TextMeshProUGUI>();
        itemName.text = item.itemToCraft.name.ToString();

        for (int i = 0; i < item.items.Count; i++)
        {
            recipes[i].SetActive(true);
            Image itemSprite = recipes[i].GetComponentInChildren<Image>();
            itemSprite.sprite = item.items[i].image;
            TextMeshProUGUI amount = recipes[i].GetComponentInChildren<TextMeshProUGUI>();
            amount.text = "X " + item.Amount[i].ToString();

        }
    }
}

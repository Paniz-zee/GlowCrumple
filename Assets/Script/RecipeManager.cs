//using System.Collections.Generic;
//using UnityEngine;

//public class RecipeManager : MonoBehaviour
//{
//    public static RecipeManager Instance;

//    private List<Recipe> learnedRecipes = new List<Recipe>();

//    public GameObject recipeButtonPrefab;
//    public Transform recipeGrid;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void LearnRecipe(Recipe newRecipe)
//    {
//        if (!learnedRecipes.Contains(newRecipe))
//        {
//            learnedRecipes.Add(newRecipe);
//            CreateRecipeButton(newRecipe);
//        }
//    }

//    private void CreateRecipeButton(Recipe recipe)
//    {
//        GameObject button = Instantiate(recipeButtonPrefab, recipeGrid);
//        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = recipe.recipeName;
//        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SelectRecipe(recipe));
//    }

//    private void SelectRecipe(Recipe recipe)
//    {
//        Debug.Log("Selected recipe: " + recipe.recipeName);
//        CookingStation.GenerateCookingSlots(recipe);
//    }
//}

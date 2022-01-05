import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class RecipesService {

  async createRecipe(recipe) {
    const res = await api.post('api/recipes', recipe)
    logger.log(res.data)
    AppState.recipes.unshift(res.data)
  }

  async deleteRecipe(id) {
    const res = await api.delete('api/recipes/' + id)
    logger.log(res.data)
    AppState.recipes = AppState.recipes.filter(r => r.id !== id)
  }

  async searchRecipes(data) {
    let search = `%${data}%`
    // logger.log(search)
    const res = await api.get('api/recipes/search/' + search)
    // logger.log(res.data)
    AppState.recipes = res.data
  }

  async getAll() {
    const res = await api.get('api/recipes')
    AppState.recipes = res.data
    // logger.log(AppState.recipes)
  }
  async getMyFavorites() {
    const res = await api.get('account/favorites')
    AppState.recipes = res.data
    // logger.log(AppState.recipes)
  }

  // NOTE to hide the favorite recipes button on favorites, you could maybe getMyFavorites on homePage after getMyRecipes, and then splice in the favorites in place where ever the recipeId matches the favoriteId? Or you could have two arrays in the AppState, one for all and one for FavoriteRecipes, and then you put a function on the recipe.vue that tries to find the recipe in mygroups, and use that to conditionally style the recipe. But then you would need to change the way the filter works with the buttons calling to fill the same array. 

  async getMyRecipes() {
    const res = await api.get('api/recipes/mine')
    AppState.recipes = res.data
    // logger.log(AppState.recipes)
  }

  async getByCategory(category) {
    const res = await api.get('api/recipes/category/' + category)
    // logger.log(res.data)
    AppState.recipes = res.data
  }

  async favorite(id) {
    const res = await api.post('account/favorites', { "id": id })
    AppState.recipes = AppState.recipes.filter(r => r.id !== id)
    // NOTE Hacky workaround to remove the favorited recipe from Home, until they refresh page. Moving on to other important things. 
    // logger.log(res.data)
  }

  async unfavorite(id) {
    const res = await api.delete('account/favorites/' + id)
    AppState.recipes = AppState.recipes.filter(r => r.id !== id)
    // logger.log(res.data)
    // logger.log(AppState.recipes)
  }
}

export const recipesService = new RecipesService();
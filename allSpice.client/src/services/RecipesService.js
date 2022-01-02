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

  // NOTE to hide the favorite recipes button on favorites, you could maybe getMyFavorites on homePage after getMyRecipes, and then splice in the favorites in place where ever the recipeId matches the favoriteId?

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
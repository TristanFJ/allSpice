import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class RecipesService {
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

  async getMyRecipes() {
    const res = await api.get('api/recipes/mine')
    AppState.recipes = res.data
    // logger.log(AppState.recipes)
  }

  async unfavorite(id) {
    const res = await api.delete('account/favorites/' + id)
    AppState.recipes = AppState.recipes.filter(r => r.id !== id)
    // logger.log(res.data)
    // logger.log(AppState.recipes)
  }
}

export const recipesService = new RecipesService();
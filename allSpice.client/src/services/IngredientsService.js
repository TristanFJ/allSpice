import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";


class IngredientsService {
  async getById(id) {
    const res = await api.get('api/ingredients/' + id)
    AppState.ingredients = res.data
    // logger.log('res.data', res.data)
    // logger.log('appstate.ingredients', AppState.ingredients)
  }

  async addIngredient(ingredient) {
    const res = await api.post('api/ingredients', ingredient)
    logger.log(res.data)
    AppState.ingredients.push(res.data)
  }

  async updateIngredient(data, id) {
    const res = await api.put('api/ingredients/' + id, data)
    logger.log(res.data)

  }

  async deleteIngredient(id) {
    const res = await api.delete('api/ingredients/' + id)
    logger.log(res.data)
    AppState.ingredients = AppState.ingredients.filter(i => i.id !== id)
  }
}

export const ingredientsService = new IngredientsService();
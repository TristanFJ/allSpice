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
}

export const ingredientsService = new IngredientsService();
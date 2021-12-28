import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class StepsService {
  async getById(id) {
    const res = await api.get('api/steps/' + id)
    // logger.log(res.data)
    AppState.steps = res.data
  }

  async addStep(step) {
    const res = await api.post('api/steps', step)
    logger.log(res.data)
    AppState.steps.push(res.data)
  }
}
export const stepsService = new StepsService();
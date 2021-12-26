<template>
  <div class="filter">
    <div class="row">
      <div class="col-3 p-0">
        <button
          type="button"
          class="btn btn-light m-1 elevation-2"
          @click="allRecipes()"
        >
          <b>Home</b>
        </button>
      </div>
      <div class="col-6 p-0">
        <button
          type="button"
          class="btn btn-light m-1 elevation-2"
          @click="myRecipes()"
        >
          <b>My Recipes</b>
        </button>
      </div>
      <div class="col-3 p-0">
        <button
          type="button"
          class="btn btn-light m-1 elevation-2"
          @click="myFavorites()"
        >
          <b>Favorites</b>
        </button>
      </div>
    </div>
  </div>
</template>


<script>
import { recipesService } from "../services/RecipesService"
import { logger } from "../utils/Logger"
export default {
  setup() {
    return {
      async allRecipes() {
        try {
          // logger.log("Home")
          await recipesService.getAll()
        } catch (error) {
          logger.log(error)
        }
      },

      async myRecipes() {
        try {
          await recipesService.getMyRecipes()
        } catch (error) {
          logger.log(error)
        }
      },

      async myFavorites() {
        try {
          // logger.log("My Favorites")
          await recipesService.getMyFavorites()
        } catch (error) {
          logger.log(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
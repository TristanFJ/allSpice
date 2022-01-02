<template>
  <div class="filter" v-if="account.id">
    <div class="row">
      <div class="col-sm-3 p-0">
        <button
          type="button"
          class="btn btn-light m-1 elevation-2"
          @click="allRecipes()"
        >
          <b>Home</b>
        </button>
      </div>
      <div class="col-sm-3 p-0">
        <button
          type="button"
          class="btn btn-light m-1 elevation-2"
          @click="myRecipes()"
        >
          <b>Mine</b>
        </button>
      </div>
      <div class="col-sm-3 p-0">
        <button
          type="button"
          class="btn btn-light m-1 elevation-2"
          @click="myFavorites()"
        >
          <b>Favorites</b>
        </button>
      </div>
      <div class="col-sm-3 p-0 dropdown">
        <button
          class="btn btn-light m-1 elevation-2"
          type="button"
          id="dropdownMenuButton1"
          data-bs-toggle="dropdown"
          aria-expanded="false"
          required
        >
          <b>{{ state }}</b>
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li v-for="category in categories" :key="category">
            <div
              class="dropdown-item selectable"
              required
              @click="
                state = category;
                getByCategory(state);
              "
            >
              {{ category }}
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { recipesService } from "../services/RecipesService"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
export default {
  setup() {
    const state = ref('Category')
    return {
      categories: computed(() => AppState.categories),
      account: computed(() => AppState.account),
      state,


      async allRecipes() {
        try {
          // logger.log("Home")
          await recipesService.getAll()
        } catch (error) {
          logger.error(error)
        }
      },

      async myRecipes() {
        try {
          await recipesService.getMyRecipes()
        } catch (error) {
          logger.error(error)
        }
      },

      async myFavorites() {
        try {
          // logger.log("My Favorites")
          await recipesService.getMyFavorites()
        } catch (error) {
          logger.error(error)
        }
      },

      async getByCategory(state) {
        try {
          await recipesService.getByCategory(state)
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
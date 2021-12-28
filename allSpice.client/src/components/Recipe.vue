<template>
  <div class="card mb-3 recipe elevation-3 m-2 p-3 my-4">
    <div class="row g-0">
      <div class="col-4">
        <img
          :src="recipe.imgUrl"
          class="img-fluid rounded-start elevation-2"
          :alt="recipe.title"
        />
      </div>
      <div class="col-8">
        <div
          class="card-body selectable rounded p-1 m-2"
          data-bs-toggle="modal"
          :data-bs-target="`#recipeModal-${recipe.id}`"
          @click="getById(recipe.id)"
        >
          <h4 class="card-title" style="text-decoration: underline">
            {{ recipe.title }}
          </h4>
          <p class="card-text">
            {{ recipe.subtitle }}
          </p>
          <p class="card-text">
            <small class="text-muted">{{ recipe.category }}</small>
          </p>
        </div>
      </div>
    </div>
    <div class="row" v-if="account.id">
      <div class="col-12 text-end p-0 py-2 favorite" v-if="!recipe.favoriteId">
        <button
          type="button"
          class="btn btn-primary btn-sm rounded-pill elevation-2"
          @click="favorite(recipe.id)"
        >
          Favorite
        </button>
      </div>
      <div class="col-12 text-end p-0 py-2 favorite" v-else>
        <button
          type="button"
          class="btn btn-secondary btn-sm rounded-pill elevation-2"
          @click="unfavorite(recipe.id)"
        >
          Unfavorite
        </button>
      </div>
    </div>
  </div>
  <RecipeModal :id="'recipeModal-' + recipe.id" :recipe="recipe" />
</template>

// TODO figure out a way to tell if it's already favorited on the front end 
// not sure if this stuff is MVP. Might want to focus on other things like Modal and ingredients etc
// I could create a new bool property called favorited, but then you'd be manipulating the actual recipe object and I don't think that would be many to many.

<script>
import { computed } from "@vue/reactivity"
import { ingredientsService } from "../services/IngredientsService"
import { recipesService } from "../services/RecipesService"
import { stepsService } from "../services/StepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { AppState } from "../AppState"
export default {
  props: { recipe: { type: Object } },
  setup() {
    return {

      account: computed(() => AppState.account),


      async getById(id) {
        try {
          await ingredientsService.getById(id)
          await stepsService.getById(id)
        } catch (error) {
          logger.log(error)
        }
      },

      async favorite(id) {
        try {
          await recipesService.favorite(id)
          Pop.toast("Favorited!", 'success')
        } catch (error) {
          logger.log(error)
        }
        // logger.log(id)
      },

      async unfavorite(id) {
        try {
          await recipesService.unfavorite(id)
          Pop.toast("Unfavorited!", 'success')
        } catch (error) {
          logger.log(error)
        }
        // logger.log(id)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.card {
  height: 25vh;
}

img {
  height: 20vh;
  object-fit: cover;
}

p {
  margin: 0;
}
</style>
<template>
  <!-- Button trigger modal -->
  <!-- <button
    type="button"
    class="btn btn-primary"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
  >
    Launch demo modal
  </button> -->

  <!-- Modal -->
  <div
    class="modal opacity"
    id="recipeModal"
    tabindex="-1"
    aria-labelledby="recipe modal"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-scrollable modal-xl">
      <div class="modal-content modal-xl">
        <div class="modal-header">
          <h5 class="modal-title">{{ recipe.title }}</h5>
          <h6 class="modal-title ms-2 p-2 bg-secondary rounded-pill">
            {{ recipe.category }}
          </h6>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>

        <div class="modal-body">
          <div class="container-fluid">
            <div class="row justify-content-between">
              <div class="col-lg-4">
                <p class="text-muted">{{ recipe.subtitle }}</p>
                <img
                  :src="recipe.imgUrl"
                  alt=""
                  class="rounded elevation-2 my-1"
                />
              </div>
              <div class="col-lg-4 bg-secondary rounded elevation-2 my-1">
                <h5 class="m-2 p-2 rounded bg-grey elevation-2">Steps:</h5>
                <ul>
                  <div v-for="step in steps" :key="step.id">
                    <b>{{ step.sequence }}</b
                    >:
                    {{ step.body }}
                  </div>
                </ul>
                <div
                  class="row bottom m-auto"
                  v-if="recipe.creatorId === account.userId"
                >
                  <div class="col-12">
                    <form @submit.prevent="addStep(recipe.id)">
                      <div class="input-group">
                        <input
                          type="number"
                          class="form-control"
                          placeholder="Sequence"
                          required
                          aria-label="Step"
                          aria-describedby="Step"
                          v-model="state.editable.sequence"
                        />
                        <input
                          type="text"
                          class="form-control"
                          placeholder="Step"
                          required
                          aria-label="Step"
                          aria-describedby="Step"
                          v-model="state.editable.body"
                        />
                        <button
                          class="btn btn-primary"
                          type="submit"
                          id="button-addon2"
                        >
                          +
                        </button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
              <div class="col-lg-3 bg-secondary rounded elevation-2 my-1">
                <h5 class="m-2 p-2 rounded bg-grey elevation-2">
                  Ingredients:
                </h5>
                <ul>
                  <li v-for="ingredient in ingredients" :key="ingredient.id">
                    {{ ingredient.quantity }}, {{ ingredient.name }}
                  </li>
                </ul>
                <div
                  class="row bottom m-auto"
                  v-if="recipe.creatorId === account.id"
                >
                  <div class="col-12">
                    <div class="input-group">
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Ingredient"
                        required
                        aria-label="Ingredient"
                        aria-describedby="button-addon2"
                      />
                      <button
                        class="btn btn-primary"
                        type="submit"
                        id="button-addon2"
                      >
                        +
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="modal-footer">
          <button
            v-if="!recipe.favoriteId && account.id"
            type="button"
            class="btn btn-secondary btn-sm rounded-pill elevation-2"
            data-bs-dismiss="modal"
            @click="favorite(recipe.id)"
          >
            Favorite
          </button>
          <button
            v-else-if="recipe.favoriteId && account.id"
            type="button"
            class="btn btn-secondary btn-sm rounded-pill elevation-2"
            data-bs-dismiss="modal"
            @click="unfavorite(recipe.id)"
          >
            Unfavorite
          </button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, reactive } from "@vue/reactivity"
import { AppState } from "../AppState"
import { recipesService } from "../services/RecipesService"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { stepsService } from "../services/StepsService"
export default {
  props: { recipe: { type: Object } },

  setup() {
    const state = reactive({
      editable: {},
    })
    return {
      ingredients: computed(() => AppState.ingredients),
      steps: computed(() => AppState.steps),
      account: computed(() => AppState.account),
      state,


      async addStep(id) {
        state.editable.recipeId = id
        logger.log(state.editable)
        try {
          await stepsService.addStep(state.editable)
          state.editable = {}
        } catch (error) {
          logger.error(error)
        }
      },

      async favorite(id) {
        try {
          await recipesService.favorite(id)
          Pop.toast("Favorited!", 'success')
        } catch (error) {
          logger.error(error)
        }
        // logger.log(id)
      },

      async unfavorite(id) {
        try {
          await recipesService.unfavorite(id)
          Pop.toast("Unfavorited!", 'success')
        } catch (error) {
          logger.error(error)
        }
        // logger.log(id)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  width: 100%;
}

.bottom {
  align-self: flex-end;
  width: 100%;
  // height: 50px;
}
</style>
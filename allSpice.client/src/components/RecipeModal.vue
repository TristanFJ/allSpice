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
          <i
            @click="deleteRecipe(recipe.id)"
            class="mdi mdi-trash-can text-danger rounded selectable me-3"
            title="delete"
            v-if="recipe.creatorId === account.id"
            data-bs-dismiss="modal"
          ></i>

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
              <div class="col-lg-3 bg-secondary rounded elevation-2 my-1">
                <h5 class="m-2 p-2 rounded bg-grey elevation-2">Steps:</h5>
                <ul class="p-0">
                  <div v-for="step in steps" :key="step.id">
                    <i
                      @click="deleteStep(step.id)"
                      class="
                        mdi mdi-trash-can
                        text-danger
                        rounded
                        selectable
                        me-3
                      "
                      title="delete"
                      v-if="recipe.creatorId === account.id"
                    ></i>
                    <b
                      :class="
                        recipe.creatorId === account.id
                          ? 'selectable rounded'
                          : ''
                      "
                      :contenteditable="
                        recipe.creatorId === account.id ? 'true' : 'false'
                      "
                      @blur="updateStepSequence(step.id)"
                      >{{ step.sequence }}</b
                    >:
                    <span
                      :class="
                        recipe.creatorId === account.id
                          ? 'selectable rounded'
                          : ''
                      "
                      :contenteditable="
                        recipe.creatorId === account.id ? 'true' : 'false'
                      "
                      @blur="updateStepBody(step.id)"
                    >
                      {{ step.body }}
                    </span>
                  </div>
                </ul>
                <div
                  class="row bottom m-auto"
                  v-if="recipe.creatorId === account.id"
                >
                  <div class="col-12 my-2">
                    <form @submit.prevent="addStep(recipe.id)">
                      <div class="input-group">
                        <input
                          type="number"
                          class="form-control"
                          placeholder="Order"
                          required
                          aria-label="Step"
                          aria-describedby="Step"
                          min="1"
                          v-model="state.step.sequence"
                        />
                        <input
                          type="text"
                          class="form-control"
                          placeholder="Step"
                          required
                          aria-label="Step"
                          aria-describedby="Step"
                          v-model="state.step.body"
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
              <div class="col-lg-4 bg-secondary rounded elevation-2 my-1">
                <h5 class="m-2 p-2 rounded bg-grey elevation-2">
                  Ingredients:
                </h5>
                <div v-for="ingredient in ingredients" :key="ingredient.id">
                  <i
                    @click="deleteIngredient(ingredient.id)"
                    class="
                      mdi mdi-trash-can
                      text-danger
                      rounded
                      selectable
                      me-3
                    "
                    title="delete"
                    v-if="recipe.creatorId === account.id"
                  ></i>

                  <b
                    :class="
                      recipe.creatorId === account.id
                        ? 'selectable rounded'
                        : ''
                    "
                    :contenteditable="
                      recipe.creatorId === account.id ? 'true' : 'false'
                    "
                    @blur="updateIngredientQuantity(ingredient.id)"
                  >
                    {{ ingredient.quantity }}</b
                  >,
                  <span
                    :class="
                      recipe.creatorId === account.id
                        ? 'selectable rounded'
                        : ''
                    "
                    :contenteditable="
                      recipe.creatorId === account.id ? 'true' : 'false'
                    "
                    @blur="updateIngredientName(ingredient.id)"
                  >
                    {{ ingredient.name }}
                  </span>
                </div>
                <div
                  class="row bottom m-auto"
                  v-if="recipe.creatorId === account.id"
                >
                  <div class="col-12 my-2">
                    <form @submit.prevent="addIngredient(recipe.id)">
                      <div class="input-group">
                        <input
                          type="text"
                          class="form-control"
                          placeholder="Quantity"
                          required
                          aria-label="Quantity"
                          aria-describedby="Quantity"
                          v-model="state.ingredient.quantity"
                        />
                        <input
                          type="text"
                          class="form-control"
                          placeholder="Name"
                          required
                          aria-label="Name"
                          aria-describedby="Name"
                          v-model="state.ingredient.name"
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
import { ingredientsService } from "../services/IngredientsService"
import { Modal } from "bootstrap"
export default {
  props: { recipe: { type: Object } },

  setup() {
    const state = reactive({
      step: {},
      ingredient: {}
    })
    return {
      ingredients: computed(() => AppState.ingredients),
      steps: computed(() => AppState.steps.sort((a, b) => { return a.sequence - b.sequence })),
      account: computed(() => AppState.account),
      state,


      // NOTE I'm not sure having a new function for each data type is the best solution but I want to get it working first (you could pass property as well as Id)

      async updateIngredientQuantity(id) {
        try {
          let quantity = window.event.target.innerText
          let data = { quantity: quantity }
          logger.log(data)
          await ingredientsService.updateIngredient(data, id)
        } catch (error) {
          logger.error(error)
        }
      },

      async updateIngredientName(id) {
        try {
          let name = window.event.target.innerText
          let data = { name: name }
          // logger.log(data)
          await ingredientsService.updateIngredient(data, id)
        } catch (error) {
          logger.error(error)
        }
      },

      async updateStepSequence(id) {
        try {
          let sequence = Number(window.event.target.innerText)
          let data = { sequence: sequence }
          // logger.log(data)
          await stepsService.updateStep(data, id)
        } catch (error) {
          logger.error(error)
        }
      },
      async updateStepBody(id) {
        try {
          let body = window.event.target.innerText
          let data = { body: body }
          // logger.log(data)
          await stepsService.updateStep(data, id)
        } catch (error) {
          logger.error(error)
        }
      },

      async addStep(id) {
        state.step.recipeId = id
        logger.log(state.step)
        try {
          await stepsService.addStep(state.step)
          state.step = {}
        } catch (error) {
          logger.error(error)
        }
      },

      async deleteStep(id) {
        try {
          const yes = await Pop.confirm('Delete your step?')
          if (!yes) { return }

          await stepsService.deleteStep(id)
          Pop.toast("Step deleted", 'success')
        } catch (error) {
          logger.error(error)
        }
      },

      async addIngredient(id) {
        try {
          state.ingredient.recipeId = id
          await ingredientsService.addIngredient(state.ingredient)
          state.ingredient = {}
        } catch (error) {
          logger.error(error)
        }
      },

      async deleteIngredient(id) {
        try {
          const yes = await Pop.confirm('Delete your ingredient?')
          if (!yes) { return }

          await ingredientsService.deleteIngredient(id)
          Pop.toast("Ingredient deleted", 'success')
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
      },

      async deleteRecipe(id) {
        try {

          const yes = await Pop.confirm('Delete your recipe?')
          if (!yes) { return }
          await recipesService.deleteRecipe(id)
          Pop.toast("Recipe deleted", 'success')
          return
        } catch (error) {
          logger.error(error)
        }
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
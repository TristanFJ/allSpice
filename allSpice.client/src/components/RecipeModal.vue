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
    class="modal fade"
    id="recipeModal"
    tabindex="-1"
    aria-labelledby="recipe modal"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="recipe modal">
            {{ recipe.title }} - {{ recipe.id }}
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">{{ recipe.subtitle }}</div>

        <div class="m-1">
          <h5 class="m-1">Ingredients</h5>
          <ul>
            <li v-for="ingredient in ingredients" :key="ingredient.id">
              {{ ingredient.name }}
            </li>
          </ul>
        </div>

        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Close
          </button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  props: { recipe: { type: Object } },

  setup() {

    return { ingredients: computed(() => AppState.ingredients) }
  }
}
</script>


<style lang="scss" scoped>
</style>
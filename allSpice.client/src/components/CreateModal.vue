<template>
  <div class="createModal">
    <div
      class="modal opacity"
      id="createModal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              Share a new recipe:
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <form @submit.prevent="createRecipe()">
            <div class="modal-body">
              <div class="input-group mb-3">
                <input
                  required
                  type="text"
                  class="form-control"
                  id="recipe-title"
                  placeholder="Title"
                  aria-label="Title"
                  maxlength="25"
                  minlength="1"
                  v-model="state.editable.title"
                />
                <div class="dropdown mx-4 my-2">
                  <button
                    class="btn btn-secondary dropdown-toggle"
                    type="button"
                    id="dropdownMenuButton1"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                    required
                  >
                    {{ state.editable.category }}
                  </button>
                  <ul
                    class="dropdown-menu"
                    aria-labelledby="dropdownMenuButton1"
                  >
                    <li v-for="category in categories" :key="category">
                      <div
                        class="dropdown-item selectable"
                        required
                        @click="state.editable.category = category"
                      >
                        {{ category }}
                      </div>
                    </li>
                  </ul>
                </div>
              </div>
              <div class="mb-3">
                <input
                  type="url"
                  class="form-control"
                  id="recipe-image"
                  placeholder="Image Link"
                  required
                  v-model="state.editable.imgUrl"
                />
              </div>
              <div class="mb-3">
                <label for="recipe-subtitle" class="col-form-label"
                  >Description:</label
                >
                <textarea
                  class="form-control"
                  id="recipe-subtitle"
                  maxlength="50"
                  minlength="1"
                  required
                  v-model="state.editable.subtitle"
                ></textarea>
              </div>
            </div>
            <div class="modal-footer">
              <button
                type="submit"
                class="btn btn-primary rounded-pill"
                data-bs-dismiss="modal"
              >
                Share
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, reactive } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import { recipesService } from "../services/RecipesService"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
export default {
  setup() {
    const state = reactive({
      editable: { category: "Pick a category" },
    })
    return {
      categories: computed(() => AppState.categories),
      state,

      async createRecipe() {
        try {
          await recipesService.createRecipe(state.editable)
          state.editable = {}
          Pop.toast("Your recipe has been shared", 'success')
        } catch (error) {
          logger.error(error)
        }
        // logger.log(state.editable)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
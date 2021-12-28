<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 text-center py-3 justify-content-around d-flex">
        <Filter />
      </div>
    </div>
  </div>

  <div class="container">
    <div class="row">
      <div class="col-lg-6" v-for="r in recipes" :key="r.id">
        <Recipe :recipe="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { recipesService } from '../services/RecipesService'
import Pop from "../utils/Pop"
import { AppState } from "../AppState"
export default {
  name: 'Home',
  setup() {
    watchEffect(async () => {
      try {
        await recipesService.getAll()
      } catch (error) {
        logger.log(error)
        Pop.toast(error.message, 'error')
      }
    })

    return {
      recipes: computed(() => AppState.recipes)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>

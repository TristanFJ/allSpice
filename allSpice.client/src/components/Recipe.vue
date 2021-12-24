<template>
  <!-- <div
    class="recipe card elevation-3 m-2 p-3 selectable text-light"
    :style="{ backgroundImage: 'url(' + recipe.imgUrl + ')' }"
  >
    <div class="row">
      <div class="col-md-8">
        <h3>{{ recipe.title }}</h3>
        <p>{{ recipe.subtitle }}</p>
      </div>
      <div class="col text-end">
        <p>{{ recipe.category }}</p>
      </div>
    </div>
  </div> -->

  <div class="card mb-3 recipe elevation-3 m-2 p-3">
    <div class="row g-0">
      <div class="col-4">
        <img
          :src="recipe.imgUrl"
          class="img-fluid rounded-start elevation-2"
          :alt="recipe.title"
        />
      </div>
      <div class="col-8">
        <div class="card-body m-2 p-3 selectable rounded">
          <h4 class="card-title">{{ recipe.title }}</h4>
          <p class="card-text">
            {{ recipe.subtitle }}
          </p>
          <p class="card-text">
            <small class="text-muted">{{ recipe.category }}</small>
          </p>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12 text-end p-0">
        <button
          type="button"
          class="btn btn-primary btn-sm rounded-pill elevation-2"
          @click="favorite(recipe.id)"
        >
          Favorite
        </button>
      </div>
    </div>
  </div>
</template>


<script>
import { logger } from "../utils/Logger"
export default {
  props: { recipe: { type: Object } },
  setup() {
    return {


      favorite(id) {
        logger.log(id)
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
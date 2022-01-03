<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark p-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <h1>All Spice</h1>
    </router-link>
    <p class="m-0 p-2">Cherish your family... And their cooking.</p>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav m-auto">
        <button
          type="button"
          class="btn btn-primary rounded-pill btn-lg elevation-3"
          data-bs-toggle="modal"
          data-bs-target="#createModal"
        >
          Share Your Recipe
        </button>
      </ul>
      <form @submit.prevent="searchRecipes()">
        <ul class="navbar-nav m-auto my-3">
          <div class="input-group mx-5">
            <input
              type="text"
              class="form-control"
              placeholder="Search Recipes"
              aria-label="Search Recipes"
              aria-describedby="button-addon2"
              v-model="search"
            />
            <button
              class="btn btn-outline-secondary"
              type="submit"
              id="button-addon2"
            >
              <i class="mdi mdi-play"></i>
            </button>
          </div>
        </ul>
      </form>
      <span class="navbar-text">
        <button
          class="
            btn
            selectable
            text-success
            lighten-30
            text-uppercase
            my-2 my-lg-0
          "
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <div class="dropdown my-2 my-lg-0" v-else>
          <div
            class="dropdown-toggle selectable"
            data-bs-toggle="dropdown"
            aria-expanded="false"
            id="authDropdown"
          >
            <img
              :src="user.picture"
              alt="user photo"
              height="40"
              class="rounded"
            />
            <span class="mx-3 text-success lighten-30">{{ user.name }}</span>
          </div>
          <div
            class="dropdown-menu p-0 list-group w-100"
            aria-labelledby="authDropdown"
          >
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item list-group-item-action hoverable">
                Manage Account
              </div>
            </router-link>
            <div
              class="
                list-group-item list-group-item-action
                hoverable
                text-danger
              "
              @click="logout"
            >
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </span>
    </div>
  </nav>
  <CreateModal />
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, ref } from 'vue'
import { logger } from "../utils/Logger"
import { recipesService } from "../services/RecipesService"
import Pop from "../utils/Pop"
export default {
  setup() {
    const search = ref('')
    return {
      search,
      user: computed(() => AppState.user),

      async searchRecipes() {
        try {
          await recipesService.searchRecipes(search.value)
          search.value = ''
        } catch (error) {
          logger.error(error)
        }
      },

      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>

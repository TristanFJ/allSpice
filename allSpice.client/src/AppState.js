import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  recipes: [],
  favoriteRecipes: [],
  ingredients: [],
  steps: [],
  categories: ["Fish", "Meat", "Sandwich", "Salad", "Soup", "Pizza"],
  select: 0
})

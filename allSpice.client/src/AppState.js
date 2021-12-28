import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  recipes: [],
  ingredients: [],
  steps: [],
  categories: ["Fish", "Meat", "Sandwich", "Salad", "Soup", "Pizza"]
})

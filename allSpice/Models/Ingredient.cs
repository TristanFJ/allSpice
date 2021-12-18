namespace allSpice.Models
{

  // NOTE I don't know if an ingredient really needs an Id (since it has a RecipeId) but I also don't think it will hurt, and it may be useful down the road?

  public class Ingredient
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Quantity { get; set; }
    public int RecipeId { get; set; }
  }
}
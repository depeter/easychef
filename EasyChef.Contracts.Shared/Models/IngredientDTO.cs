namespace EasyChef.Contracts.Shared.Models
{
    public class IngredientDTO
    {
        public int RecepyId { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public RecepyDTO Recepy { get; set; }
    }
}

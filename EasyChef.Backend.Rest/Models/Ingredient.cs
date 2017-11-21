namespace EasyChef.Backend.Rest.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int RecepyId { get; set; }
        public virtual Recepy Recepy { get; set; }
    }
}

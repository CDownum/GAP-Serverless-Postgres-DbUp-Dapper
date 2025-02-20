
namespace GAP.Core.Domain
{
    public class LookupModel : DomainModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public Guid ModifiedBy { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

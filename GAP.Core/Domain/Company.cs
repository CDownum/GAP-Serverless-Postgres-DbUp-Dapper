
namespace GAP.Core.Domain
{
    public class Company : IntLookupModel
    {
        public DateTime LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enabled { get; set; }
    }
}

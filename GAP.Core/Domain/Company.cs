
namespace GAP.Core.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enabled { get; set; }
    }
}

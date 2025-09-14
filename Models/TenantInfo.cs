using Finbuckle.MultiTenant.Abstractions;  


namespace Backend.Models
{
    public class TenantInfo : ITenantInfo
    {
        public string? Id { get; set; }
        public string? Identifier { get; set; }
        public string? Name { get; set; }
        public string? ConnectionString { get; set; }
        public string? Items { get; set; }
    }
}

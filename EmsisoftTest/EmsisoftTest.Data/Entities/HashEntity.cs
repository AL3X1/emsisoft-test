using EmsisoftTest.Data.Interfaces;

namespace EmsisoftTest.Data.Entities;

public class HashEntity : IEntityBase
{
    public long Id { get; set; }
    
    public string Hash { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public bool IsDeleted { get; set; }
}
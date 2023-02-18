namespace EmsisoftTest.Data.Interfaces;

public interface IEntityBase
{
    long Id { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }

    bool IsDeleted { get; set; }
}
namespace Qel.Medit.Dal.Entities.Common;

public abstract class Entity<T> where T : struct
{
    /// <summary>
    /// Возвращает или создает уникальный идентификатор сущности.
    /// </summary>
    public T Id { get; set; }
}

namespace Qel.Medit.Dal.Entities.Common;

/// <summary>
/// Интерфейс с полями создания и изменения сущности
/// </summary>
public interface ICreateAndModifyProperties
{
    /// <summary>
    /// Возвращает или задает дату создания сущности.
    /// </summary>
    public DateTime? CreationDateTime { get; set; }

    /// <summary>
    /// Возвращает или задает дата изменения сущности.
    /// </summary>
    public DateTime? ModifyDateTime { get; set; }
}

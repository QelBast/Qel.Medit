namespace Qel.Medit.Dal.Entities.Common;

/// <summary>
/// Интерфейс добавляет сущность поведение "мягкого удаления"
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Возвращает или задает флаг пометки удаления.
    /// </summary>
    public bool IsDeleted { get; set; }
}

using Qel.Medit.Dal.Entities.Common;

namespace Qel.Medit.Dal.Entities;

/// <summary>
/// Представляет сущность файла, создаваемого пользователем
/// </summary>
public class User : BaseGuidEntity, ICreateAndModifyProperties, ISoftDelete
{
    /// <summary>
    ///  Возвращает или задаёт логин пользователя
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Возвращает или задаёт текст, по которому составлялась схема
    /// </summary>
    public required string PasswordHash { get; set; }

    ///<inheritdoc/>
    public DateTime? CreationDateTime { get; set; }

    ///<inheritdoc/>
    public DateTime? ModifyDateTime { get; set; }

    ///<inheritdoc/>
    public bool IsDeleted { get; set; }
}

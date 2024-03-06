namespace WembleyScada.Domain.Exceptions;
public class ChildEntityDuplicationException : DomainException
{
    public ChildEntityDuplicationException(object childEntityId, object childEntity, object parentEntityId, object parentEntity) : base($"Child entity of type {childEntity.GetType()} with ID {childEntityId} already existed in parent entity of type {parentEntity.GetType()} with ID {parentEntityId}.")
    {
        ChildEntityId = childEntityId;
        ChildEntity = childEntity;
        ParentEntityId = parentEntityId;
        ParentEntity = parentEntity;
    }

    public ChildEntityDuplicationException(object childEntityId, object childEntity, object parentEntityId, object parentEntity, Exception? innerException): base($"Child entity of type {childEntity.GetType()} with ID {childEntityId} already existed in parent entity of type {parentEntity.GetType()} with ID {parentEntityId}.", innerException)
    {
        ChildEntityId = childEntityId;
        ChildEntity = childEntity;
        ParentEntityId = parentEntityId;
        ParentEntity = parentEntity;
    }

    public object ChildEntityId { get; set; }
    public object ChildEntity { get; set; }
    public object ParentEntity { get; set; }
    public object ParentEntityId { get; set; }
}

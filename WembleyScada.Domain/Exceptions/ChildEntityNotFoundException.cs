namespace WembleyScada.Domain.Exceptions;
public class ChildEntityNotFoundException: DomainException
{
    public object ChildEntityId { get; set; }
    public Type ChildEntityType { get; set; }
    public object ParentEntity { get; set; }
    public object ParentEntityId { get; set; }

    public ChildEntityNotFoundException(object childEntityId,
                                        Type childEntityType,
                                        object parentEntityId,
                                        object parentEntity) : 
        base($"Child entity of type {childEntityType.GetType()} with ID {childEntityId} was not found in parent entity of type {parentEntity.GetType()} with ID {parentEntityId}.")
    {
        ChildEntityId = childEntityId;
        ChildEntityType = childEntityType;
        ParentEntityId = parentEntityId;
        ParentEntity = parentEntity;
    }

    public ChildEntityNotFoundException(object childEntityId,
                                        Type childEntityType,
                                        object parentEntityId,
                                        object parentEntity,
                                        Exception? innerException) : 
        base($"Child entity of type {childEntityType.GetType()} with ID {childEntityId} was not found in parent entity of type {parentEntity.GetType()} with ID {parentEntityId}.", innerException)
    {
        ChildEntityId = childEntityId;
        ChildEntityType = childEntityType;
        ParentEntityId = parentEntityId;
        ParentEntity = parentEntity;
    }
}

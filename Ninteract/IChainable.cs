namespace NInteract
{
    public interface IChainable<TCollaborator> where TCollaborator : class
    {
        IVerifiable<TCollaborator> And();
    }
}
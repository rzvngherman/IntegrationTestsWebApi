namespace WebApplication1.Domain.Repository
{
    public interface IAttachmentRepository
    {
        Attachment Insert();
        Attachment InsertFail();
    }
}

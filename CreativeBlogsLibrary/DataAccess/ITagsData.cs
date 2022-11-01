namespace CreativeBlogsLibrary.DataAccess;

public interface ITagsData
{
    Task CreateTags(TagModel tags);
    Task<List<TagModel>> GetAllTags();
}
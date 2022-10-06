namespace CreativeBlogsLibrary.Models;
public class TagModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string TagName { get; set; }

    public string TagDescription { get; set; }

}

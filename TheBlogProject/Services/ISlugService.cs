namespace TheBlogProject.Services
{
    public interface ISlugService
    {
        public string UrlFriendly(string title);
        bool IsUnique(string slug);
    }
}
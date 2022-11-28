namespace Roulette.Licence
{
    public class LicenceEntity
    {
        public LicenceEntity(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; }
        public string Content { get; }
    }
}
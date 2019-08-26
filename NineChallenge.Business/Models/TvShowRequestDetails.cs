namespace NineChallenge.Business.Models
{
    public class TvShowRequestDetails
    {
        public bool Drm { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public int EpisodeCount { get; set; }
        public ImageDetails Image { get; set; }
    }
}

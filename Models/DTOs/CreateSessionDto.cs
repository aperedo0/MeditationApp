namespace MeditationApp.Dtos
{
    public class CreateSessionDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string AudioUrl { get; set; }
        public required int Duration { get; set; }
        public required string UserId { get; set; }  // Keep only required fields, no ID!
    }
}
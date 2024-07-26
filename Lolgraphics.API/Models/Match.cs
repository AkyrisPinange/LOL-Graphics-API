using System.ComponentModel.DataAnnotations;

namespace Adapter.Api.Models
{
    public class Match
    {
        public int Id { get; set; }
        public required string MatcheId { get; set; }
    }
}

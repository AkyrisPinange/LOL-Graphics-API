using Lolgraphics.Core.addMatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MatchService
    {
        private readonly IMatchesRepository matchesRepository;

        public MatchService(IMatchesRepository matchesRepository)
        {
            this.matchesRepository = matchesRepository;
        }

        public void AddMatches (List<Match> matches)
        {
            matchesRepository.AddMatchesAsync (matches);
        }

    }
}

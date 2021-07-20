using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotesRestApi.Core.DTOs;
using VotesRestApi.Core.Enums;
using VotesRestApi.Repositories.Configure;
using VotesRestApi.Repositories.Interfaces;
using VotesRestApi.Service.Interfaces;

namespace VotesRestApi.Service
{
    public class ReportService : IReportServices
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IUserRepository _userRepository;

        public ReportService(
            IVoteRepository voteRepository,
            IUserRepository userRepository
        )
        {
            _voteRepository = voteRepository;
            _userRepository = userRepository;
        }

        public async Task<ReportDto> GetReport(Guid adminId, DateTime period)
        {
            var admin = await _userRepository.GetByIdAsync(adminId);

            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("You don't have admin permissions.");
            }

            var users = _userRepository.GetAll();
            var votes = _voteRepository.GetAllVotesDto();

            votes = votes.Where(x => x.Date.Year == period.Year
                                        && x.Date.Month == period.Month)
                        .ToList();

            var mostVotedEmployee = votes.GroupBy(x => x.VotedUser.Id)
                                         .Select(mv => new 
                                         {
                                             Count = mv.Count(),
                                             Name = mv.First().VotedUser.Name,
                                             Period = mv.First().Date.Date,
                                             ID = mv.Key
                                         })
                                         .OrderByDescending(x => x.Count)
                                         .FirstOrDefault();

            ReportDto report = new ReportDto()
            {
                MostVotedPlayer = new MostVotedPlayerDto(mostVotedEmployee.Name, mostVotedEmployee.Count),
                Period = mostVotedEmployee.Period,
                RegisteredEmployeeCount = users.Count(x => !x.IsAdmin)
            };
        
            var votedEmployeePerNomination = votes.GroupBy(x => new { x.VotedUser.Id, x.Nomination })
                                                  .Select(mv => new
                                                  {
                                                      Count = mv.Count(),
                                                      Name = mv.First().VotedUser.Name,
                                                      Nomination = mv.First().Nomination,
                                                      NominationDesc = mv.First().NominationDescription
                                                  })
                                                  .OrderByDescending(x => x.Count)
                                                  .ToList();
        
            report.MostVotedEmployeeForNomination = new Dictionary<string, string>();
        
            foreach (var item in Enum.GetValues(typeof(Nomination)))
            {
                Nomination nomination = (Nomination)item;
        
                var mostVoted = votedEmployeePerNomination.FirstOrDefault(x => x.Nomination == nomination);
                
                if(mostVoted != null)
                {
                    report.MostVotedEmployeeForNomination.Add(mostVoted.Name, mostVoted.NominationDesc);
                }
            }

            return report;
        }
    }
}

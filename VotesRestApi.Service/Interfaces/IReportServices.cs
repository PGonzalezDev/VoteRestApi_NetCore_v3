using System;
using System.Threading.Tasks;
using VotesRestApi.Core.DTOs;

namespace VotesRestApi.Service.Interfaces
{
    public interface IReportServices
    {
        Task<ReportDto> GetReport(Guid adminId, DateTime period);
    }
}

using Core.Entities.Concretes;
using Core.Exceptions;
using Core.Logging;
using DataAccess.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessRules;

public class LogBusinessRules(ILogRepository logRepository)
{
    public Log LogMustBeExist(Guid logId)
    {
        var log = logRepository.GetById(logId);
        if (log is null)
        {
            throw new BusinessException("Log not found.", StatusCodes.Status404NotFound);
        }

        return log;
    }
}
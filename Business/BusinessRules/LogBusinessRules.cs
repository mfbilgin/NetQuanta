using Business.Constants.Messages;
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
            throw new BusinessException(LogMessages.LogNotFound, StatusCodes.Status404NotFound,logId.ToString());
        }

        return log;
    }
}
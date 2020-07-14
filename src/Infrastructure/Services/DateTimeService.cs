using CleanArchiTemplate.Application.Common.Interfaces;
using System;

namespace CleanArchiTemplate.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

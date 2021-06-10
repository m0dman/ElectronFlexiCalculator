using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RandoxITUtility.API.Business.Interfaces;
using RandoxITUtility.Domain.Entities;

namespace  RandoxITUtility.API.Business.Interfaces
{
    public interface ITimeManagementManager
    {
        /// <summary>
        /// Calculates flexi times.
        /// </summary>
        /// <returns></returns>
        TimeResults CalculateFlexiTime(List<DaysOfTheWeek> daysOfTheWeek);

        // Task<IEnumerable<DaysOfTheWeek>> GetDaysOfTheWeekByCommencingDate(DateTime commencingDate);
        // Task<IEnumerable<TimeData>> SaveOrUpdateTimeData(IEnumerable<TimeData> insertData, DateTime dateCommencing);
    }
}
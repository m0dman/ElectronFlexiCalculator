using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RandoxITUtility.API.Business.Interfaces;
using RandoxITUtility.Domain.Entities;
using System.Linq;

namespace RandoxITUtility.API.Business
{
    public class TimeManagementManager : ITimeManagementManager
    {
        private readonly ILogger _Logger;

        public TimeManagementManager(ILogger<TimeManagementManager> logger)
        {
            _Logger = logger;
        }

        public TimeResults CalculateFlexiTime(List<DaysOfTheWeek> daysOfTheWeek)
        {

            double totalMinutes = 0;
            double workingWeek = 2400;

            foreach(var d in daysOfTheWeek){
                
                TimeSpan value = d.EndTime.Subtract(d.StartTime);

                totalMinutes += value.TotalMinutes;

                int lunchTime = d.LunchLength.Minute * -1;

                totalMinutes += lunchTime;
            }

            double totalFlexiMinutes =  totalMinutes - workingWeek;

            TimeSpan totalFlexi = TimeSpan.FromMinutes(totalFlexiMinutes);

            DateTime workDayEndTime = new DateTime(2020, 09, 03, 17, 20, 00);
            DateTime can447Train = new DateTime(2020, 09, 03, 16, 30, 00);
            DateTime can347Train = new DateTime(2020, 09, 03, 15, 30, 00);
            DateTime can247Train = new DateTime(2020, 09, 03, 14, 30, 00);
            DateTime earliestCanLeave = workDayEndTime.Subtract(totalFlexi);

            string TotalFlexiAmountHours = totalFlexi.Hours.ToString();
            string TotalFlexiAmountMinutes = totalFlexi.Minutes.ToString();
            string TotalFlexiAmount = $"{TotalFlexiAmountHours} hour(s) {TotalFlexiAmountMinutes} minutes";

            TimeResults endResults = new TimeResults();

            endResults.totalAmountOfFlexi = TotalFlexiAmount;
            endResults.earliestCanLeave = earliestCanLeave;
            

            if (earliestCanLeave <= can447Train)
            {
                endResults.can447Train = true;
            }
            if (earliestCanLeave <= can347Train)
            {
                endResults.can347Train = true;
            }
            if (earliestCanLeave <= can247Train)
            {
                endResults.can247Train = true;
            }
            
            return endResults;
        }

        // public async Task<IEnumerable<DaysOfTheWeek>> GetDaysOfTheWeekByCommencingDate(DateTime commencingDate)
        // {
        //     return await _TimeManagementRepository.GetDaysOfTheWeekByCommencingDateAsync(commencingDate);
        // }

        // public async Task<IEnumerable<TimeData>> SaveOrUpdateTimeData(IEnumerable<TimeData> insertData, DateTime dateCommencing)
        // {
        //     var dateCommenceResponse = await _TimeManagementRepository.GetDaysOfTheWeekByCommencingDateAsync(dateCommencing);

        //     if(dateCommenceResponse.Any())
        //     {
        //         return await _TimeManagementRepository.UpdateTimeDataAsync(insertData, dateCommencing);
        //     }

        //     return await _TimeManagementRepository.SaveTimeDataAsync(insertData, dateCommencing);
        // }
    }
}
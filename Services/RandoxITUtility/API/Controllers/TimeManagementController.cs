using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandoxITUtility.API.Business.Interfaces;
using RandoxITUtility.Domain.Entities;
using RandoxITUtility.Application.Queries.GetDaysOfTheWeek;
using RandoxITUtility.Application.Queries;
using RandoxITUtility.Utilities;
using MediatR;
using RandoxITUtility.API.Controllers;

namespace RandoxITUtilitiesAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TimeManagementController : ApiController
    {
        private readonly ITimeManagementManager _TimeManagementManager;
        private readonly ILogger _Logger;
        public TimeManagementController(ITimeManagementManager timeManagementManager, ILogger<TimeManagementController> logger)
        {
            _TimeManagementManager = timeManagementManager;
            _Logger = logger;
        }

        /// <summary>
        /// Calculates the flexi time using a list of days passed in
        /// </summary>
        /// <returns>Amount of flexi time, earliest can leave and if can make each train</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CalculateFlexiTimes([FromBody]List<DaysOfTheWeek> insertData)
        {

            TimeResults flexiResults = _TimeManagementManager.CalculateFlexiTime(insertData);

            if (insertData != null){
                return Ok(flexiResults);
            }

            return BadRequest($"Time was not recieved");
        }

        /// <summary>
        /// Gets an Analyser using the info provided with top level information.
        /// </summary>
        /// <param name="id">The id of the Analyser that will be retrieved from the database.</param>
        /// <returns>The Analyser information that was retrieved.</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDaysOfTheWeekByCommencingDate(DateTime commencingDate)
        {
            _Logger.LogInformation($"Days Of The Week: {HelperMethods.GetCallerMemberName()}");
            List<TimeDataDTO> result = await Mediator.Send(new GetDaysOfTheWeekQuery(commencingDate));

            if (result != null)
                return Ok(result);

            return StatusCode(204, "No Days found.");
        }

        /// <summary>
        /// Calculates the flexi time using a list of days passed in
        /// </summary>
        /// <returns>Amount of flexi time, earliest can leave and if can make each train</returns>
        // [HttpPost]
        // [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        // public async Task<IActionResult> SaveOrUpdateTimeData(DateTime dateCommencing, [FromBody]List<TimeData> insertData)
        // {
        //     List<TimeDataDTO> result = await Mediator.Send(new GetDaysOfTheWeekQuery(commencingDate));

        //     if (result)
        //     IEnumerable<TimeData> response = await Mediator.Send(new GetDaysOfTheWeekQuery(commencingDate));

        //     if (response.Any()){
        //         return Ok(response);
        //     }

        //     return BadRequest($"Time was not updated/saved");
        // }
    }
}
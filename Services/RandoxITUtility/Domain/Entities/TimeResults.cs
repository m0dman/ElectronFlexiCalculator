using System;
using RandoxITUtility.Domain;

namespace RandoxITUtility.Domain.Entities
{
    public class TimeResults
    {
        public string totalAmountOfFlexi { get; set; } 
        public DateTimeOffset earliestCanLeave { get; set; }
        public bool can247Train { get; set; }
        public bool can347Train { get; set; }
        public bool can447Train { get; set; }
    }
}

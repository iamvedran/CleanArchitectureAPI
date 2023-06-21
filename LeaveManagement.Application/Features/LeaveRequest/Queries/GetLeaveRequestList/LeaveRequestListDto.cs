using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class LeaveRequestListDto
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <example>1</example>
        public string RequestindEmployeeId { get; set; }
        /// <summary>
        /// Leave type
        /// </summary>        /// <summary>
        /// Leave type
        /// </summary>
        public LeaveTypeDto LeaveType { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        /// <example>2023-01-12</example>
        public DateTime DateRequested { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        /// <example>2023-01-15</example>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        /// <example>2023-01-16</example>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Is approved?
        /// </summary>
        /// <example>true</example>
        public bool? Approved { get; set; }

        
    }
}

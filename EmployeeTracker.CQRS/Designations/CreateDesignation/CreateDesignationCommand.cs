﻿using MediatR;

namespace EmployeeTracker.CQRS.Designations.CreateDesignation
{
    public class CreateDesignationCommand : IRequest
    {
        public required string DesignationName { get; set; }
        public string? Description { get; set; }
    }
}

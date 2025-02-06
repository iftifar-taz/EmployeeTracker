﻿using Identity.Application.Dtos;
using MediatR;

namespace Identity.Application.Features.Sessions.Commands
{
    public class CreateSessionCommand : IRequest<SessionResponseDto>
    {
        public required string Email { get; set; }
        public required string PasswordRaw { get; set; }
    }
}

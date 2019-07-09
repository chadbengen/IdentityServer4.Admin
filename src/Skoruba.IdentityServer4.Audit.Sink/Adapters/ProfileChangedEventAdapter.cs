﻿using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Skoruba.IdentityServer4.Audit.Sink.Events;
using System;

namespace Skoruba.IdentityServer4.Audit.Sink.Adapters
{
    public class ProfileChangedEventAdapter : IAuditArgs
    {
        private readonly IHttpContextAccessor _httpContext;

        public ProfileChangedEvent Event { get; }

        public ProfileChangedEventAdapter(ProfileChangedEvent evt, IHttpContextAccessor httpContext)
        {
            Event = evt ?? throw new ArgumentNullException(nameof(evt));
            _httpContext = httpContext;
        }

        public string EventName => Event.Name;
        public string EventId => Event.Id.ToString();
        public EventDetail EventDetail => new EventDetail(Event);
        public AuditArgResource Source => new AuditArgResource(null, null, AuditArgResource.AppType);
        public AuditArgResource Actor => new AuditArgResource(_httpContext.HttpContext.User.GetSubjectId(), _httpContext.HttpContext.User.GetDisplayName(), AuditArgResource.UserType);
        public AuditArgResource Subject => new AuditArgResource(Event.SubjectId, Event.SubjectName, AuditArgResource.UserType);
        public string Changes => Event.Changes;
    }
}
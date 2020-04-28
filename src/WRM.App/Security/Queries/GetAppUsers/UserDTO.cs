using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WRM.App.Mappings;
using WRM.Domain.Entities;

namespace WRM.App.Security.Queries.GetAppUsers
{
    public class UserDTO : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string PhoneNumber { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserDTO>();
        }
    }
}

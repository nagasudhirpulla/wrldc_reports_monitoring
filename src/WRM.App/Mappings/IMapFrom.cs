﻿using AutoMapper;
using System.Collections.Generic;
using System.Text;

namespace WRM.App.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
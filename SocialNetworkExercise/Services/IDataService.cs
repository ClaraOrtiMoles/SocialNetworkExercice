﻿using SocialNetworkExercise.Models;
using System.Collections.Generic;

namespace SocialNetworkExercise.Services
{
    public interface IDataService
    {
        User CreateUser(string userName, Dictionary<string, User> data);
        User GetUser(string userName, Dictionary<string, User> data);
    }
}

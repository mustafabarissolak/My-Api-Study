﻿using City.Api.Core;

namespace City.Api.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(UserEntity user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}

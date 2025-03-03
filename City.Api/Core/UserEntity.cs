﻿namespace City.Api.Core
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<CityEntity>? CityEntities { get; set; }
    }
}

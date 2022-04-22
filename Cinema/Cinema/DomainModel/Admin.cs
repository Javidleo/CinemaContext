﻿using System;

namespace DomainModel
{
    public class Admin
    {
        public int Id { get; private set; }

        public int CinemaId { get; private set; }

        public string Name { get; private set; }

        public string Family { get; private set; }

        public string NationalCode { get; private set; }

        public string Email { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public Guid AdminGuid { get; private set; }

        public virtual Cinema Cinema { get; private set; }

        Admin(int cinemaid, string name, string family, string nationalCode, string email,string userName, string password)
        {
            this.CinemaId = cinemaid;
            this.Name = name;
            this.Family = family;
            this.NationalCode = nationalCode;
            this.Email = email;
            this.UserName = userName;
            this.Password = password;
        }

        public static Admin Create(int cinemaId, string name, string family, string nationalCode, string email,string userName, string password)
        => new(cinemaId, name, family, nationalCode, email,userName, password);
    }
}

using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Salon
    {
        public int Id { get; private set; }

        public int CinemaId { get; private set; }

        public string Name { get; private set; }

        public bool IsDisabled { get; private set; }

        public int Capacity { get; private set; }

        public Guid SalonGuid { get; private set; }

        public virtual Cinema Cinema { get; private set; }

        public virtual List<Chair> Chairs { get; private set; } = new List<Chair>();

        public virtual List<SalonActivity> SalonActivities { get; private set; } = new List<SalonActivity>();

        public virtual List<Ticket> Tickets { get; private set; } = new List<Ticket>();

        public virtual List<MovieSansSalon> MovieSansSalons { get; private set; } = new List<MovieSansSalon>();

        public Salon() { }

        Salon(int cinemaId, string name, int capacity)
        {
            CinemaId = cinemaId;
            Name = name;
            Capacity = capacity;
            IsDisabled = false;
        }

        public static Salon Create(int cinemaId, string name, int capacity)
        => new(cinemaId, name, capacity);
    }
}

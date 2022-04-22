using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Cinema
    {
        public int Id { get; private set; }

        public int CityId { get; private set; }

        public string Name { get; private set; }

        public decimal TicketPrice { get; private set; }

        public bool IsDisabled { get; private set; }

        public string Address { get; private set; }

        public Guid CinemaGuid { get; private set; }

        public virtual List<Salon> Salons { get; private set; } = new List<Salon>();

        public virtual List<CinemaActivity> CinemaActivities { get; private set; } = new List<CinemaActivity>();

        public virtual List<Ticket> Tickets { get; private set; }= new List<Ticket>();

        public virtual City City { get; private set; }

        public virtual List<Admin> Admins { get; private set; } = new List<Admin>();

        public Cinema() { }
        private Cinema(string name, decimal ticketPrice, string address, int cityId) 
        {
            Name = name;
            TicketPrice = ticketPrice;
            Address = address;
            CityId = cityId;
            IsDisabled = false;
        }

        public static Cinema Create(string name, decimal ticketPrice, string address, int cityId)
        => new(name, ticketPrice, address, cityId);
    }
}

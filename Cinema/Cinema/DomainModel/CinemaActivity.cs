using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class CinemaActivity
    {
        public int Id { get; private set; }

        public int CinemaId { get; private set; }

        public DateTime StartDate { get; private set; }

        public string StartDatePersian { get; private set; }

        public DateTime EndDate { get; private set; }

        public string EndDatePersian { get; private set;}

        public string Description { get; private set; }

        public Guid AdminGuid { get; private set; }

        public string AdminFullName { get; private set; }

        public Guid CinemaActivityGuid { get; private set; }

        public virtual Cinema Cinema { get; private set; }

        private CinemaActivity() { }

        public static CinemaActivity Create()
        {
            throw new NotImplementedException();
        }
    }
}

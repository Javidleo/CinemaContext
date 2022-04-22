using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class ChairActivity
    {
        public int Id { get; private set; }

        public int ChairId { get; private set; }

        public DateTime StartDate { get; private set; }

        public string StartDatePersian { get; private set; }

        public DateTime EndDate { get; private set; }

        public string EndDatePersian { get; private set;}

        public string Desicription { get; private set; }

        public Guid ChairActivityGuid { get; private set; }

        public Guid AdminGuid { get; private set; }

        public string AdminFullName { get; private set; }

        public virtual Chair Chair { get; private set; }
        private ChairActivity() { }

        public static ChairActivity Create()
        {
            throw new NotImplementedException();
        }
    }
}

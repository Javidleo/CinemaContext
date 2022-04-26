using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public class Chair
    {
        public int Id { get; private set; }

        public int SalonId { get; private set; }

        public byte Number { get; private set; }

        public byte Row { get; private set; }

        public bool InUse { get; private set; }

        public bool IsDisabled { get; private set; }

        public Guid ChairGuid { get; private set; }

        public virtual Salon Salon { get; private set; }

        public virtual List<ChairActivity> ChairActivities { get; private set; } = new List<ChairActivity>();

        public virtual List<Ticket> Tickets { get; private set; } = new List<Ticket>();

        public Chair() { }

        private Chair(int salonId, byte number, byte row)
        {
            SalonId = salonId;
            Number = number;
            Row = row;
        }

        public static Chair Create(int salonId, byte number, byte row)
        => new(salonId, number, row);

        public void Disable()
        => IsDisabled = true;

        public void MakeInUse()
        => InUse = true;
    }
}

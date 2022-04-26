using System;

namespace DomainModel.Domain
{
    public class MovieActores
    {
        public int Id { get; private set; }

        public int MovieId { get; private set; }

        public string BaseMaleActorName { get; private set; }

        public string BaseFemaleActorName { get; private set; }

        public string SupportedMaleActorName { get; private set; }

        public string SupportedFemaleActorName { get; private set; }

        public Guid MovieActorGuid { get; private set; }

        public Guid AdminGuid { get; private set; }

        public string AdminFullName { get; private set; }

        public virtual Movie Movie { get; private set; }

        MovieActores() { }
        private MovieActores(string baseMaleActorName, string baseFemaleActorName, string supportedMaleActorName,
                                 string supportedFemaleActorName, Guid adminGuid, string adminFullName)
        {
            BaseMaleActorName = baseMaleActorName;
            BaseFemaleActorName = baseFemaleActorName;
            SupportedMaleActorName = supportedMaleActorName;
            SupportedFemaleActorName = supportedFemaleActorName;
            AdminGuid = adminGuid;
            AdminFullName = adminFullName;
        }

        public static MovieActores Create(string baseMaleActorName, string baseFemaleActorName, string supportedMaleActorName,
                                         string supportedFemaleActorName,Guid adminGuid, string adminFullName)
        => new(baseMaleActorName, baseFemaleActorName, supportedMaleActorName, supportedFemaleActorName,adminGuid,adminFullName);
    }
}

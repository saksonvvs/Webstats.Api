using System;

namespace Swimlane.Webstats.Dto
{
    public abstract class AbstractDto
    {
        public Guid Uid { get; set; }
        public DateTime DteCreated { get; set; }

        public Guid OwnerUid { get; set; }

        public AbstractDto()
        {
            Uid = new Guid();
            DteCreated = new DateTime();
            OwnerUid = new Guid();
        }




    }
}

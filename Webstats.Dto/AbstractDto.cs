using System;

namespace Webstats.Dto
{
    public abstract class AbstractDto
    {
        public Guid Uid { get; set; }
        public DateTime DteCreated { get; set; }
        public Guid OwnerUid { get; set; }

        public AbstractDto()
        {
            Uid = new Guid();
            DteCreated = DateTime.Now;
            OwnerUid = new Guid();
        }




    }
}

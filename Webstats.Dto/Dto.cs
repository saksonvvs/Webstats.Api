using System;

namespace Webstats.Dto
{
    public class Dto
    {
        public Guid Uid { get; set; }
        public DateTime DteCreated { get; set; }
        public Guid OwnerUid { get; set; }

        public Dto()
        {
            Uid = new Guid();
            DteCreated = DateTime.Now;
            OwnerUid = new Guid();
        }




    }
}

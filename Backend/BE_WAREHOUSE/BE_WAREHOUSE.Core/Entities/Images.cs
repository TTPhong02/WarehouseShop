using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class Images
    {
        public Guid ImagesId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UsersId { get; set; }
        public string ImagesPath { get; set; }
    }
}

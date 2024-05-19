using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Model.Excel
{
    public class ExcelRequest<T>
    {
        public List<Guid>? EntityIds { get; set; }
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string TitleHeader { get; set; }
        public List<string>? Columns { get; set; }
        public string WorksheetName { get; set; }
        public List<T>? Data { get; set; }
    }
}

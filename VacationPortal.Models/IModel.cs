using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationPortal.Models
{
    public interface IModel
    {
        public int Id { get; set; }
        public ModelStatus ModelStatus { get; set; }
    }
}

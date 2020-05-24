using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreinbeheersysteemMain.Models
{
    public class VerbindingViewModel
    {
        public List<VerbindingDetailViewModel> Verbindingen { get { Verbindingen.OrderBy(v => v.VertrekTijd); return Verbindingen; } set { } }
    }
}

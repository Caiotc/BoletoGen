using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoGen2
{
    public interface IBoletoGenService
    {
        string BoletoGenGet(Guid InstitutionId, Guid userId);
    }
}

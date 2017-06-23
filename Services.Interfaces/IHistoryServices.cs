using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IHistoryServices
    {
        string createHistory(HistoryEntity historyEntity);
        IEnumerable<HistoryEntity> allHistory();
    }
}

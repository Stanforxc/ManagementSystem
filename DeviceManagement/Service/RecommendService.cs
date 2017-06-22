using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud;
using EntityModel;

namespace Service
{
    public class RecommendService
    {
        private HistoryCrudOperator hisCrudOp = new HistoryCrudOperator();

        public List<int> recommend(string type) {

            return hisCrudOp.bestOfType_1(type);

        }

    }
}

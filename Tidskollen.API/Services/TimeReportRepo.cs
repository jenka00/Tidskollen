using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Model;
using Tidskollen.Models;

namespace Tidskollen.API.Services
{
    public class TimeReportRepo : ITidskollen<TimeReport>
    {
        private TidDbContext _tidContext;

        public TimeReportRepo(TidDbContext tidContext)
        {
            _tidContext = tidContext;
        }
        public Task<TimeReport> Add(TimeReport newEntity)
        {
            throw new NotImplementedException();
        }

        public Task<TimeReport> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TimeReport>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TimeReport> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TimeReport> Update(TimeReport Entity)
        {
            throw new NotImplementedException();
        }
    }
}

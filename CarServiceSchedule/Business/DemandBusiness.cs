using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using CarServiceSchedule.Repositories;

namespace CarServiceSchedule.Business
{
    public class DemandBusiness : IDemandBusiness
    {
        public IDemandRepository _demandRepository;
        public DemandBusiness(IDemandRepository demandRepository)
        {
            _demandRepository = demandRepository;

        }

        public Task<Demand> CreateDemand(Demand demand)
        {
            if(demand.UserId.Equals(0) || demand.UserId ==0){
                return null;
            }
            return _demandRepository.Create(demand);
        }

        public Task<IEnumerable<Demand>> GetDemands()
        {
            var result = _demandRepository.Get();
            return result;
        }
    }
}
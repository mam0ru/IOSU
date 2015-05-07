using System;
using iosu.Interfaces.DAO;

namespace iosu.DAO.SearchParameters
{
    public class OrderSearchParameters : IBaseSearchParameters
    {
        public long? Amount { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public long? PartnerId { get; set; }
    }
}
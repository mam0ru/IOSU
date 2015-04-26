using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iosu.Interfaces.DAO;

namespace iosu.DAO.SearchParameters
{
    public class OrderSearchParameters : IBaseSearchParameters
    {
        public long? Amount { get; set; }
    }
}
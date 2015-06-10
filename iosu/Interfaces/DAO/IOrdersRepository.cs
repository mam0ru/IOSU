using System;
using System.Collections.Generic;
using iosu.Entities;

namespace iosu.Interfaces.DAO
{
    public interface IOrdersRepository: IRepository<Order>
    {
        void SendToArchive(long partnerId, string tableName, bool isNewArchive);
        
        void RestoreFromArchive(string archiveTableName);

        IList<Order> GetArchivedOrders(string tableName);
    }
}

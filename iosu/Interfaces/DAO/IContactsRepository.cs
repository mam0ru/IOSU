﻿using iosu.Entities;

namespace iosu.Interfaces.DAO
{
    public interface IContactsRepository: IRepository<Contact>
    {
        void Evict(Contact contact);
    }
}
﻿using iosu.Entities;
using iosu.Interfaces.DAO;

namespace iosu.DAO
{
    public class ContactsRepository: GenericRepository<Contact>, IContactsRepository
    {
    }
}
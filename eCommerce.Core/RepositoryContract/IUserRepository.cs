﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.RepositoryContract;

public interface IUserRepository
{
    Task<ApplicationUser> AddUser(ApplicationUser user);
    Task<ApplicationUser> GetUserByEmailAndPassword(string email, string password);
}


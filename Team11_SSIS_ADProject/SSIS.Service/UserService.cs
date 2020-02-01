﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.Models;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Repository;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class UserService : IUserService
    {
        UserRepository userContext;

        public UserService()
        {
            this.userContext = new UserRepository();
        }

        public bool FindIfUserExist(string email)
        {
            return userContext.FindIfUserExist(email);
        }

        public ApplicationUser FindUserByEmail(string email)
        {
            return userContext.FindUserByEmail(email);
        }
    }
}
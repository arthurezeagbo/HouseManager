﻿using Data.Model;
using Service.DTO_s;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAdmin
    {
        Task<IEnumerable<CreateUserDTO>> GetAllAdmin();

        Task<CreateUserDTO> GetAdminById(string id);
        
        Task<IEnumerable> GetAllGuarantor();

        Task<GuarantorDTO> GetGuarantorById(int id);

        Task<IEnumerable> GetAllEmployer();

        Task<EmployerDTO> GetEmployerById(int id);

        Task<IEnumerable> GetAllHelper();

        Task GetHelperById(int id);

        Task<bool> DisableUser(string userId);

        Task<bool> EnableUser(string userId);
    }
}
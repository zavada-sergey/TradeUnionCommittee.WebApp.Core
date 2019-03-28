﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    public class PrivilegeEmployeesService : IPrivilegeEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public PrivilegeEmployeesService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<PrivilegeEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
                var privilege = await _context.PrivilegeEmployees
                    .Include(x => x.IdPrivilegesNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<PrivilegeEmployeesDTO>(privilege);
                return new ActualResult<PrivilegeEmployeesDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<PrivilegeEmployeesDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(PrivilegeEmployeesDTO dto)
        {
            try
            {
                dto.CheckPrivileges = true;
                await _context.PrivilegeEmployees.AddAsync(_mapperService.Mapper.Map<PrivilegeEmployees>(dto));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(PrivilegeEmployeesDTO dto)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<PrivilegeEmployees>(dto)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ActualResult(Errors.TupleDeletedOrUpdated);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PrivilegeEmployees);
                var result = await _context.PrivilegeEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.PrivilegeEmployees.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
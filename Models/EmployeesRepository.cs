﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeDapper.Models
{
    public class EmployeesRepository
    {
        private string connectionstring;

        public EmployeesRepository()
        {
            connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        }

        public List<Employee> GetAll(RequestModel request)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                    .Query<Employee>("Employees_GetAll",
                    request,
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }
        }
        public Employee Get(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                        .Query<Employee>("Employees_Get",
                            new { Id },
                            commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
            }
        }
        public int Create(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                int lastInsertedId =
                    db.Query<int>("Employees_Create",
                    employee,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
                return lastInsertedId;
            }
        }
        public int Update(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                    .Execute("Employees_Update",
                       employee,
                       commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db.Execute(
                        "Employees_Delete",
                        new { Id },
                        commandType: CommandType.StoredProcedure);
            }
        }
    }
}
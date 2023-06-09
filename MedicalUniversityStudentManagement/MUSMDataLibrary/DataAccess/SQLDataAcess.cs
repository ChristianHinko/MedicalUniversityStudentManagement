﻿using System.Collections.Generic;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

// Dapper functions notes:
//  - Query: Use when loading data since you will probably want the rows returned to you.
//  - QueryMultiple: Used for when you need multiple result sets. Returns a GridReader and can be used to return the result of multiple Select statements, using a concept of MARS (Multiple active result set).
//  - Execute: Used for executing the DML statements (like Insert, Update and Delete) whose purpose is to make changes to the data in the database. The return type is an integer.

namespace MUSMDataLibrary.DataAccess
{
    // Make sure to not call Load/Save Data within a loop. This will open and close a IDbConnection every call which is expensive.
    // Call it once and pass in a storedProcedureName that accepts a table value parameter so that you can create the table value
    // parameter before you call the method and pass it in as a stored procedure parameter. This allows for one database call.

    // Make this class internal to ensure only classes within this project can call on it. When using MUSMDataLibrary to contact the db you should not call on this class.
    internal static class SqlDataAccess
    {
        // 90% of loading data will call this. However if you want a stored procedure with output parameters, the caller
        // will need to know about Dapper if passing in parameters since it will need to use the dynamic parameters type.
        public static IEnumerable<T> LoadData<T>(string connString, string storedProcedureName, object parameters = null)
        {
            using (IDbConnection cnn = new SqlConnection(connString))
            {
                IEnumerable<T> rows = cnn.Query<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return rows;
            }
        }

        public static int ModifyData(string connString, string storedProcedureName, object parameters = null)
        {
            using (IDbConnection cnn = new SqlConnection(connString))
            {
                return cnn.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }


        public static async Task<IEnumerable<T>> LoadDataAsync<T>(string connString, string storedProcedureName, object parameters = null)
        {
            using (IDbConnection cnn = new SqlConnection(connString))
            {
                return await cnn.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public static async Task<int> ModifyDataAsync(string connString, string storedProcedureName, object parameters = null)
        {
            using (IDbConnection cnn = new SqlConnection(connString))
            {
                return await cnn.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data.SqlServerCe;
using System.Data;

namespace Database
{
    public static class Companies
    {

        public static List<Company> GetAllCompanies()
        {
            var list = new List<Company>();
            SqlCeCommand sqlCeCommand = new SqlCeCommand("select * from company order by name", DatabaseConnector.DatabaseConnection);
            sqlCeCommand.CommandType = CommandType.Text;
            SqlCeResultSet sqlCeResultSet = sqlCeCommand.ExecuteResultSet(ResultSetOptions.Scrollable);
            if (sqlCeResultSet.HasRows)
            {
                int ordinal0 = sqlCeResultSet.GetOrdinal("id");
                int ordinal1 = sqlCeResultSet.GetOrdinal("name");
                int ordinal2 = sqlCeResultSet.GetOrdinal("address");
                int ordinal3 = sqlCeResultSet.GetOrdinal("OIB");
                sqlCeResultSet.ReadFirst();
                list.Add(new Company()
                {
                    Id = sqlCeResultSet.GetInt32(ordinal0),
                    Name = sqlCeResultSet.GetString(ordinal1),
                    Address = sqlCeResultSet.GetString(ordinal2),
                    OIB = sqlCeResultSet.GetString(ordinal3)
                });
                while (sqlCeResultSet.Read())
                    list.Add(new Company()
                    {
                        Id = sqlCeResultSet.GetInt32(ordinal0),
                        Name = sqlCeResultSet.GetString(ordinal1),
                        Address = sqlCeResultSet.GetString(ordinal2),
                        OIB = sqlCeResultSet.GetString(ordinal3)
                    });
            }
            return list;
        }

        public static Company AddUpdateCompany(int id, string name, string address, string OIB)
        {
            var company = GetCompany(id);
            var sql = "insert into company "
            + "(name, address, OIB) "
            + "values (@name, @address, @OIB)";


            if (company != null)
            {
                company.Id = id;
                company.Name = name;
                company.Address = address;
                company.OIB = OIB;

                sql = "update company "
                  + "set name=@name, address=@address, OIB=@OIB "
                  + "where id=@id";
            }
            else
            {
                company = new Company()
                {
                    Name = name,
                    Address = address,
                    OIB = OIB
                };
            }

            try
            {
                SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
                cmd.Parameters.AddWithValue("@id", company.Id);
                cmd.Parameters.AddWithValue("@name", company.Name);
                cmd.Parameters.AddWithValue("@address", company.Address);
                cmd.Parameters.AddWithValue("@OIB", company.OIB);
                cmd.ExecuteNonQuery();

                return company;
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                return null;
            }
        }

        public static Company GetCompany(int id)
        {
            string sql = "select id, name, address, OIB from company where id=@id";
            Company company = null;
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.Text;
            SqlCeResultSet rs = cmd.ExecuteResultSet(
            ResultSetOptions.Scrollable);
            if (rs.HasRows)
            {
                int ordId = rs.GetOrdinal("id");
                int ordName = rs.GetOrdinal("name");
                int ordAddress = rs.GetOrdinal("address");
                int ordOIB = rs.GetOrdinal("OIB");

                rs.ReadFirst();
                company = new Company();
                company.Id = rs.GetInt32(ordId);
                company.Name = rs.GetString(ordName);
                company.Address = rs.GetString(ordAddress);
                company.OIB = rs.GetString(ordOIB);
            }
            return company;
        }

        public static void DeleteCompany(int companyId)
        {
            string sql = "delete from company where id=@id";
            SqlCeCommand cmd = new SqlCeCommand(sql, DatabaseConnector.DatabaseConnection);
            cmd.Parameters.AddWithValue("@id", companyId);
            cmd.ExecuteNonQuery();
            DatabaseConnector.Disconnect();
        }
    }
}

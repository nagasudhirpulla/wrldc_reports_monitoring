using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WRM.Domain.Interfaces.Reports;
using Oracle.ManagedDataAccess.Client;
using System.Globalization;
using System.Threading.Tasks;

namespace WRM.Infra.Reports
{
    public class ReportsFetchService : IReportsFetchService
    {
        private readonly string _oracleConnString;
        public ReportsFetchService(IConfiguration configuration)
        {
            _oracleConnString = configuration["ConnectionStrings:ReportsOracleConnection"];
        }

        public async Task<List<(DateTime, double)>> FetchTimeseriesData(string queryString, string queryTimeType, DateTime startTime, DateTime endTime)
        {
            List<(DateTime, double)> res = new List<(DateTime, double)>();
            string sql = "";
            if (queryTimeType == "date_key")
            {
                sql = queryString.Replace("{start_time}", startTime.ToString("yyyyMMdd"))
                                     .Replace("{end_time}", endTime.ToString("yyyyMMdd"));
            }
            using (OracleConnection con = new OracleConnection(_oracleConnString))
            {
                using OracleCommand cmd = con.CreateCommand();
                try
                {
                    // get measurement data
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = sql;

                    //Execute the command and use DataReader to read the data
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        DateTime timestamp = new DateTime();
                        if (queryTimeType == "date_key")
                        {
                            string inpTimeFormat = "yyyyMMdd";
                            // we will get date_key integer for timestamp
                            string dateKey = reader.GetInt32(0).ToString();
                            // convert date key to timestamp
                            timestamp = DateTime.ParseExact(dateKey, inpTimeFormat, CultureInfo.InvariantCulture);
                        }
                        if (!reader.IsDBNull(1))
                        {
                            double val = reader.GetDouble(1);
                            res.Add((timestamp, val));
                        }
                    }
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    res = new List<(DateTime, double)>();
                }
            }
            return res;
        }
    }
}

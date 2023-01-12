using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CountryController(IConfiguration configuration)
        {
             this._configuration =  configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ID, COUNTRYNAME from userdb.country_tbl";
            DataTable table = new DataTable();
            string sqlDataSource =  _configuration.GetConnectionString("DbConnection");
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(MySqlCommand myCommand = new MySqlCommand(query,mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
                
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Country Name)
        {
            string query = @"Insert into userdb.country_tbl(ID, COUNTRYNAME) values(guid(), @CountryName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@CountryName", Name.CountryName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult("Added SuccessFully");
        }

        [HttpPut]
        public JsonResult Put(Country Name)
        {
            string query = @"Update userdb.country_tbl set COUNTRYNAME = @CountryName where ID = @Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Id", Name.Id);
                    myCommand.Parameters.AddWithValue("@CountryName", Name.CountryName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult("Updated SuccessFully");
        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"Delete userdb.country_tbl where ID = @Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Id", Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult("Deleted SuccessFully");
        }
    }
}

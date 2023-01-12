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
    public class StateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StateController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ID, STATENAME, COUNTRY_ID from userdb.state_tbl";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
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
        public JsonResult Post(State Name)
        {
            string query = @"Insert into userdb.state_tbl(ID, STATENAME, COUNTRY_ID) values(guid(),@StateName,  @Country_Id)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@StateName", Name.StateName);
                    myCommand.Parameters.AddWithValue("@Country_Id", Name.CountryId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }

            }
            return new JsonResult("Added SuccessFully");
        }

        [HttpPut]
        public JsonResult Put(State Name)
        {
            string query = @"Update userdb.state_tbl set STATENAME = @StateName where Id = @Id AND CountryId = @Country_Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Id", Name.Id);
                    myCommand.Parameters.AddWithValue("@StateName", Name.StateName);
                    myCommand.Parameters.AddWithValue("@Country_Id", Name.CountryId);
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
            string query = @"Delete userdb.state_tbl where ID = @Id";
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

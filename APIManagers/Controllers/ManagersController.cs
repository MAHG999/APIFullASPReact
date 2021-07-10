
using APIManagers.Context;
using APIManagers.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIManagers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ManagersController : Controller
    {
        #region
        /// <summary>
        /// Import the context
        /// </summary>
        private readonly AppDbContext context;
        public ManagersController(AppDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region GetAllData
        /// <summary>
        /// Create of the metod Get for get all data from the database
        /// </summary>
        /// <returns>Values from the database in form the list</returns>
        [HttpGet]
        public ActionResult Get()
        {
            //Error control
            try
            {
                //Return of the value of the list of the all values in the data base
                return Ok(context.Gestores_DB.ToList());
            }
            catch (Exception ex)
            {
                //Return de error message for to control the errors
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetDataByID
        /// <summary>
        /// Get data by ID 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Return the file with the ID </returns>
        [HttpGet("id", Name = "GetManagerbyID")]
        public ActionResult Get(int ID)
        {
            try
            {
                var manager = context.Gestores_DB.FirstOrDefault(g => g.ID == ID);
                return Ok(manager);
            }
            catch (Exception ex)
            {
                //Return de error message for to control the errors
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Post
        /// <summary>
        /// Save the data 
        /// </summary>
        /// <param name="Manager"></param>
        /// <returns>Return the refcord whqat it save</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Managers_DB Manager)
        {
            try
            {
                context.Gestores_DB.Add(Manager);
                context.SaveChanges();
                return CreatedAtRoute("GetManagerbyID", new { id = Manager.ID }, Manager);
            }
            catch (Exception ex)
            {
                //Return de error message for to control the errors
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Put
        /// <summary>
        /// Modificate data by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Manager"></param>
        /// <returns>Return ID and data </returns>
        [HttpPut("{ID}")]
        public ActionResult Put(int ID, [FromBody] Managers_DB Manager)
        {
            try
            {
                if (Manager.ID == ID)
                {
                    context.Entry(Manager).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetManagerbyID", new { id = Manager.ID }, Manager);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                //Return de error message for to control the errors
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Retuyns the value of ID what was erase</returns>
        [HttpDelete("{ID}")]
        public ActionResult Delete(int ID)
        {
            try
            {
                var Managers = context.Gestores_DB.FirstOrDefault(g => g.ID == ID);
                if (Managers != null)
                {
                    context.Gestores_DB.Remove(Managers);
                    context.SaveChanges();
                    return Ok(ID);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}

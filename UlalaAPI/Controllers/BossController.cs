﻿using DAL.Entities;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using UlalaAPI.Helper;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class BossController : ApiController
    {
        BossRepository repo = new BossRepository();

        #region POST Add d'un Boss
        /// <summary>
        /// Post API/Boss
        /// </summary>
        /// <param name="Boss">Boss à insérer</param>
        public IHttpActionResult Post(BossModel Boss)
        {
            //Attention penser a remettre admin only
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Boss == null || Boss.NameEN == null || Boss.NameFR == null) return BadRequest();
                else
                {
                    repo.Create(Boss.MapTo<BossEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les boss
        /// <summary>
        /// Get API/Boss
        /// </summary>
        /// <returns>List de tous les boss</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<BossModel> List = repo.GetAll().Select(Boss => Boss?.MapTo<BossModel>());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un Boss by Id
        /// <summary>
        /// Get API/Boss/{id}
        /// </summary>
        /// <param name="id">id du Boss à récupérer</param>
        /// <returns>Boss avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                BossModel Objet = repo.GetOne(id)?.MapTo<BossModel>();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Boss by Id
        /// <summary>
        /// Delete API/Boss/{id}
        /// </summary>
        /// <param name="id">id du Boss à supprimer</param>
        public IHttpActionResult Delete(int id)
        {
            //Attention penser a remettre admin only
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else
                {
                    repo.Delete(id);
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region PUT Update d'un Boss by Id
        /// <summary>
        /// Put API/Boss/{id}
        /// </summary>
        /// <param name="Boss">Boss à insérer</param>
        /// <param name="id">Id du Boss à Updateier</param>
        public IHttpActionResult Put(int id, BossModel Boss)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                else if (Boss == null || Boss.NameEN == null || Boss.NameFR == null) return BadRequest();
                else
                {
                    repo.Update(id, Boss.MapTo<BossEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}

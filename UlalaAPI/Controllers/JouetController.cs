﻿using DAL.Entities;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UlalaAPI.Helper;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class JouetController : ApiController
    {
        JouetRepository repo = new JouetRepository();

        #region POST Ajout d'un jouet
        /// <summary>
        /// Post API/Jouet
        /// </summary>
        /// <param name="E">Jouet à insérer</param>
        public IHttpActionResult Post(JouetModel Jouet)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Jouet == null || Jouet.ImagePath == null || Jouet.NomFR == null || Jouet.NomEN == null) return BadRequest();
                else
                {
                    repo.Create(Jouet.MapTo<JouetEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de tous les Jouets
        /// <summary>
        /// Get API/Jouet
        /// </summary>
        /// <returns>Liste de tous les Jouets</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<JouetModel> Liste = repo.GetAll().Select(Jouet => Jouet?.MapTo<JouetModel>());
                if (Liste.Count() == 0) return NotFound();
                else return Json(Liste);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'un Jouet by Id
        /// <summary>
        /// Get API/Jouet/{id}
        /// </summary>
        /// <param name="id">id du Jouet à récupérer</param>
        /// <returns>Jouet avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                JouetModel Objet = repo.GetOne(id)?.MapTo<JouetModel>();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'un Jouet by Id
        /// <summary>
        /// Delete API/Jouet/{id}
        /// </summary>
        /// <param name="id">id du Jouet à supprimer</param>
        public IHttpActionResult Delete(int id)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
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

        #region PUT Update d'un Jouet by Id
        /// <summary>
        /// Put API/Jouet/{id}
        /// </summary>
        /// <param name="Jouet">Jouet à insérer</param>
        /// <param name="Id">Id du Jouet à modifier</param>
        public IHttpActionResult Put(int Id, JouetModel Jouet)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(Id) != null) return NotFound();
                else if (Jouet == null || Jouet.ImagePath == null || Jouet.NomFR == null || Jouet.NomEN == null) return BadRequest();
                else
                {
                    repo.Update(Id, Jouet.MapTo<JouetEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}

﻿using DAL.Entities;
using DAL.Services;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Http;
using UlalaAPI.Helper;
using UlalaAPI.Mapper;
using UlalaAPI.Models;

namespace UlalaAPI.Controllers
{
    public class ClasseController : ApiController
    {
        ClasseRepository repo = new ClasseRepository();

        #region POST Add d'une Classe
        /// <summary>
        /// Post API/Classe
        /// </summary>
        /// <param name="Classe">Classe à insérer</param>
        public IHttpActionResult Post(ClasseModel Classe)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (Classe == null || Classe.NameEN == null || Classe.NameFR == null) return BadRequest();
                else
                {
                    repo.Create(Classe.MapTo<ClasseEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération de toutes les Classes
        /// <summary>
        /// Get API/Classe
        /// </summary>
        /// <returns>List de toutes les Classes</returns>
        public IHttpActionResult Get()
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                IEnumerable<ClasseModel> List = repo.GetAll().Select(Classe => Classe?.MapTo<ClasseModel>());
                if (List.Count() == 0) return NotFound();
                else return Json(List);
            }
            else return Unauthorized();
        }
        #endregion

        #region GET Récupération d'une Classe by Id
        /// <summary>
        /// Get API/Classe/{id}
        /// </summary>
        /// <param name="id">id de la Classe à récupérer</param>
        /// <returns>Classe avec l'id correspondant</returns>
        public IHttpActionResult Get(int id)
        {
            if ((new[] { "Admin", "User", "Anonyme" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                ClasseModel Objet = repo.GetOne(id)?.MapTo<ClasseModel>();
                if (Objet == null) return NotFound();
                else return Json(Objet);
            }
            else return Unauthorized();
        }
        #endregion

        #region DELETE Suppression d'une Classe by Id
        /// <summary>
        /// Delete API/Classe/{id}
        /// </summary>
        /// <param name="id">id de la Classe à supprimer</param>
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

        #region PUT Update d'une Classe by Id
        /// <summary>
        /// Put API/Classe/{id}
        /// </summary>
        /// <param name="Classe">Classe à insérer</param>
        /// <param name="id">Id de la Classe à Updateier</param>
        public IHttpActionResult Put(int id, ClasseModel Classe)
        {
            if ((new[] { "Admin" }).Contains(ValidateTokenAndRole.ValidateAndGetRole(Request), StringComparer.OrdinalIgnoreCase))
            {
                if (repo.GetOne(id) == null) return NotFound();
                if (Classe == null || Classe.NameEN == null || Classe.NameFR == null) return BadRequest();
                else
                {
                    repo.Update(id, Classe.MapTo<ClasseEntity>());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
        #endregion
    }
}

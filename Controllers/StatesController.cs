using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Prueba.Pagination;
using System.Linq;
using Prueba.Model;
using System;

namespace Prueba.Controllers
{
    [ApiController]
    public class StatesController : Controller
    {
        private readonly ISO3166DBContext _context;

        public StatesController(ISO3166DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all subdivision as a paginated result
        /// </summary>
        /// <param name="SearchCriteria">Texto de búsqueda.</param>
        /// <param name="SortField">Nombre de campo por el cual ordenar (distingue mayúsculas).</param>
        /// <param name="SortType">Tipo de orden: ASC (ascendente) / DESC (descendente).</param>
        /// <param name="CurrentPage">Número de página a obtener.</param>
        /// <param name="RecordsPerPage">Número de registros por página.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/States")]
        public GenericPaginator<States> Get(string SearchCriteria, string SortField = "Name", string SortType = "ASC", int CurrentPage = 1, int RecordsPerPage = 10)
        {
            List<States> _States;
            GenericPaginator<States> _StatesPaginatedResult;
            _States = _context.States.ToList();

            // Apply SearchCriteria
            if (!string.IsNullOrEmpty(SearchCriteria))
            {
                foreach (var item in SearchCriteria.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _States = _States.Where(x => x.Name.Contains(item) || x.StateCode.Contains(item)).ToList();
                }
            }

            // Apply Sort Field and Sort Type
            switch (SortField)
            {
                case "StateId":
                    if (SortType.ToLower() == "desc")
                        _States = _States.OrderByDescending(x => x.StateId).ToList();
                    else if (SortType.ToLower() == "asc")
                        _States = _States.OrderBy(x => x.StateId).ToList();
                    break;

                case "name":
                    if (SortType.ToLower() == "desc")
                        _States = _States.OrderByDescending(x => x.Name).ToList();
                    else if (SortType.ToLower() == "asc")
                        _States = _States.OrderBy(x => x.Name).ToList();
                    break;

                //Appply default sort fiel and sort type
                default:
                    _States = _States.OrderBy(x => x.Name).ToList();
                    break;
            }

            //Apply Pagination 
            int _TotalRecs = 0;
            int _TotalPages = 0;

            _TotalRecs = _States.Count(); // Get total records in table
            _States = _States.Skip((CurrentPage - 1) * RecordsPerPage).Take(RecordsPerPage).ToList(); // Setup Pagination
            _TotalPages = (int)Math.Ceiling((double)_TotalRecs / RecordsPerPage); // Get total pages

            // Set all pagination params and return result
            _StatesPaginatedResult = new GenericPaginator<States>()
            {
                CurrentPage = CurrentPage,
                RecordsPerPage = RecordsPerPage,
                TotalRecords = _TotalRecs,
                TotalPages = _TotalPages,
                CurrentSearch = SearchCriteria,
                CurrentSort = SortField,
                CurrentSortType = SortType,
                Result = _States
            };

            return _StatesPaginatedResult;
        }

        /// <summary>
        /// Get Subdivision by given name
        /// </summary>
        /// <param name="StateName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/States/{StateName}")]
        public dynamic Get(string StateName)
        {
            if (StateName == null)
            {
                return NotFound();
            }

            var deptos = _context.Cities.FirstOrDefault(c => c.Name.Contains(StateName));

            if (deptos == null)
            {
                return NotFound();
            }
            else
            {
                return deptos;
            }
        }

        /// <summary>
        /// Get al subdivision of a country given name
        /// </summary>
        /// <param name="CountryName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetStatesByCountryName/{CountryName}")]
        public dynamic GetStatesByCountryName(string CountryName)
        {
            if (CountryName == null)
            {
                return NotFound();
            }

            var deptos = (from c in _context.States
                          where c.Country.Name.Contains(CountryName)
                          select c);

            if (deptos == null)
            {
                return NotFound();
            }
            else
            {
                return deptos;
            }
        }

        /// <summary>
        /// Create a new subdivision binded to parent country
        /// </summary>
        /// <param name="NewState"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddNewState")]
        public dynamic AddNewState([FromBody] States_Item NewState)
        {
            try
            {
                States _State;
                _State = new()
                {
                    Name = NewState.Name,
                    CountryId = NewState.CountryId,
                    Latitude = NewState.Latitude,
                    Longitude = NewState.Longitude,
                    StateCode = NewState.StateCode
                };

                _context.States.Add(_State);
                _context.SaveChanges();
                return CreatedAtAction(nameof(States), new { id = NewState.StateId }, NewState);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Remove a sub region and all child cities
        /// </summary>
        /// <param name="StateName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/DelStateRecord")]
        public dynamic DelStateRecord(string StateName)
        {
            try
            {
                if (StateName == null)
                {
                    return NotFound();
                }

                var _ItemToDel = _context.States.FirstOrDefault(c => c.Name.Contains(StateName));

                if (_ItemToDel == null)
                {
                    return NotFound();
                }
                else
                {
                    //Get Cities to delete
                    var _ChildCitiesToDel = (City)(from c in _context.Cities where c.StateId == _ItemToDel.StateId select c);
                    //remover cities
                    _context.Cities.Remove(_ChildCitiesToDel);
                    //remove states
                    _context.States.Remove(_ItemToDel);
                    _context.SaveChanges();

                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

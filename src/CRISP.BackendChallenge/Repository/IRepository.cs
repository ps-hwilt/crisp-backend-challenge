﻿using CRISP.BackendChallenge.Context.Models;

namespace CRISP.BackendChallenge.Repository
{
    /// <summary>
    /// Interface for the repository.
    /// </summary>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Generic method that will allow for more complex EF queries.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<Employee> Query();

        /// <summary>
        /// Get All Entities available in the database.
        /// </summary>
        /// <typeparam name="T">The type of entity</typeparam>
        /// <returns>An enumerable of the Entity Type</returns>
        IEnumerable<Employee> GetAll();

        /// <summary>
        /// Get an instance of the Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Employee? GetById(int id);

        /// <summary>
        /// Add an entity to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        void Add(Employee entity);

        /// <summary>
        /// Delete an entity from the database.
        /// returns true if deletion is complete
        /// returns false if employee is not found
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        bool Delete(int id);

        /// <summary>
        /// Update an entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Employee? Update(Employee entity);
        
        
        /// <summary>
        /// Searches an entity in the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="department"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<Employee> Search(string? name, int? department);

        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        void Save();
    }
}
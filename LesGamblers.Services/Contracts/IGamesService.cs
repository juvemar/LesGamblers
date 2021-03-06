﻿namespace LesGamblers.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IGamesService
    {
        IQueryable<Game> GetAll();

        IQueryable<Game> GetById(int id);

        void Add(Game game);

        void UpdateGame(Game game, int id);

        void DeleteAll(bool hardDelete);

        IQueryable<Game> GetAllWithDeleted();
    }
}

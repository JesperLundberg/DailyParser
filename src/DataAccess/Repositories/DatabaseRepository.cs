﻿using DataAccess.DatabaseContexts;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class DatabaseRepository : IDatabaseRepository
{
    private GameContext GameContext { get; }

    public DatabaseRepository(GameContext gameContext)
    {
        GameContext = gameContext;
    }

    public IEnumerable<Game> GetAllGames()
    {
        return GameContext.Games.AsAsyncEnumerable().ToBlockingEnumerable();
    }
}
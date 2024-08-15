using WebApp.contracts;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GetGameDto> games = [
    new (
        1,
        "Carter1",
        "This is the first object"
    ),

    new (
        2,
        "Carter2",
        "Second object"
    )
];
// GET /games
app.MapGet("games", () => games);

app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(GetGameEndpointName); 

// POST
app.MapPost("games", (CreateGameDto newGame) =>
{
    GetGameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Description
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
});

// PUT 
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => {
    var index = games.FindIndex(game => game.Id == id);

    games[index] = new GetGameDto(
        id,
        updatedGame.Name,
        updatedGame.Description
    );
    return Results.NoContent();
});

app.Run();

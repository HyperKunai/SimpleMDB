namespace Smdb.Api.ActorMovies;

using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using Shared.Http;
using Smdb.Core.ActorMovies;

public class ActorMoviesController
{
    private IActorMovieService actorMovieService;

    public ActorMoviesController(IActorMovieService actorMovieService)
    {
        this.actorMovieService = actorMovieService;
    }

    public async Task ReadActorMovies(
        HttpListenerRequest req,
        HttpListenerResponse res,
        Hashtable props,
        Func<Task> next
    )
    {
        int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
        int size = int.TryParse(req.QueryString["size"], out int s) ? s : 9;

        var result = await actorMovieService.ReadActorMovies(page, size);

        await JsonUtils.SendPagedResultResponse(req, res, props, result, page, size);
        await next();
    }

    public async Task CreateActorMovie(
        HttpListenerRequest req,
        HttpListenerResponse res,
        Hashtable props,
        Func<Task> next
    )
    {
        var text = (string)props["req.text"]!;
        var actorMovie = JsonSerializer.Deserialize<ActorMovie>(text, JsonUtils.DefaultOptions);

        if (actorMovie == null)
        {
            await HttpUtils.SendResponse(
                req, res, props,
                (int)HttpStatusCode.BadRequest,
                "Invalid actor-movie payload.",
                "text/plain"
            );
            await next();
            return;
        }

        var result = await actorMovieService.CreateActorMovie(actorMovie);

        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task ReadActorMovie(
        HttpListenerRequest req,
        HttpListenerResponse res,
        Hashtable props,
        Func<Task> next
    )
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;

        var result = await actorMovieService.ReadActorMovie(id);

        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task UpdateActorMovie(
        HttpListenerRequest req,
        HttpListenerResponse res,
        Hashtable props,
        Func<Task> next
    )
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;

        var text = (string)props["req.text"]!;
        var actorMovie = JsonSerializer.Deserialize<ActorMovie>(text, JsonUtils.DefaultOptions);

        if (actorMovie == null)
        {
            await HttpUtils.SendResponse(
                req, res, props,
                (int)HttpStatusCode.BadRequest,
                "Invalid actor-movie payload.",
                "text/plain"
            );
            await next();
            return;
        }

        var result = await actorMovieService.UpdateActorMovie(id, actorMovie);

        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task DeleteActorMovie(
        HttpListenerRequest req,
        HttpListenerResponse res,
        Hashtable props,
        Func<Task> next
    )
    {
        var uParams = (NameValueCollection)props["req.params"]!;
        int id = int.TryParse(uParams["id"]!, out int i) ? i : -1;

        var result = await actorMovieService.DeleteActorMovie(id);

        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }
}
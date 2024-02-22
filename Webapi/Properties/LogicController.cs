namespace Webapi;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("/")]
public class LogicController : ControllerBase
{
    private readonly MyDbContext _myDb;

    public LogicController(MyDbContext myDbContext)
    {
        _myDb = myDbContext;
    }

    [HttpGet("highscores")]
    public IActionResult GetHighscores()
    {
        return Ok(_myDb.Highscore.ToList());
    }



    [HttpPost("highscores")]
    public IActionResult PostHighscore(Highscore hs)
    {
        if (hs.Name != null)
        {
            _myDb.Highscore.Add(hs);
            _myDb.SaveChanges();
            return Ok("Highscore is saved!");
        }
        else
        {
            return BadRequest("Dåligt!");
        }
    }


}
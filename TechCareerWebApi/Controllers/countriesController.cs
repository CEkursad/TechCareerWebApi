using Microsoft.AspNetCore.Mvc;

namespace WebUygulaması.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class countriesController : Controller
    {
        [HttpGet]
        public string[] Get()
        {
            string[] countries = new string[]
             { "Almanya", "İngiltere","Fransa","Hırvatistan","Kanada","Macaristan","Bulgaristan","Letonya","Türkiye",
            "Irak","İran","Ukrayna","Rusya","Azerbaycan","Portekiz","İspanya","Norveç","İsveç","İsviçre","Makedonya"};
            return countries;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            string[] countries = new string[]
            {
                  "Almanya", "İngiltere","Fransa","Hırvatistan","Kanada","Macaristan","Bulgaristan","Letonya","Türkiye",
            "Irak","İran","Ukrayna","Rusya","Azerbaycan","Portekiz","İspanya","Norveç","İsveç","İsviçre","Makedonya"};

            if (id >= 0 && id < countries.Length)
            {
                return countries[id];
            }
            else

                return id + " nolu id listede mevcut degildir lutfen 0 ile 19 arasında deger isteyiniz.";

        }
    }
}

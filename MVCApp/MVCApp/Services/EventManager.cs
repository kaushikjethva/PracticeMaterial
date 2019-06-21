using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCApp.InfraStructure;
using MVCApp.Models;

namespace MVCApp.Services
{
    public class EventManager : IEventManager
    {
        private static List<EventData> events = new List<EventData>()
        {
            new EventData{
                Id =1,
                Title ="Developing Cloud apps using .Net Core",
            Speaker="Kaushik Jethva",
            Location="Mumbai",
            StartDate=DateTime.Now.AddDays(2),
            EndDate=DateTime.Now.AddDays(5),
            Url="http://learningskills.com/register/EV102-core"
            },
            new EventData{
                Id =1,
                Title ="Cloud architecting using .Net Core",
            Speaker="Vivek Tiwari",
            Location="Mumbai",
            StartDate=DateTime.Now.AddDays(6),
            EndDate=DateTime.Now.AddDays(8),
            Url="http://learningskills.com/register/EV105-AZ300"
            }
        };

        private EventDBContext _db;
        public EventManager(EventDBContext db)
        {
            _db = db;
        }

        public EventData Add(EventData data)
        {
            //data.Id = events.Max(e => e.Id) + 1;
            //events.Add(data);
            _db.Events.Add(data);
            _db.SaveChanges();            
            return data;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EventData GetEvent(int id)
        {
            return _db.Events.Find(id);
        }

        public IEnumerable<EventData> GetEvents()
        {
            return _db.Events.ToList();
        }

        public EventData Update(int id, EventData data)
        {
            throw new NotImplementedException();
        }
    }
}

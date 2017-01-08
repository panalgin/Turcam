using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turcam;
using Newtonsoft.Json;

namespace Turcam.Controller
{
    public sealed class DrillBitController : BaseController
    {
        public override bool Add(string data)
        {
            base.Add(data);

            return false;
        }

        public override bool Delete(string data)
        {
            base.Delete(data);

            return false;
        }

        public override string Read()
        {
            base.Read();

            using(TurcamEntities m_Context = new TurcamEntities())
            {
                var m_DrillBits = m_Context.DrillBits.ToList();
                return JsonConvert.SerializeObject(m_DrillBits);
            }
        }

        public override string Update(string data)
        {
            base.Update(data);

            DrillBit m_PassedBit = JsonConvert.DeserializeObject<DrillBit>(data);

            if (m_PassedBit != null)
            {
                using (TurcamEntities m_Context = new TurcamEntities())
                {
                    DrillBit m_ActualBit = m_Context.DrillBits.Where(q => q.ID == m_PassedBit.ID).FirstOrDefault();

                    if (m_ActualBit != null)
                    {
                        DrillBit m_AnotherExists = m_Context.DrillBits.Where(q => q.Name != m_ActualBit.Name && q.Name == m_PassedBit.Name).FirstOrDefault();

                        if (m_AnotherExists != null)
                            return "Bu isimle kaydedilmiş başka bir takım mevcut.";

                        try
                        {
                            m_ActualBit.Name = m_PassedBit.Name;
                            m_ActualBit.Diameter = m_PassedBit.Diameter;
                            m_ActualBit.Length = m_PassedBit.Length;
                            m_ActualBit.Shank = m_PassedBit.Shank;

                            m_Context.SaveChanges();

                            return "OK";
                        }
                        catch (Exception ex)
                        {
                            Logger.Enqueue(ex);

                            return ex.Message;
                        }
                    }
                }
            }

            return null;
        }

        public override string Get(int id)
        {
            base.Get(id);

            using(TurcamEntities m_Context = new TurcamEntities())
            {
                var m_DrillBit = m_Context.DrillBits.Where(q => q.ID == id);

                if (m_DrillBit != null)
                    return JsonConvert.SerializeObject(m_DrillBit);
                else
                    return null;
            }
        }
    }
}

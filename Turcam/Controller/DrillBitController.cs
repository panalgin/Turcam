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
        public override string Add(string data)
        {
            base.Add(data);

            DrillBit m_PassedBit = JsonConvert.DeserializeObject<DrillBit>(data);

            if (m_PassedBit != null)
            {
                using (TurcamEntities m_Context = new TurcamEntities())
                {
                    DrillBit m_ExistingBit = m_Context.DrillBits.Where(q => q.Name == m_PassedBit.Name).FirstOrDefault();

                    if (m_ExistingBit != null)
                        return "Bu isimle kaydedilmiş başka bir takım mevcut.";
                    else
                    {
                        try
                        {
                            DrillBit m_Bit = new DrillBit();

                            m_Bit.Name = m_PassedBit.Name;
                            m_Bit.Diameter = m_PassedBit.Diameter;
                            m_Bit.Length = m_PassedBit.Diameter;
                            m_Bit.Shank = m_PassedBit.Shank;

                            m_Context.DrillBits.Add(m_Bit);
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

        public override bool Delete(int id)
        {
            base.Delete(id);

            using (TurcamEntities m_Context = new TurcamEntities())
            {
                var m_ActualBit = m_Context.DrillBits.Where(q => q.ID == id).FirstOrDefault();

                if (m_ActualBit != null)
                {
                    m_Context.DrillBits.Remove(m_ActualBit);
                    m_Context.SaveChanges();

                    return true;
                }

                else
                    return false;
            }
        }

        public override string Read()
        {
            base.Read();

            using(TurcamEntities m_Context = new TurcamEntities())
            {
                var m_DrillBits = m_Context.DrillBits.OrderBy(q => q.Name).ToList();
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

using DAL;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Globalization;

using System.IO;
//using Stripe;

//using Newtonsoft.Json.Linq;

namespace BAL
{
    public class MasterUpdate : BusinessBase
    {
        //string path = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        public UserMaster Login(string Name, string Phone)
        {
            UserMaster um = new UserMaster();
            um = (from a in DB.UserMasters where a.UserName == Name select a).FirstOrDefault();
            if (um == null)
            {
                um.UserName = Name;
                um.Phone = Phone;
                um.CreatedDate = System.DateTime.Now;
                DB.UserMasters.InsertOnSubmit(um);
                DB.SubmitChanges();
            }
            else
            {
                um = (from b in DB.UserMasters where b.UserName == Name && b.Phone == Phone select b).FirstOrDefault();
            }
            return um;

        }

        public Player AddPlayer(int UserId, int playerId, string GroupName, int GroupId)
        {
            Player p = new Player();
            int GId = 0;
            if (GroupName != "" && GroupId == 0)
            {
                tblGroup tg = new tblGroup();
                tg = (from a in DB.tblGroups where a.GroupName == GroupName select a).FirstOrDefault();
                if (tg == null)
                {
                    tg.UserId = UserId;
                    tg.GroupName = GroupName;
                    tg.CreatedDate = System.DateTime.Now;
                    DB.tblGroups.InsertOnSubmit(tg);
                    DB.SubmitChanges();
                }
                if (playerId > 0)
                {
                    p = (from b in DB.Players where b.PlayerId == playerId select b).FirstOrDefault();
                    p.UserId = UserId;
                    p.GroupId = GroupId;
                    p.GroupName = GroupName;
                    p.CreatedDate = System.DateTime.Now;
                }
                else
                {
                    p.UserId = UserId;
                    p.GroupId = GroupId;
                    p.GroupName = GroupName;
                    p.CreatedDate = System.DateTime.Now;
                    DB.Players.InsertOnSubmit(p);
                }
                DB.SubmitChanges();
            }
            else if (GroupId > 0)
            {
                if (playerId > 0)
                {
                    p = (from b in DB.Players where b.PlayerId == playerId select b).FirstOrDefault();
                    p.UserId = UserId;
                    p.GroupId = GroupId;
                    p.GroupName = GroupName;
                    p.CreatedDate = System.DateTime.Now;
                }
                else
                {
                    p.UserId = UserId;
                    p.GroupId = GroupId;
                    p.GroupName = GroupName;
                    p.CreatedDate = System.DateTime.Now;
                    DB.Players.InsertOnSubmit(p);
                }
                DB.SubmitChanges();
            }
            return p;
        }

        public void DeletePlayer(int UserId, int PlayerId, int GroupId)
        {
            var data = (from a in DB.Players where a.UserId == UserId && a.PlayerId == PlayerId && a.GroupId == GroupId select a).FirstOrDefault();
            DB.Players.DeleteOnSubmit(data);
            DB.SubmitChanges();
        }

        public void DeleteGroup(int UserId, int GroupId)
        {
            var data = (from a in DB.tblGroups where a.UserId == UserId && a.Id == GroupId select a).FirstOrDefault();
            DB.tblGroups.DeleteOnSubmit(data);
            DB.SubmitChanges();
        }
    }
}

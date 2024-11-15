using Core.DataService;
using Services.AppServices;
using Services.DataContext;
using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.Account
{
    public class AccountDataService : EntityContextDataService<User>
    {
        public AccountDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }

        public User Login(string userName, string encryptPassword, bool isfromSingIn)
        {
            try
            {
                if (isfromSingIn == true)
                {
                    return DbContext.Set<User>().FirstOrDefault(x =>
                   (x.EmailAddress == userName) && x.Password == encryptPassword &&
                   x.IsActivated == true);
                }
                else
                {
                    return DbContext.Set<User>().FirstOrDefault(x =>
                   (x.EmailAddress == userName) && x.Password == encryptPassword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public User ProcessNewUserLogin(string userName, string encryptPassword, string key, bool isfromSingIn)
        {
            try
            {
                User userInfo = DbContext.Set<User>().FirstOrDefault(x =>
                     (x.EmailAddress == userName) && x.Password == encryptPassword);

                if (userInfo != null) {
                    userInfo.IsActivated = false;
                    DbContext.SaveChanges();
                }

                return userInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IList<CountryListModel> GetAllCountryList()
        {

            IList<CountryListModel> dataList = DbContext.Database.SqlQuery<CountryListModel>("select ID,Name from CountryList order by id asc").ToList();

            return dataList;
        }

        public IList<DistricListInBD> GetAllDistrictListInsideBangladesh()
        {

            IList<DistricListInBD> dataList = DbContext.Database.SqlQuery<DistricListInBD>("select * from DistricListInBD order by Name asc").ToList();

            return dataList;
        }


        public IList<Get_Travel_From_List> GetTravelFromSourceDataList()
        {

            IList<Get_Travel_From_List> dataList = DbContext.Database.SqlQuery<Get_Travel_From_List>("Get_Travel_From_List").ToList();

            return dataList;
        }

        public IList<Get_Travel_To_Data_List_By_FromId> GetTravelToDataRemoveFromId(int id)
        {
            SqlParameter prmFromId = new SqlParameter("@fromId", id);

            List<Get_Travel_To_Data_List_By_FromId> dataList = DbContext.Database.SqlQuery<Get_Travel_To_Data_List_By_FromId>("Get_Travel_To_Data_List_By_FromId @fromId", prmFromId).ToList();

            return dataList;
        }

        public IList<DistricListInBD> GetDistrictListByRemoveParamId(int id)
        {

            IList<DistricListInBD> dataList = DbContext.Database.SqlQuery<DistricListInBD>("select * from DistricListInBD where id <> "+ id + " order by Name asc").ToList();

            return dataList;
        }
        public IList<TimeSchedule> GetBusTimeScheduleListByFromAndToId(int fromId, int toId)
        {
            IList<TimeSchedule> dataList = DbContext.Database.SqlQuery<TimeSchedule>("select * from TimeSchedule where FromId = " + fromId + " and ToId = "+ toId + " order by StartTime asc").ToList();

            return dataList;
        }

        public IList<TicketBookingHistory> GetTicketBookingHistoryByParam(int fromId, int toId, string startTime, string travelDate)
        {
            IList<TicketBookingHistory> dataList = DbContext.Database.SqlQuery<TicketBookingHistory>("select * from TicketBookingHistory where FromId = " + fromId + " and ToId = " + toId + " and StartTime='"+ startTime + "' and TravelDate ='"+ travelDate + "' order by StartTime asc").ToList();

            return dataList;
        }

        public bool SaveBookingCustomerInformation(PassengerInformation model)
        {
            try
            {
                if (model != null) {

                    DbContext.Set<PassengerInformation>().Add(model);
                    DbContext.SaveChanges();
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool BookSelectedSeat(TicketBookingHistoryUpdateModel model)
        {
            try
            {
                TicketBookingHistory dataList = DbContext.Database.SqlQuery<TicketBookingHistory>("select * from TicketBookingHistory where FromId = " + model.fromId + " and ToId = " + model.toId + " and StartTime='" + model.startTime + "' and TravelDate = '" + model.DepartDate + "' order by StartTime asc").FirstOrDefault();

                if (dataList != null) {
                    if (model.seatList != "")
                    {
                        TicketBookingHistory info = new TicketBookingHistory();
                        info = dataList;

                        var seatArray = model.seatList.Split(',');

                        for (int i = 0; i < seatArray.Length; i++)
                        {
                            if (seatArray[i] == "1")
                                info.S1 = true;

                            if (seatArray[i] == "2")
                                info.S2 = true;

                            if (seatArray[i] == "3")
                                info.S3 = true;

                            if (seatArray[i] == "4")
                                info.S4 = true;

                            if (seatArray[i] == "5")
                                info.S5 = true;

                            if (seatArray[i] == "6")
                                info.S6 = true;

                            if (seatArray[i] == "7")
                                info.S7 = true;

                            if (seatArray[i] == "8")
                                info.S8 = true;

                            if (seatArray[i] == "9")
                                info.S9 = true;

                            if (seatArray[i] == "10")
                                info.S10 = true;

                            if (seatArray[i] == "11")
                                info.S11 = true;

                            if (seatArray[i] == "12")
                                info.S12 = true;

                            if (seatArray[i] == "13")
                                info.S13 = true;

                            if (seatArray[i] == "14")
                                info.S14 = true;

                            if (seatArray[i] == "15")
                                info.S15 = true;

                            if (seatArray[i] == "16")
                                info.S16 = true;

                            if (seatArray[i] == "17")
                                info.S17 = true;

                            if (seatArray[i] == "18")
                                info.S18 = true;

                            if (seatArray[i] == "19")
                                info.S19 = true;

                            if (seatArray[i] == "20")
                                info.S20 = true;

                            if (seatArray[i] == "21")
                                info.S21 = true;

                            if (seatArray[i] == "22")
                                info.S22 = true;

                            if (seatArray[i] == "23")
                                info.S23 = true;

                            if (seatArray[i] == "24")
                                info.S24 = true;

                            if (seatArray[i] == "25")
                                info.S25 = true;

                            if (seatArray[i] == "26")
                                info.S26 = true;

                            if (seatArray[i] == "27")
                                info.S27 = true;

                            if (seatArray[i] == "28")
                                info.S28 = true;

                            if (seatArray[i] == "29")
                                info.S29 = true;

                            if (seatArray[i] == "30")
                                info.S30 = true;

                            if (seatArray[i] == "31")
                                info.S31 = true;

                            if (seatArray[i] == "32")
                                info.S32 = true;

                            if (seatArray[i] == "33")
                                info.S33 = true;

                            if (seatArray[i] == "34")
                                info.S34 = true;

                            if (seatArray[i] == "35")
                                info.S35 = true;

                            if (seatArray[i] == "36")
                                info.S36 = true;

                            if (seatArray[i] == "37")
                                info.S37 = true;

                            if (seatArray[i] == "38")
                                info.S38 = true;

                            if (seatArray[i] == "39")
                                info.S39 = true;

                            if (seatArray[i] == "40")
                                info.S40 = true;

                            if (seatArray[i] == "41")
                                info.S41 = true;

                            if (seatArray[i] == "42")
                                info.S42 = true;

                            if (seatArray[i] == "43")
                                info.S43 = true;

                            if (seatArray[i] == "44")
                                info.S44 = true;

                            if (seatArray[i] == "45")
                                info.S45 = true;
                        }// End forr loop

                        int isDelte = DbContext.Database.ExecuteSqlCommand("delete TicketBookingHistory where FromId = " + model.fromId + " and ToId = " + model.toId + " and StartTime='" + model.startTime + "' and TravelDate = '" + model.DepartDate + "' ");
                        DbContext.Set<TicketBookingHistory>().Add(info);
                        DbContext.SaveChanges();


                    }// End empty seat check
                }
                if (dataList == null)
                {
                    if (model.seatList != "")
                    {
                        var seatArray = model.seatList.Split(',');
                        TicketBookingHistory info = new TicketBookingHistory();
                        info.FromId = model.fromId;
                        info.ToId = model.toId;
                        info.StartTime = model.startTime;
                        info.TravelDate = model.DepartDate;
                        info.StationName = "";

                        for (int i = 0; i < seatArray.Length; i++)
                        {
                            if (seatArray[i] == "1")
                                info.S1 = true;
                            else
                                info.S1 = false;

                            if (seatArray[i] == "2")
                                info.S2 = true;
                            else
                                info.S2 = false;

                            if (seatArray[i] == "3")
                                info.S3 = true;
                            else
                                info.S3 = false;

                            if (seatArray[i] == "4")
                                info.S4 = true;
                            else
                                info.S4 = false;

                            if (seatArray[i] == "5")
                                info.S5 = true;
                            else
                                info.S5 = false;

                            if (seatArray[i] == "6")
                                info.S6 = true;
                            else
                                info.S6 = false;

                            if (seatArray[i] == "7")
                                info.S7 = true;
                            else
                                info.S7 = false;

                            if (seatArray[i] == "8")
                                info.S8 = true;
                            else
                                info.S8 = false;

                            if (seatArray[i] == "9")
                                info.S9 = true;
                            else
                                info.S9 = false;

                            if (seatArray[i] == "10")
                                info.S10 = true;
                            else
                                info.S10 = false;

                            if (seatArray[i] == "11")
                                info.S11 = true;
                            else
                                info.S11 = false;

                            if (seatArray[i] == "12")
                                info.S12 = true;
                            else
                                info.S12 = false;

                            if (seatArray[i] == "13")
                                info.S13 = true;
                            else
                                info.S13 = false;

                            if (seatArray[i] == "14")
                                info.S14 = true;
                            else
                                info.S14 = false;

                            if (seatArray[i] == "15")
                                info.S15 = true;
                            else
                                info.S15 = false;

                            if (seatArray[i] == "16")
                                info.S16 = true;
                            else
                                info.S16 = false;

                            if (seatArray[i] == "17")
                                info.S17 = true;
                            else
                                info.S17 = false;

                            if (seatArray[i] == "18")
                                info.S18 = true;
                            else
                                info.S18 = false;

                            if (seatArray[i] == "19")
                                info.S19 = true;
                            else
                                info.S19 = false;

                            if (seatArray[i] == "20")
                                info.S20 = true;
                            else
                                info.S20 = false;

                            if (seatArray[i] == "21")
                                info.S21 = true;
                            else
                                info.S21 = false;

                            if (seatArray[i] == "22")
                                info.S22 = true;
                            else
                                info.S22 = false;

                            if (seatArray[i] == "23")
                                info.S23 = true;
                            else
                                info.S23 = false;

                            if (seatArray[i] == "24")
                                info.S24 = true;
                            else
                                info.S24 = false;

                            if (seatArray[i] == "25")
                                info.S25 = true;
                            else
                                info.S25 = false;

                            if (seatArray[i] == "26")
                                info.S26 = true;
                            else
                                info.S26 = false;

                            if (seatArray[i] == "27")
                                info.S27 = true;
                            else
                                info.S27 = false;

                            if (seatArray[i] == "28")
                                info.S28 = true;
                            else
                                info.S28 = false;

                            if (seatArray[i] == "29")
                                info.S29 = true;
                            else
                                info.S29 = false;

                            if (seatArray[i] == "30")
                                info.S30 = true;
                            else
                                info.S30 = false;

                            if (seatArray[i] == "31")
                                info.S31 = true;
                            else
                                info.S31 = false;

                            if (seatArray[i] == "32")
                                info.S32 = true;
                            else
                                info.S32 = false;

                            if (seatArray[i] == "33")
                                info.S33 = true;
                            else
                                info.S33 = false;

                            if (seatArray[i] == "34")
                                info.S34 = true;
                            else
                                info.S34 = false;

                            if (seatArray[i] == "35")
                                info.S35 = true;
                            else
                                info.S35 = false;

                            if (seatArray[i] == "36")
                                info.S36 = true;
                            else
                                info.S36 = false;

                            if (seatArray[i] == "37")
                                info.S37 = true;
                            else
                                info.S37 = false;

                            if (seatArray[i] == "38")
                                info.S38 = true;
                            else
                                info.S38 = false;

                            if (seatArray[i] == "39")
                                info.S39 = true;
                            else
                                info.S39 = false;

                            if (seatArray[i] == "40")
                                info.S40 = true;
                            else
                                info.S40 = false;

                            if (seatArray[i] == "41")
                                info.S41 = true;
                            else
                                info.S41 = false;

                            if (seatArray[i] == "42")
                                info.S42 = true;
                            else
                                info.S42 = false;

                            if (seatArray[i] == "43")
                                info.S43 = true;
                            else
                                info.S43 = false;

                            if (seatArray[i] == "44")
                                info.S44 = true;
                            else
                                info.S44 = false;

                            if (seatArray[i] == "45")
                                info.S45 = true;
                            else
                                info.S45 = false;
                        }// End forr loop

                        DbContext.Set<TicketBookingHistory>().Add(info);
                        DbContext.SaveChanges();

                        return true;
                    }// End empty seat check

                }// end Null check
                
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public User GetUserInfoByActivateKey(string key)
        {
            string qry = "SELECT * from users where UserActivateKey = '" + key + "'";
            User dataInfo = DbContext.Database.SqlQuery<User>(qry).FirstOrDefault();
            return dataInfo;
        }

        public int CheckUserEmailIsExist(string email)
        {
            string qry = "SELECT * from users where EmailAddress = '" + email + "'";
            User dataInfo = DbContext.Database.SqlQuery<User>(qry).FirstOrDefault();
            if (dataInfo != null)
                return 1;
            else
                return 0;
        }

        public string ResendActivateKey(string key, string newKey)
        {
            string email = "";
            User userInfo = new User();// DbContext.Set<User>().FirstOrDefault(x => x.UserActivateKey == key);

            if (userInfo != null) {
                userInfo.CreatedDate = DateTime.Now;
                DbContext.SaveChanges();
                email = userInfo.EmailAddress;
            }
            return email;
        }

        public bool ProcessNewUser(UserOperationModel mObj)
        {


            if (mObj != null && mObj.isAddOperation == true)
            {
                string encryptNewPass = SimpleCryptService.Factory().Encrypt(mObj.User.Password);
                mObj.User.Password = encryptNewPass;
                mObj.User.ConfirmPassword = encryptNewPass;
                mObj.User.IsActivated = false;
                mObj.User.CreatedDate = DateTime.Now;
                //mObj.User.LastActiveTime = DateTime.Now;
                DbContext.Set<User>().Add(mObj.User);
                DbContext.SaveChanges();

                UserRoleMaps map = new UserRoleMaps();
                map.UserRoleId = 5;
                map.UserId = mObj.User.UserID;
                DbContext.Set<UserRoleMaps>().Add(map);
                DbContext.SaveChanges();

                return true;
            }

            return false;
            

        }

    }
}

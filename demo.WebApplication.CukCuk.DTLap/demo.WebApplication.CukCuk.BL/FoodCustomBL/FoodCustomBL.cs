using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.DTLap.Common.Entities;
using demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using demo.WebApplication.CukCuk.DTLap.DL.FoodCustomDL;
using demo.WebApplication.CukCuk.DTLap.DL.FoodDL;
using demo.WebApplication.CukCuk.DTLap.DL.ServiceHobbieDL;
using Demo.WebApplication.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.BL.FoodCustomBL
{

    public class FoodCustomBL : BaseBL<FoodCustom>, IFoodCustomBL
    {

        #region Field

        private IFoodCustomDL _foodCustomDL;
        private IServiceHobbieDL _serviceHobbieDL;
        private IFoodDL _foodDL;
        #endregion

        #region Constructor

        public FoodCustomBL(IFoodCustomDL foodCustomDL, IServiceHobbieDL serviceHobbieDL, IFoodDL foodDL) : base(foodCustomDL)
        {
            _foodCustomDL = foodCustomDL;
            _serviceHobbieDL = serviceHobbieDL;
            _foodDL = foodDL;
        }

        #endregion

        /// <summary>
        /// Thêm mới một bản ghi món ăn và sở thích phục vụ
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public Guid InsertOneFoodCustom(FoodCustom record)
        {
            var dbConnection = _foodCustomDL.GetOpenConnection(); //Khởi tạo ở constructor
            var transaction = dbConnection.BeginTransaction();
            try
            {
                if (record != null)
                {
                    Guid result = Guid.Empty;
                    if (record.Food != null)
                    {
                        var valdateFailures = ValidateRequestData(record.Food);
                        if (valdateFailures.Count > 0)
                        {
                            throw new InvalidDataException();
                        }
                        result = _foodDL.InsertOneRecord(record.Food, transaction, dbConnection);
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }

                    if (result != Guid.Empty)
                    {
                        if (record.Hobbies?.Count > 0)
                        {
                            int count = record.Hobbies.Count;
                            for (int i = 0; i < count; i++)
                            {
                                if (record.Hobbies[i].ServiceHobbieName != null && record.Hobbies[i].ServiceHobbieName != "")
                                {
                                    record.Hobbies[i].FoodID = result;
                                    var hobbieResult = _serviceHobbieDL.InsertOneRecord(record.Hobbies[i], transaction, dbConnection);
                                    if (hobbieResult == Guid.Empty)
                                    {
                                        throw new Exception();
                                    }
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    else
                    {
                        throw new Exception();
                    }

                    return result;
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
            catch (InvalidDataException)
            {
                throw new InvalidDataException();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
                transaction.Dispose();
            }
        }

        /// <summary>
        /// Hàm Sửa một một bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>ID bản ghi vừa thêm mới</returns>
        /// Created by: DTLap(09/03/2023)
        public int UpdateOneFoodCustom(FoodCustom record)
        {
            var dbconnection = _foodCustomDL.GetOpenConnection();
            var transaction = dbconnection.BeginTransaction();
            try
            {
                if (record.Food != null)
                {
                    var valdateFailures = ValidateRequestData(record.Food);
                    if (valdateFailures.Count > 0)
                    {
                        throw new InvalidDataException();
                    }
                    var result = _foodDL.UpdateRecordById(record.Food, transaction, dbconnection);
                    if (result > 0)
                    {
                        int currentNumberServiceHobbies = _serviceHobbieDL.GetRecordsByID(record.Food.FoodID).Count;
                        if (currentNumberServiceHobbies > 0)
                        {
                            int deletedServiceHobbies = _serviceHobbieDL.DeleteRecordsByFoodID(record.Food.FoodID, dbconnection, transaction);
                            if (deletedServiceHobbies != currentNumberServiceHobbies)
                            {
                                throw new Exception();
                            }
                        }
                        if (record.Hobbies != null)
                        {
                            int count = record.Hobbies.Count;

                            if (count > 0)
                            {
                                //_serviceHobbieDL.DeleteRecordsByFoodID(record.Food.FoodID, dbconnection, transaction);

                                for (int i = 0; i < count; i++)
                                {
                                    if (record.Hobbies[i].ServiceHobbieName != null && record.Hobbies[i].ServiceHobbieName != "")
                                    {
                                        record.Hobbies[i].FoodID = record.Food.FoodID;
                                        var hobbieResult = _serviceHobbieDL.InsertOneRecord(record.Hobbies[i], transaction, dbconnection);
                                        if (hobbieResult == Guid.Empty)
                                        {
                                            throw new Exception();
                                        }
                                    }
                                }
                            }
                        }

                        //else
                        //{
                        //    _serviceHobbieDL.DeleteRecordsByFoodID(record.Food.FoodID, dbconnection, transaction);
                        //}
                        transaction.Commit();
                        return result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbconnection.Close();
                dbconnection.Dispose();
                transaction.Dispose();
            }
        }

        /// <summary>
        /// Hàm đang test, chưa sử dụng đến
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public override int AffterInsert(FoodCustom record)
        {
            int numberOfRow = record.Hobbies.Count;
            int numberAffectedRow = 0;
            for (int i = 1; i <= numberOfRow; i++)
            {
                var insertGUID = _serviceHobbieDL.InsertOneRecord(record.Hobbies[i]);
                if (insertGUID != Guid.Empty)
                {
                    numberAffectedRow++;
                }
            }
            return numberAffectedRow;
        }

        /// <summary>
        /// Hàm dùng để validate dữ liệu 
        /// </summary>
        /// <param name="record"></param>
        /// <returns>trường thông tin bị lỗi</returns>
        public List<string> ValidateRequestData(Food record)
        {
            var valdateFailures = new List<string>();
            var properties = typeof(Food).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var properyValue = property.GetValue(record);
                var requiredAttribute = (RequiredAttribute?)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();

                if (requiredAttribute != null && String.IsNullOrEmpty(properyValue?.ToString()))
                {
                    valdateFailures.Add(propertyName);
                }
            }

            if (record.FoodName.Length > 100)
            {
                valdateFailures.Add("FoodName");
            }

            if (record.FoodCode.Length > 100)
            {
                valdateFailures.Add("FoodCode");
            }

            string pattern = @"^\d+$";

            if (!Regex.IsMatch(record.SellingPrice.ToString(), pattern))
            {
                valdateFailures.Add("SellingPrice");
            }

            if (!Regex.IsMatch(record.CostPrice.ToString(), pattern))
            {
                valdateFailures.Add("CostPrice");
            }

            return valdateFailures;
        }

    }
}

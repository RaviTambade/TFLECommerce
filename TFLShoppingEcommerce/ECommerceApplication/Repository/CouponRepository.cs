using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{
    public class CouponRepository : ICouponRepository
    {
        public async Task<CouponApplyResult> applyCouponAsync(string code, decimal totalAmount)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return new CouponApplyResult { Success = false, Message = "Coupon code is required." };
            }

            using IDbConnection conn = DatabaseConnection.getConnection();
            using IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT code, discount_percentage, start_date, end_date
                                FROM discount_codes
                                WHERE code = @code
                                LIMIT 1";
            cmd.Parameters.Add(new MySqlParameter("@code", code.Trim()));

            try
            {
                conn.Open();
                using IDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return new CouponApplyResult { Success = false, Message = "Invalid or expired coupon" };
                }

                decimal percent = Convert.ToDecimal(reader["discount_percentage"]);
                DateTime startDate = Convert.ToDateTime(reader["start_date"]).Date;
                DateTime endDate = Convert.ToDateTime(reader["end_date"]).Date;
                DateTime today = DateTime.UtcNow.Date;

                if (today < startDate || today > endDate)
                {
                    return new CouponApplyResult { Success = false, Message = "Invalid or expired coupon" };
                }

                decimal discount = Math.Round(totalAmount * (percent / 100m), 2);
                decimal finalAmount = Math.Round(totalAmount - discount, 2);

                return new CouponApplyResult
                {
                    Success = true,
                    Discount = discount,
                    FinalAmount = finalAmount,
                    Message = "Coupon applied successfully",
                    Code = code.Trim()
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CouponApplyResult { Success = false, Message = "Unable to apply coupon now." };
            }
            finally
            {
                await Task.CompletedTask;
            }
        }
    }
}

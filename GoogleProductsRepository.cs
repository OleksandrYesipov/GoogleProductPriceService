 public class GoogleProductPriceService
    {
        //id product B039933
        //цены в xml <g:price>29.90 EUR</g:price> <g:sale_price>23.92 EUR</g:sale_price>
        //вывод скрипьта  "$25.19"
        public string CalculatePrice()
        {


            decimal price = 329.00M;//цена продукта в SEK
            decimal standardVAT = 1.2400M;//ндс стандартный
            decimal localVAT = 1M;//локальный ндс не нашел в perl scp присваивалось значение 1
            decimal сurrencyRate = 10.53378000000000000000M;//курс валют EUR
            string VATType = "inkl.";
            string userVATtype = "inkl.";
            string currency = "EUR";


            decimal returPrice =0;

            if (сurrencyRate != 0)
            {
                price = price / сurrencyRate;
            }
            if (VATType == "exkl.")
            {

                if (userVATtype == "exkl.")
                {
                    returPrice = price;
                }
                else if (userVATtype == "inkl.")
                {
                    returPrice = price * localVAT;
                }
            }

            else if (VATType == "inkl.")
            {
                if (userVATtype == "exkl.")
                {
                    if (standardVAT > 0)
                    {
                        returPrice = (price / standardVAT);
                    }
                }
                //выполняется только эта часть 
                else if (userVATtype == "inkl.")
                {
                    if (standardVAT > 0)
                    {
                        returPrice = ((price / standardVAT) * localVAT);
                    }
                }
            }
            var result = "";
           
            if (currency == "EUR")
            {
                result = returPrice.ToString("C", CultureInfo.CurrentCulture);
                
            }
            else
            {
                result = Convert.ToString((returPrice < 0) ? Convert.ToInt32(Convert.ToInt32(returPrice) - 0.5) : Convert.ToInt32((Convert.ToInt32(returPrice) + 0.5)));
            }

            return result;
        }
